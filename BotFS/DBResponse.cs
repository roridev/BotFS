namespace BotFS
{
    public class DBResponse<T>
    {
        public bool HasValue { get; set; }
        public T Response { get; set; }
    }
}