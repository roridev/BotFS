namespace BotFS
{
    /// <summary>
    /// A Server Provider [e.g MongoDB, Postgres, MariaDB]
    /// </summary>
    /// <typeparam name="T"> The provider class</typeparam>
    public interface IServerProvider<T,A> where T : BaseServerProvider<A>
    {
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public BaseServerProvider<A> Provider {get;set;}
        public DBResponse<BaseServerProvider<A>> TryConnect();
        public DBResponse<Database<T,A>> GetDatabase(string Name);
    }
}