using System.Windows.Input;
using Evidences.Services;
using Xamarin.Forms;
using System.Linq;
using Evidences.Models;
using Prism.Navigation;
using Prism.Commands;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Evidences.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        protected IYoutubeSearchService YoutubeSearchService { get; }
        protected IReactionService ReactionService { get; }
        protected ISongService SongService { get; }

        private CurrentSong currentSong;
        private ObservableCollection<Song> songQueue = new ObservableCollection<Song>();

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

            SearchYoutube = new DelegateCommand<string>(async (x) => await SearchYoutubeExecute(x));
            NowPlaying = new DelegateCommand(async () => await NowPlayingExecute());

            RegisterSignalREvents();
        }

        public CurrentSong CurrentSong
        {
            get => currentSong;
            set 
            {
                SetProperty(ref currentSong, value);
                RaisePropertyChanged(nameof(HasNowPlaying));
            }
        }

        public bool HasNowPlaying => CurrentSong != null;
        public bool HasSongsToBePlayed => SongQueue.Count() > 0;
        public bool IsQueueEmpty => SongQueue.Count() == 0;
        public bool IsNowPlayingEmpty => CurrentSong == null;

        public string SearchQuery { get; set; }

        public ObservableCollection<Song> SongQueue
        {
            get => songQueue;
            set => SetProperty(ref songQueue, value);
        }

        public ICommand SearchYoutube { get; }
        public ICommand NowPlaying { get; }

        private async Task SearchYoutubeExecute(string query)
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("query", SearchQuery);
            await NavigationService.NavigateAsync("Search", navigationParams);
        }

        private async Task NowPlayingExecute()
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("song", CurrentSong);
            await NavigationService.NavigateAsync("NowPlaying", navigationParams);
        }

        private void UpdateNowPlaying(State state)
            => CurrentSong = state.CurrentSong;

        private void UpdateQueue(State state)
        {
            SongQueue.Clear();
            SongQueue = new ObservableCollection<Song>(state.Queue);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            await ExecuteBusyAction(async () =>
            {
                if (!SignalRService.IsConnected)
                {
                    await SignalRService.Connect();
                }
                var state = await StateService.GetState();
                UpdateNowPlaying(state);
                UpdateQueue(state);
            });
        }

        protected override void SignalRService_OnSongStarted(object sender, CurrentSong e)
        {
            if (SongQueue.Any(x =>x.Id == e.SongId))
            {
                var song = SongQueue.FirstOrDefault(x => x.Id == e.SongId);
                SongQueue.Remove(song);
                RaisePropertyChanged(nameof(IsQueueEmpty));
                RaisePropertyChanged(nameof(HasSongsToBePlayed));

            }
            this.CurrentSong = e;
        }

        protected override void SignalRService_OnSongFinished(object sender, CurrentSong e)
        {
            base.SignalRService_OnSongFinished(sender, e);
            this.CurrentSong = null;
        }
    }
}