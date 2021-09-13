using MongoDB.Bson;

namespace Core.DataAccess.MongoDb.Concrete
{
    public abstract class MongoBaseEntity
    {
        public ObjectId Id { get; set; }
    }
}
