using System.Windows.Input;
using Evidences.Services;
using Xamarin.Forms;

namespace Evidences.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        protected ISignalRService SignalRService { get; }
        protected IYoutubeSearchService YoutubeSearchService { get; }
        protected IUserService UserService { get; }

        public MainViewModel(
                IStateService stateService,
                ISignalRService signaRService,
                IYoutubeSearchService youtubeSearchService,
                IUserService userService
            ) : base(stateService)
        {
            SignalRService = signaRService;
            YoutubeSearchService = youtubeSearchService;
            UserService = userService;
            CreateUser = new Command(CreateUserExecute);
            SearchYoutube = new Command(SearchYoutubeExecute);

            RegisterSignalREvents();
        }

        public string UserName { get; set; }

        public string YoutubeSearchQuery { get; set; }

        public ICommand CreateUser { get; }

        public ICommand SearchYoutube { get; }

        private async void CreateUserExecute()
        {
            try
            {
                await UserService.Add(UserName);
                await SignalRService.Connect();
            }
            catch (System.Exception ex)
            {
            }
        }

        private async void SearchYoutubeExecute()
        {
            try
            {
                var results = await YoutubeSearchService.SearchVideo($"{YoutubeSearchQuery} karaoke", 1);
            }
            catch (System.Exception)
            {
            }
        }

        private void RegisterSignalREvents()
        {
            SignalRService.OnRemoveSong += SignalRService_OnRemoveSong;
            SignalRService.OnSongAdded += SignalRService_OnSongAdded;
            SignalRService.OnSongFinished += SignalRService_OnSongFinished;
            SignalRService.OnSongStarted += SignalRService_OnSongStarted;
        }

        private void SignalRService_OnSongStarted(object sender, Models.CurrentSong e)
        {
            
        }

        private void SignalRService_OnSongFinished(object sender, Models.CurrentSong e)
        {
            
        }

        private void SignalRService_OnSongAdded(object sender, Models.Song e)
        {
            
        }

        private void SignalRService_OnRemoveSong(object sender, Models.SongToRemove e)
        {
            
        }
    }
}