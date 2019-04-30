using System.Windows.Input;
using Evidences.Services;
using Xamarin.Forms;
using System.Linq;
using Evidences.Models;
using Prism.Navigation;
using Prism.Commands;

namespace Evidences.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        protected IYoutubeSearchService YoutubeSearchService { get; }
        protected IReactionService ReactionService { get; }
        protected ISongService SongService { get; }

        public MainViewModel(IStateService stateService,
            IUserService userService,
            ISignalRService signaRService,
            INavigationService navigationService,
            IYoutubeSearchService youtubeSearchService,
            IReactionService reactionService,
            ISongService songService) :
             base(stateService, userService, signaRService, navigationService)
        {
            YoutubeSearchService = youtubeSearchService;
            ReactionService = reactionService;
            SongService = songService;

            SearchYoutube = new DelegateCommand(SearchYoutubeExecute);
            SendReaction = new DelegateCommand(SendReactionExecute);

            RegisterSignalREvents();
        }

        public string YoutubeSearchQuery { get; set; }
        public string Reaction { get; set; }
        public DelegateCommand SearchYoutube { get; }

        public DelegateCommand SendReaction { get; }

        private async void SearchYoutubeExecute()
        {
            await NavigationService.NavigateAsync("../Search");
            return;

            try
            {
                var results = await YoutubeSearchService.SearchVideo($"{YoutubeSearchQuery} karaoke", 1);
                var result = results.FirstOrDefault();
                if (result == null)
                {
                    return;
                }

                var song = new Song()
                {
                    Title = result.Title,
                    Author = result.Author,
                    Description = result.Description,
                    Duration = result.Duration,
                    Url = result.Url,
                    Thumbnail = result.Thumbnail,
                    NoAuthor = result.NoAuthor,
                    NoDescription = result.NoDescription,
                    ViewCount = result.ViewCount,
                    AddedByUser = CurentUser.Id
                };

                await SongService.Add(song);
            }
            catch (System.Exception)
            {
            }
        }

        private async void SendReactionExecute()
        {
            try
            {
                await ReactionService.SendReaction(Reaction);
            }
            catch (System.Exception ex)
            {

            }
        }
    }
}