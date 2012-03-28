using System;
using RecreateMe.Locales;
using RecreateMe.Sports;

namespace RecreateMe.Scheduling.Games
{
    public interface IAmAGame
    {
        int? MaxPlayers { get; set; }
        int? MinPlayers { get; set; }
        Location Location { get; set; }
        Sport Sport { get; set; }
        DateTimeOffset DateTime { get; set; }
        string Id { get; set; }
        string Creator { get; set; }
        bool IsPrivate { get; set; }
    }
}