using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotFS.Test
{
    public class MongoDatabase : Database<MongoProvider, MongoClient>
    {
        public string Name {get;set; }
        public BaseServerProvider<MongoClient> Provider {get;set; }
        public int Size {get;set; }
        public List<string> Tables {get;set; }
        public DBResponse<bool> Drop()
        {
            var response = new DBResponse<bool>
            {
                HasValue = false
            };
            if (this.Provider.Databases == null) this.Provider.Refresh();
            if(this.Provider.Databases.Contains(this.Name))
            { 
                this.Provider.Server.DropDatabase(this.Name);
                response.HasValue = true;
                response.HasValue = true;
            }
            return response;
        }
        public static implicit operator MongoDatabase(DBResponse<Database<MongoProvider, MongoClient>> o)
        {
            return (MongoDatabase)o.Response;
        }
    }
}
