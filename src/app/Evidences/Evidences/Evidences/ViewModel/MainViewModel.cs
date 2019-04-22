using System.Windows.Input;
using Evidences.Services;
using Xamarin.Forms;
using System.Linq;
using Evidences.Models;

namespace Evidences.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        protected ISignalRService SignalRService { get; }
        protected IYoutubeSearchService YoutubeSearchService { get; }
        protected IUserService UserService { get; }
        protected ISongService SongService { get; }

        public MainViewModel(
                IStateService stateService,
                ISignalRService signaRService,
                IYoutubeSearchService youtubeSearchService,
                IUserService userService,
                ISongService songService
            ) : base(stateService)
        {
            SignalRService = signaRService;
            YoutubeSearchService = youtubeSearchService;
            UserService = userService;
            SongService = songService;

            CreateUser = new Command(CreateUserExecute);
            SearchYoutube = new Command(SearchYoutubeExecute);

            RegisterSignalREvents();
        }

        public string UserName { get; set; }

        public string YoutubeSearchQuery { get; set; }

        public ICommand CreateUser { get; }

        public ICommand SearchYoutube { get; }

        public User CurentUser => UserService.Get();

        private async void CreateUserExecute()
        {
            try
            {
                if (CurentUser == null)
                {
                    await UserService.Add(UserName);
                }
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