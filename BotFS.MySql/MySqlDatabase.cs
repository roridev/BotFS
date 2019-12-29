using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotFS.MySql
{
    public class MySqlDatabase : IDatabase<MySqlProvider, MySqlConnection>
    {
        public string Name { get;set; }
        public BaseServerProvider<MySqlConnection> Provider { get;set; }
        public int Size { get;set; }
        public List<string> Tables { get;set; }

        public DBResponse<bool> Drop()
        {
            var response = new DBResponse<bool>
            {
                HasValue = false
            };
            if (this.Provider.Databases == null) this.Provider.Refresh();
            if (this.Provider.Databases.Contains(this.Name))
            {
                this.Provider.Server.Execute($"drop database {this.Name};");
                response.HasValue = true;
                response.Response = true;
            }
            return response;
        }

        public DBResponse<List<string>> Refresh()
        {
            var response = new DBResponse<List<string>> 
            {
                HasValue = false
            };
            if (this.Provider.Server.Database != this.Name) this.Provider.Server.Execute($"use {this.Name};");
            response.HasValue = true;
            this.Tables = this.Provider.Server.Query<string>("show tables;").AsList<string>();
            this.Size = this.Tables.Count;
            response.Response = this.Tables;
            return response;
        }
        public static implicit operator MySqlDatabase(DBResponse<IDatabase<MySqlProvider, MySqlConnection>> o)
        {
            return (MySqlDatabase)o.Response;
        }
    }
    
}
