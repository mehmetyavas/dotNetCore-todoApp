using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceColection)
        {
            serviceColection.AddMemoryCache();
            serviceColection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceColection.AddSingleton<ICacheManager, MemoryCacheManager>();

            

        }
    }
}
