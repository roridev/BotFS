using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BotFS.Test
{
    public class MongoTable<T> : IDBTable<T>
    {
        public string Database { get;set; }
        public int Size { get;set; }

        public DBResponse<bool> Add(T value)
        {
            throw new NotImplementedException();
        }

        public DBResponse<bool> Delete(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public DBResponse<bool> Drop()
        {
            throw new NotImplementedException();
        }

        public DBResponse<T> Find(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public DBResponse<List<T>> FindAll(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public DBResponse<IDBTable<T>> Refresh()
        {
            throw new NotImplementedException();
        }

        public DBResponse<bool> Update(Expression<Func<T, bool>> filter, T update)
        {
            throw new NotImplementedException();
        }
    }
}
