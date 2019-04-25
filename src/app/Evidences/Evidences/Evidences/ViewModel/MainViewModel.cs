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
        protected ISongService SongService { get; }

        public MainViewModel(IStateService stateService,
            IUserService userService,
            ISignalRService signaRService,
            INavigationService navigationService,
            IYoutubeSearchService youtubeSearchService,
            ISongService songService) :
             base(stateService, userService, signaRService, navigationService)
        {
            YoutubeSearchService = youtubeSearchService;
            SongService = songService;

            SearchYoutube = new DelegateCommand(SearchYoutubeExecute);

            RegisterSignalREvents();
        }

        public string YoutubeSearchQuery { get; set; }
        public DelegateCommand SearchYoutube { get; }

        private async void SearchYoutubeExecute()
        {
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

    }
}