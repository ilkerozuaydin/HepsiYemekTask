using Microsoft.Extensions.DependencyInjection;

namespace Core.Abstract.DependencyResolvers
{
    public interface IDependencyResolverModule
    {
        void Load(IServiceCollection services);

    }
}