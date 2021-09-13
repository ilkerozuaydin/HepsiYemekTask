using Core.Abstract.DependencyResolvers;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.Utilities.Mapper;
using Core.Utilities.Mapper.Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolvers.Concrete
{
    public class CoreModule : IDependencyResolverModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddSingleton<ICacheManager, RedisCacheManager>();
            services.AddSingleton<IMapper, Mapper>();
        }
    }
}