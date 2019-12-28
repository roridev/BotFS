using MongoDB.Driver;
using System;

namespace BotFS.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Mongo m = new Mongo("http://google.com","test database");
            MongoDatabase teste = m.GetDatabase("teste");
            
            Console.WriteLine("AAAAAAAAAAA");
        }
    }
}
