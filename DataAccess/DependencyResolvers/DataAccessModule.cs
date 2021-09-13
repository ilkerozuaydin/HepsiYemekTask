using Core.Abstract.DependencyResolvers;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb;
using DataAccess.Concrete.MongoDb.Collections;
using DataAccess.Concrete.MongoDb.Context;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.DependencyResolvers
{
    public class DataAccessModule: IDependencyResolverModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository>(t=> new CategoryMongoRepository(t.GetRequiredService<MongoDbContextBase>(),Collections.Category));
            services.AddScoped<IProductRepository>(t=> new ProductMongoRepository(t.GetRequiredService<MongoDbContextBase>(),Collections.Product));

        }

    }
}