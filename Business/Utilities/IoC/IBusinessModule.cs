using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Utilities.IoC
{
    public interface IBusinessModule
    {
        void Load(IServiceCollection services, IConfiguration configuration);
    }
}
