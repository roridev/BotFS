using System;
using System.Collections.Generic;

namespace BotFS
{
    /// <summary>
    /// The base for all server providers
    /// </summary>
    /// <typeparam name="T">The class provided from a library [e.g. MongoDB.Driver] </typeparam>
    public interface BaseServerProvider<T>
    {
        public T Server { get; set; }
        public List<String> Databases { get; set; } 
        public ServerStatus Status { get; set; }
        public DBResponse<List<String>> Refresh();
    }

    public enum ServerStatus
    {
        CONNECTED,DISCONNECTED,INVALID
    }
}