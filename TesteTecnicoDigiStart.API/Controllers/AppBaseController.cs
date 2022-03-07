using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TesteTecnicoDigiStart.API.Controllers
{
    public class AppBaseController : ControllerBase
    {
        protected readonly IServiceProvider ServiceProvider;
        protected readonly IConfiguration Configuration;

        protected T GetService<T>()
        {
            return ServiceProvider.GetService<T>();
        }

        public AppBaseController(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            ServiceProvider = serviceProvider;
            Configuration = configuration;
        }
    }
}
