using Autofac;
using Evidences.Factories;
using Evidences.Repositories;
using Evidences.Services;
using Evidences.ViewModel;
using Evidences.YouTube;

namespace Evidences
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterRepositories(builder);
            RegisterServices(builder);
            RegisterViewModel(builder);
        }

        private void RegisterRepositories(ContainerBuilder builder)
        {
            builder
                .Register(c => HttpClientFactory.Create())
                .AsSelf()
                .SingleInstance();

            builder
                .Register(RepositoryFactory.Create<ICurrentSongRepository>)
                .AsSelf();

            builder
                .Register(RepositoryFactory.Create<IReactionRepository>)
                .AsSelf();

            builder
                .Register(RepositoryFactory.Create<IScoreRepository>)
                .AsSelf();

            builder
                .Register(RepositoryFactory.Create<ISignalRRepository>)
                .AsSelf();

            builder
                .Register(RepositoryFactory.Create<ISongRepository>)
                .AsSelf();

            builder
                .Register(RepositoryFactory.Create<IStateRepository>)
                .AsSelf();

            builder
                .Register(RepositoryFactory.Create<IUserRepository>)
                .AsSelf();
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder
                .RegisterType<VideoSearch>()
                .AsSelf();

            builder
                .RegisterType<SignalRService>()
                .As<ISignalRService>()
                .SingleInstance();

            builder
                .RegisterType<SongService>()
                .As<ISongService>();

            builder
                .RegisterType<StateService>()
                .As<IStateService>();

            builder
                .RegisterType<UserService>()
                .As<IUserService>();

            builder
                .RegisterType<YoutubeSearchService>()
                .As<IYoutubeSearchService>();
        }

        private void RegisterViewModel(ContainerBuilder builder)
        {
            builder
                .RegisterType<MainViewModel>()
                .AsSelf();
        }
    }
}