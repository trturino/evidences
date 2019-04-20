using Autofac;
using Xamarin.Forms;

namespace Evidences
{
    public partial class App : Application
    {
        public static IContainer Container { get; private set; }

        public App()
        {
            InitializeComponent();
            BootstrapIoc();

            MainPage = new NavigationPage(new MainPage());
        }

        private void BootstrapIoc()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new ApplicationModule());
            Container = builder.Build();

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