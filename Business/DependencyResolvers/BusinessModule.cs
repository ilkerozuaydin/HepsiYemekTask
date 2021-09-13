
using Business.Abstract;
using Business.Concrete;
using Core.Abstract.DependencyResolvers;
using DataAccess.Concrete.MongoDb.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Business.DependencyResolvers
{
    public class BusinessModule : IDependencyResolverModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddSingleton<MongoDbContextBase,MongoDbContext>();
            services.AddTransient<IProductService, ProductManager>();
            services.AddTransient<ICategoryService, CategoryManager>();
            
        }
    }
}