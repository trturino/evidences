using System.Threading.Tasks;
using Autofac;
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
            await InitializeNavigaton();
        }

        protected Task InitializeNavigaton()
           => NavigationService.NavigateAsync("Go/Main");

        //TODO: half mozzarella half pepperoni
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var builder = containerRegistry.GetBuilder();
            builder.RegisterModule(new ApplicationModule());

            containerRegistry.RegisterForNavigation<NavigationPage>("Go");
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>("Main");
            containerRegistry.RegisterForNavigation<OnboardingPage, OnboardingViewModel>("Login");
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