using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Dapper;

namespace BotFS.MySql
{
    public class MySqlTable<T> : IDBTable<T, MySqlDatabase>
    {
        public MySqlDatabase Database { get;set; }
        public string Name { get;set; }
        public long Size { get;set; }

        public DBResponse<bool> Add(T value)
        {
            var response = new DBResponse<bool>
            {
                HasValue = false
            };
            if (this.Database.Provider.Server.Database != this.Database.Name) this.Database.Refresh();
            if(!this.Database.Tables.Contains(this.Name))
            {
                //this.Database.Provider.Server.Execute($"create table {this.Name} {};");
                this.Database.Refresh();
            }

            return response;
        }

        public DBResponse<bool> Delete(Expression<Func<T, bool>> filter)
        {
            var response = new DBResponse<bool>
            {
                HasValue = false
            };
            return response;
        }

        public DBResponse<bool> Drop()
        {
            var response = new DBResponse<bool>
            {
                HasValue = false
            };
            return response;
        }

        public DBResponse<T> Find(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public DBResponse<List<T>> FindAll(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public DBResponse<IDBTable<T, MySqlDatabase>> Refresh()
        {
            throw new NotImplementedException();
        }

        public DBResponse<bool> Update(Expression<Func<T, bool>> filter, T update)
        {
            var response = new DBResponse<bool>
            {
                HasValue = false
            };
            return response;
        }
    }
}
