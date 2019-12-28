using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotFS.Test
{
    public class MongoDatabase : Database<MongoProvider, MongoClient>
    {
        public string Name {get;set; }
        public BaseServerProvider<MongoClient> Server {get;set; }
        public int Size {get;set; }
        public List<string> Tables {get;set; }
    }
}
