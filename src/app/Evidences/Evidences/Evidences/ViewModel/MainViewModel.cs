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

        private Song currentSong = new Song();
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

            SearchYoutube = new DelegateCommand(async () => await SearchYoutubeExecute());
            NowPlaying = new DelegateCommand(async () => await NowPlayingExecute());

            RegisterSignalREvents();
        }

        public Song CurrentSong
        {
            get => currentSong;
            set => SetProperty(ref currentSong, value);
        }

        public ObservableCollection<Song> SongQueue
        {
            get => songQueue;
            set => SetProperty(ref songQueue, value);
        }

        public DelegateCommand SearchYoutube { get; }
        public DelegateCommand NowPlaying { get; }

        private Task SearchYoutubeExecute()
            => NavigationService.NavigateAsync("Search");

        private Task NowPlayingExecute()
            => NavigationService.NavigateAsync("Go/NowPlaying");

        private async Task UpdateNowPlaying(State state)
        {
            await ExecuteBusyAction(async () =>
            {
                CurrentSong = state.CurrentSong;
            });
        }

        private async Task UpdateQueue(State state)
        {
            SongQueue.Clear();
            SongQueue = new ObservableCollection<Song>(state.Queue);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var state = await StateService.GetState();
            await UpdateNowPlaying(state);
            await UpdateQueue(state);
        }
    }
}