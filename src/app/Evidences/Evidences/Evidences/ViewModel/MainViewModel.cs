using System.Windows.Input;
using Evidences.Services;
using Xamarin.Forms;
using System.Linq;
using Evidences.Models;
using Prism.Navigation;
using Prism.Commands;
using System.Threading.Tasks;

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

            SearchYoutube = new DelegateCommand(async () => await SearchYoutubeExecute());
            //SendReaction = new DelegateCommand(SendReactionExecute);

            RegisterSignalREvents();
        }

        public string YoutubeSearchQuery { get; set; }
        public string Reaction { get; set; }
        public DelegateCommand SearchYoutube { get; }
        public DelegateCommand SendReaction { get; }

        private Task SearchYoutubeExecute()
            => NavigationService.NavigateAsync("Go/Search");

        //private async void SendReactionExecute()
        //{
        //    try
        //    {
        //        await ReactionService.SendReaction(Reaction);
        //    }
        //    catch (System.Exception ex)
        //    {

        //    }
        //}
    }
}