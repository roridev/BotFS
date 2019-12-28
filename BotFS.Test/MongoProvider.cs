using System.Collections.Generic;
using MongoDB.Driver;
namespace BotFS.MongoDB
{
    public class MongoProvider : BaseServerProvider<MongoClient>
    {
        public MongoClient Server {get;set;}
        public List<string> Databases {get;set;}
        public ServerStatus Status {get;set;}

        public DBResponse<List<string>> Refresh()
        {
            var response = new DBResponse<List<string>>
            {
                HasValue = false
            };
            if (this.Server != null)
            {
                response.Response = this.Server.ListDatabaseNames().ToList();
                response.HasValue = true;
            }
            return response;
        }
    }
}