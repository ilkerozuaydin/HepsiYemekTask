using Core.DataAccess.MongoDb.Abstract;
using Core.DataAccess.MongoDb.Concrete;
using Core.DataAccess.MongoDb.Concrete.Models;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;

namespace DataAccess.Concrete.MongoDb
{
    public class CategoryMongoRepository : MongoDbRepositoryBase<Category>, ICategoryRepository
    {
        public CategoryMongoRepository(MongoDbContextBase mongoDbContext, string collectionName) : base(mongoDbContext.mongoConnectionSettings, collectionName)
        {
        }
    }
}
