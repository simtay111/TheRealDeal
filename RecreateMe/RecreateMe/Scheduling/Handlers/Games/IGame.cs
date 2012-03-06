using System;
using Newtonsoft.Json;
using RecreateMe.Locales;
using RecreateMe.Sports;

namespace RecreateMe.Scheduling.Handlers.Games
{
    public interface IGame
    {
        string Id { get; set; }
        DateTimeOffset DateTime { get; set; }

        [JsonIgnore]
        Sport Sport { get; set; }

        [JsonIgnore]
        Location Location { get; set; }

        int? MinPlayers { get; set; }
        int? MaxPlayers { get; set; }
        bool IsPrivate { get; set; }
        bool HasTeams { get; set; }
        string Creator { get; set; }
    }
}