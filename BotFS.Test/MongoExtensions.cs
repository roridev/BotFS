using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotFS.Test
{
    public static class MongoExtensions
    {
        public static MongoDatabase ToBFS(this IMongoDatabase db, BaseServerProvider<MongoClient> srv)
        {
            var responce = new MongoDatabase
            {
                Name = db.DatabaseNamespace.DatabaseName,
                Tables = db.ListCollectionNames().ToList(),
                Server = srv,
                Size = db.ListCollectionNames().ToList().Count
            };
            return responce;
        }
    }
}
