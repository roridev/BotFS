using MongoDB.Driver;
 using static BotFS.Utils.Logger;
using System;

namespace BotFS.Test
{
    public class Mongo : IServerProvider<MongoProvider,MongoClient>
    {
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public BaseServerProvider<MongoClient> Provider {get;set;}

        public Mongo(string connectionString, string name)
        {
            this.Name = name;
            this.ConnectionString = connectionString;
            var connection = this.TryConnect();
            if (connection.HasValue)
                this.Provider = connection.Response;
        }

        public DBResponse<Database<MongoProvider, MongoClient>> GetDatabase(string Name)
        {
            var response = new DBResponse<Database<MongoProvider, MongoClient>>
            {
                HasValue = this.Provider.Databases.Contains(Name),
                Response = this.Provider.Databases.Contains(Name) ? this.Provider.Server.GetDatabase(Name).ToBFS(this.Provider) : null
            };
            return response;
        }

        public DBResponse<BaseServerProvider<MongoClient>> TryConnect()
        {
            Log(this.Name,"Attemping to connect to database.");
            this.Provider = this.Provider ?? new MongoProvider();
            var response = new DBResponse<BaseServerProvider<MongoClient>>
            {
                HasValue = false
            };
            try
            {
                var _server = new MongoClient(
                    new MongoClientSettings
                    {
                        Server = new MongoServerAddress(this.ConnectionString),
                        ClusterConfigurator = config => config.ConfigureCluster(
                            settings => settings.With(serverSelectionTimeout: TimeSpan.FromSeconds(1))
                            )
                    }
                    );
                MongoDatabase ping = (MongoDatabase)this.GetDatabase("local").Response;
                var innerProvider = new MongoProvider
                {
                    Server = _server,
                    Status = ServerStatus.CONNECTED
                };
                response.HasValue = true;
                response.Response = innerProvider;
                Log(this.Name, "Connected.");
            }
            catch (TimeoutException)
            {
                
                this.Provider.Status = ServerStatus.DISCONNECTED;
                Warn(this.Name,"Couldn't connect to database.");
            }
            catch (ArgumentException)
            {
                this.Provider.Status = ServerStatus.INVALID;
                Err(this.Name, "Invalid Connection String");
            }
            return response;
        }
    }
}