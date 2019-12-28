using System.Collections.Generic;
using System.Dynamic;
namespace BotFS
{
   /// <summary>
    /// A Generic Database
    /// </summary>
    /// <typeparam name="T"> The provider class</typeparam>
    public interface Database<T,A> where T : BaseServerProvider<A>
    {
        /// <summary>
        /// The name of the database
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The Server that said database is stored.
        /// </summary>
        public BaseServerProvider<A> Server { get; set; }
         /// <summary>
        /// The quantity of database tables in the database.
        /// </summary>
        public int Size { get; set; }
         /// <summary>
        /// A list containing the names of all database tables.
        /// </summary>
        public List<string> Tables {get;set;}
    }
}