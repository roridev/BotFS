using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace BotFS.MySql
{
    public class MySqlProvider : BaseServerProvider<MySqlConnection>
    {
        public MySqlConnection Server { get;set; }
        public List<string> Databases { get;set; }
        public ServerStatus Status { get;set; }

        public DBResponse<List<string>> Refresh()
        {
            var response = new DBResponse<List<string>>
            {
                HasValue = false
            };
            if(this.Server != null)
            {
                if (this.Server.State != ConnectionState.Open) this.Server.Open();
                response.HasValue = true;
                response.Response = this.Server.Query<string>("show databases;").AsList<string>();
            }
            return response;
        }
    }
}
