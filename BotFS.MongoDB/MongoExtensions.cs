using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotFS.MongoDB
{
    public static class MongoExtensions
    {
        public static MongoDatabase ToBFS(this IMongoDatabase db, BaseServerProvider<MongoClient> srv)
        {
            if(srv.Server == null)
            {
                return null;
            }else
            {
                var responce = new MongoDatabase
                {
                    Name = db.DatabaseNamespace.DatabaseName,
                    Tables = db.ListCollectionNames().ToList(),
                    Provider = srv,
                    Size = db.ListCollectionNames().ToList().Count
                };
                return responce;
            }
            
        }
        public static MongoTable<T> ToBFS<T>(this IMongoCollection<T> collection, MongoDatabase db)
        {
            if (db.Provider == null) return null;
            var response = new MongoTable<T>
            {
                Database = db,
                Size = collection.CountDocuments<T>(_ => true),
                Name = collection.CollectionNamespace.CollectionName
            };
            return response;
        }
        public static DBResponse<MongoTable<T>> GetTable<T>(this MongoDatabase db, string Name)
        {
            var response = new DBResponse<MongoTable<T>>
            {
                HasValue = false
            };
            if (db.Provider == null)
            {
                return response;
            }
            else
            {
                
                if (db.Tables == null) db.Refresh();
                response.HasValue = true;
                response.Response = db.Provider.Server.GetDatabase(db.Name).GetCollection<T>(Name).ToBFS(db);
                db.Refresh();
                return response;
            }
            
        }

    }
}
