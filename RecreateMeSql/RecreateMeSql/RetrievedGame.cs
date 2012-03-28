using System;
using RecreateMe.Locales;
using RecreateMe.Sports;

namespace RecreateMeSql
{
    //Used b/c graphClient can't handle the abstract game class
    public class RetrievedGame
    {
        public string Id { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public Sport Sport { get; set; }
        public Location Location { get; set; }
        public int? MinPlayers { get; set; }
        public int? MaxPlayers { get; set; }
        public bool IsPrivate { get; set; }
        public string Creator { get; set; }
    }
}