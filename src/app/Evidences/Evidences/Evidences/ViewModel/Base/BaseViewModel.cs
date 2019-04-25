using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Evidences.Models;
using Evidences.Services;
using Prism.Mvvm;
using Prism.Navigation;

namespace Evidences.ViewModel
{
    public abstract class BaseViewModel : BindableBase, INavigationAware, IDestructible
    {
        protected ISignalRService SignalRService { get; }
        protected IUserService UserService { get; }
        protected IStateService StateService { get; }
        protected INavigationService NavigationService { get; }

        protected BaseViewModel(IStateService stateService,
            IUserService userService,
            ISignalRService signaRService,
            INavigationService navigationService)
        {
            StateService = stateService;
            SignalRService = signaRService;
            UserService = userService;
            NavigationService = navigationService;
        }

        public bool IsNotBusy { get { return !IsBusy; } }
        public bool IsBusy { get; set; }
        public User CurentUser => UserService.Get();

        protected virtual void RegisterSignalREvents()
        {
            SignalRService.OnRemoveSong += SignalRService_OnRemoveSong;
            SignalRService.OnSongAdded += SignalRService_OnSongAdded;
            SignalRService.OnSongFinished += SignalRService_OnSongFinished;
            SignalRService.OnSongStarted += SignalRService_OnSongStarted;
        }

        protected virtual void SignalRService_OnSongStarted(object sender, Models.CurrentSong e)
        {
        }

        protected virtual void SignalRService_OnSongFinished(object sender, Models.CurrentSong e)
        {
        }

        protected virtual void SignalRService_OnSongAdded(object sender, Models.Song e)
        {
        }

        protected virtual void SignalRService_OnRemoveSong(object sender, Models.SongToRemove e)
        {
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {
        }

        public virtual void Destroy()
        {
        }

        protected async Task ExecuteBusyAction(Func<Task> theBusyAction)
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                await theBusyAction();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}