using System.Net.Http;
using Autofac;
using Refit;

namespace Evidences.Factories
{
    public static class RepositoryFactory
    {
        public static T Create<T>(IComponentContext c)
        {
            return RestService.For<T>(c.Resolve<HttpClient>());
        }
    }
}