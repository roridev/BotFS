using MongoDB.Driver;
using System.Collections.Generic;

namespace BotFS.MongoDB
{
	public class MongoProvider : BaseServerProvider<MongoClient>
	{
		public MongoClient Server { get; set; }
		public List<string> Databases { get; set; }
		public ServerStatus Status { get; set; }

		public DBResponse<List<string>> Refresh()
		{
			var response = new DBResponse<List<string>>
			{
				HasValue = false
			};
			if (Server != null)
			{
				response.Response = Server.ListDatabaseNames().ToList();
				response.HasValue = true;
			}
			return response;
		}
	}
}