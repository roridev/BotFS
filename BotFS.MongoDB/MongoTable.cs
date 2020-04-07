using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BotFS.MongoDB
{
	public class MongoTable<T> : IDBTable<T, MongoDatabase>
	{
		public MongoDatabase Database { get; set; }
		public long Size { get; set; }
		public string Name { get; set; }

		public DBResponse<bool> Add(T value)
		{
			var response = new DBResponse<bool>
			{
				HasValue = false
			};
			if (Database.Tables == null) Database.Refresh();
			try
			{
				Database.Provider.Server
				.GetDatabase(Database.Name)
				.GetCollection<T>(Name)
				.InsertOne(value);
				response.HasValue = true;
				response.Response = true;
				return response;
			}
			catch (MongoWriteException)
			{
				return response;
			}
		}

		public DBResponse<bool> Delete(Expression<Func<T, bool>> filter)
		{
			var response = new DBResponse<bool>
			{
				HasValue = false
			};
			try
			{
				Database.Provider.Server
				.GetDatabase(Database.Name)
				.GetCollection<T>(Name)
				.DeleteOne(filter);
				response.HasValue = true;
				response.Response = true;
				return response;
			}
			catch (MongoException)
			{
				return response;
			}
		}

		public DBResponse<bool> Drop()
		{
			var response = new DBResponse<bool>
			{
				HasValue = false
			};
			try
			{
				Database.Provider.Server
				.GetDatabase(Database.Name)
				.DropCollection(Name);
				response.HasValue = true;
				response.Response = true;
				Database.Refresh();
				return response;
			}
			catch (MongoException)
			{
				return response;
			}
		}

		public DBResponse<T> Find(Expression<Func<T, bool>> filter)
		{
			var response = new DBResponse<T>
			{
				HasValue = false
			};
			try
			{
				response.HasValue = true;
				response.Response = Database.Provider.Server
				.GetDatabase(Database.Name)
				.GetCollection<T>(Name)
				.Find<T>(filter).First();
				return response;
			}
			catch (MongoException)
			{
				return response;
			}
		}

		public DBResponse<List<T>> FindAll(Expression<Func<T, bool>> filter)
		{
			var response = new DBResponse<List<T>>
			{
				HasValue = false
			};
			try
			{
				response.HasValue = true;
				response.Response = Database.Provider.Server
				.GetDatabase(Database.Name)
				.GetCollection<T>(Name)
				.Find<T>(filter).ToList();
				return response;
			}
			catch (MongoException)
			{
				return response;
			}
		}

		public DBResponse<IDBTable<T, MongoDatabase>> Refresh()
		{
			var response = new DBResponse<IDBTable<T, MongoDatabase>>
			{
				HasValue = true,
				Response = Database.Provider.Server
					.GetDatabase(Database.Name)
					.GetCollection<T>(Name).ToBFS<T>(Database)
			};
			Size = response.Response.Size;
			return response;
		}

		public DBResponse<bool> Update(Expression<Func<T, bool>> filter, T update)
		{
			var response = new DBResponse<bool>
			{
				HasValue = false
			};
			try
			{
				Database.Provider.Server
				.GetDatabase(Database.Name)
				.GetCollection<T>(Name)
				.ReplaceOne(filter, update);
				response.HasValue = true;
				response.Response = true;
				return response;
			}
			catch (MongoException)
			{
				return response;
			}
		}

		public static implicit operator MongoTable<T>(DBResponse<MongoTable<T>> o) => o.Response;
	}
}