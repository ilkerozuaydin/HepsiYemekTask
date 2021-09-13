using Core.DataAccess.MongoDb.Abstract;
using Core.DataAccess.MongoDb.Concrete;
using Core.DataAccess.MongoDb.Concrete.Models;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;

namespace DataAccess.Concrete.MongoDb
{
    public class ProductMongoRepository : MongoDbRepositoryBase<Product>, IProductRepository
    {
        public ProductMongoRepository(MongoDbContextBase mongoDbContextBase, string collectionName) : base(mongoDbContextBase.mongoConnectionSettings, collectionName)
        {
        }
    }
}
