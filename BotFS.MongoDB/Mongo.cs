using MongoDB.Driver;
using System;
using static BotFS.Utils.Logger;

namespace BotFS.MongoDB
{
	public class Mongo : IServerProvider<MongoProvider, MongoClient>
	{
		public string ConnectionString { get; set; } = "";
		public string Name { get; set; } = "";

		public int Port { get; internal set; } = 27017;
		public BaseServerProvider<MongoClient> Provider { get; set; } = new MongoProvider() { Status = ServerStatus.DISCONNECTED };

		public Mongo(string connectionString, string name)
		{
			ConnectionString = connectionString;
			Name = name;
		}

		public Mongo(string connectionString, int port, string name)
		{
			ConnectionString = connectionString;
			Port = port;
			Name = name;
		}

		public DBResponse<IDatabase<MongoProvider, MongoClient>> GetDatabase(string name) => Provider.Status != ServerStatus.CONNECTED
				? new DBResponse<IDatabase<MongoProvider, MongoClient>> { HasValue = false }
				: new DBResponse<IDatabase<MongoProvider, MongoClient>>
				{
					HasValue = true,
					Response = Provider.Server.GetDatabase(name).ToBFS(Provider)
				};

		public DBResponse<BaseServerProvider<MongoClient>> TryConnect()
		{
			Log(Name, "Connecting...");
			var response = new DBResponse<BaseServerProvider<MongoClient>>()
			{
				HasValue = false
			};

			try
			{
				var _server = new MongoClient(new MongoClientSettings
				{
					Server = new MongoServerAddress(ConnectionString),
					ClusterConfigurator = config => config.ConfigureCluster(
						settings => settings.With(serverSelectionTimeout: TimeSpan.FromSeconds(1))
					)
				});

				_server.StartSession();
				Provider.Server = _server;
				Provider.Status = ServerStatus.CONNECTED;
				Provider.Databases = _server.ListDatabaseNames().ToList();
				response.Response = Provider;
			}
			catch (TimeoutException)
			{
				Provider.Status = ServerStatus.DISCONNECTED;
				Warn(Name, "Conection Unreachable.");
			}
			catch (ArgumentException)
			{
				Provider.Status = ServerStatus.INVALID;
				Warn(Name, "Invalid Connection String. Connection Canceled.");
			}
			return response;
		}
	}
}