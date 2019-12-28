using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BotFS
{
    /// <summary>
    /// A Generic Database Table [E.g. MongoCollection, SqlTable]
    /// </summary>
    /// <typeparam name="T">
    /// The type of that table (POCO)
    /// </typeparam>
    /// <typeparam name="Db">
    /// The database type of said table
    /// </typeparam>
    public interface IDBTable<T,Db> 
    {
         /// <summary>
        /// The database the table is stored.
        /// </summary>
        public Db Database { get; set; }
        /// <summary>
        /// The name of the table.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The quantity of database objects in said table.
        /// </summary>
        public long Size { get; set; }
        /// <summary>
        /// Finds a database object that matches a filter
        /// </summary>
        /// <param name="filter">The filter to be checked on the database.</param>
        public DBResponse<T> Find(Expression<Func<T, bool>> filter);
        /// <summary>
        /// Finds all database objects that matches a filter
        /// </summary>
        /// <param name="filter">The filter to be checked on the database.</param>
        public DBResponse<List<T>> FindAll(Expression<Func<T, bool>> filter);
        /// <summary>
        /// Adds a object on the database.
        /// </summary>
        /// <param name="value">The POCO object to be added.</param>
        public DBResponse<bool> Add(T value);
        /// <summary>
        /// Updates a database object.
        /// </summary>
        /// <param name="filter">The filter to be checked on the database.</param>
        /// <param name="update">The POCO object to be changed.</param>
        public DBResponse<bool> Update(Expression<Func<T, bool>> filter, T update);
        /// <summary>
        /// Deletes a database object that matches a filter.
        /// </summary>
        /// <param name="filter">The filter to be checked on the database.</param>
        public DBResponse<bool> Delete(Expression<Func<T, bool>> filter);
        /// <summary>
        /// Refresh the database.
        /// </summary>
        public DBResponse<IDBTable<T,Db>> Refresh();
        /// <summary>
        /// Drop the database.
        /// </summary>
        public DBResponse<bool> Drop();
    }
}