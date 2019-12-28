using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BotFS.MongoDB
{

    public class MongoTable<T> : IDBTable<T,MongoDatabase>
    {
        public MongoDatabase Database { get;set; }
        public long Size { get;set; }
        public string Name { get; set; }

        public DBResponse<bool> Add(T value)
        {
            var response = new DBResponse<bool>
            {
                HasValue = false
            };
            if (this.Database.Tables == null) this.Database.Refresh();
            try
            {
                this.Database.Provider.Server
                .GetDatabase(this.Database.Name)
                .GetCollection<T>(this.Name)
                .InsertOne(value);
                response.HasValue = true;
                response.Response = true;
                return response;
            }
            catch(MongoWriteException)
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
                this.Database.Provider.Server
                .GetDatabase(this.Database.Name)
                .GetCollection<T>(this.Name)
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
                this.Database.Provider.Server
                .GetDatabase(this.Database.Name)
                .DropCollection(this.Name);
                response.HasValue = true;
                response.Response = true;
                this.Database.Refresh();
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
                response.Response = this.Database.Provider.Server
                .GetDatabase(this.Database.Name)
                .GetCollection<T>(this.Name)
                .Find<T>(filter).First();
                return response;
            }
            catch(MongoException)
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
                response.Response = this.Database.Provider.Server
                .GetDatabase(this.Database.Name)
                .GetCollection<T>(this.Name)
                .Find<T>(filter).ToList();
                return response;
            }
            catch (MongoException)
            {
                return response;
            }
        }

        public DBResponse<IDBTable<T,MongoDatabase>> Refresh()
        {
            var response = new DBResponse<IDBTable<T,MongoDatabase>>
            {
                HasValue = true,
                Response = this.Database.Provider.Server
                    .GetDatabase(this.Database.Name)
                    .GetCollection<T>(this.Name).ToBFS<T>(this.Database)
            };
            this.Size = response.Response.Size;
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
                this.Database.Provider.Server
                .GetDatabase(this.Database.Name)
                .GetCollection<T>(this.Name)
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
        public static implicit operator MongoTable<T>(DBResponse<MongoTable<T>> o)
        {
            return o.Response;
        }
    }
}
