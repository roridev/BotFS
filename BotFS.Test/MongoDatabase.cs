using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotFS.MongoDB
{
    public class MongoDatabase : IDatabase<MongoProvider, MongoClient>
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

        public DBResponse<List<string>> Refresh()
        {
            var response = new DBResponse<List<string>>
            {
                HasValue = false
            };
            if (this.Provider.Databases == null) this.Provider.Refresh();
            if(this.Provider.Databases.Contains(this.Name))
            {
                this.Tables = this.Provider.Server.GetDatabase(this.Name).ToBFS(this.Provider).Tables;
                response.HasValue = true;
                response.Response = this.Tables;
            }
            return response;
        }

        public static implicit operator MongoDatabase(DBResponse<IDatabase<MongoProvider, MongoClient>> o)
        {
            return (MongoDatabase)o.Response;
        }
    }
}
