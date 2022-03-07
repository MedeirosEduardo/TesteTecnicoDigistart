using Microsoft.Extensions.DependencyInjection;
using TesteTecnicoDigiStart.Interface;
using TesteTecnicoDigiStart.Repository;

namespace TesteTecnicoDigiStart.API
{
    public class DependencyInjection
    {
        public static void Register(IServiceCollection serviceProvider)
        {
            RepositoryDependence(serviceProvider);
        }

        private static void RepositoryDependence(IServiceCollection serviceProvider)
        {
            serviceProvider.AddScoped<IRequestHelper, RequestHelper>();
            serviceProvider.AddScoped<IUsersRepository, UsersRepository>();
        }
    }
}
