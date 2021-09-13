using Core.DataAccess.MongoDb.Abstract;
using Core.DataAccess.MongoDb.Concrete;
using Core.Entities.Abstract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Core.Entities.Concrete
{
    public class Category : MongoBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}