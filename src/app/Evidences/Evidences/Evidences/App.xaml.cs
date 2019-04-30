using System.Threading.Tasks;
using Autofac;
using Evidences.Services;
using Evidences.ViewModel;
using Evidences.Views;
using Prism;
using Prism.Autofac;
using Prism.Ioc;
using Xamarin.Forms;

namespace Evidences
{
    public partial class App : PrismApplication
    {
        public App() : this(null) { }

        public App(IPlatformInitializer platformInitializer) : base(platformInitializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
#if DEBUG
            HotReloader.Current.Start(this);
#endif
            await InitializeNavigaton();
        }

        //TODO: Future William, although this works, you are not satisfied!
        protected async Task InitializeNavigaton()
        {
            var userService = Container.Resolve<IUserService>();
            var currentUser = userService?.Get();

            if (currentUser == null)
            {
                await NavigationService.NavigateAsync("Go/Onboarding");
            }
            else
            {
                await NavigationService.NavigateAsync("Go/Home");
            }
        }

        //TODO: half mozzarella half pepperoni
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var builder = containerRegistry.GetBuilder();
            builder.RegisterModule(new ApplicationModule());

            containerRegistry.RegisterForNavigation<NavigationPage>("Go");
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>("Home");
            containerRegistry.RegisterForNavigation<OnboardingPage, OnboardingViewModel>("Onboarding");
            containerRegistry.RegisterForNavigation<SearchPage, SearchViewModel>("Search");
            containerRegistry.RegisterForNavigation<NowPlayingPage, NowPlayingViewModel>("NowPlaying");
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}