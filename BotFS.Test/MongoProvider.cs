using System.Collections.Generic;
using MongoDB.Driver;
namespace BotFS.Test
{
    public class MongoProvider : BaseServerProvider<MongoClient>
    {
        public MongoClient Server {get;set;}
        public List<string> Databases {get;set;}
        public ServerStatus Status {get;set;}
    }
}