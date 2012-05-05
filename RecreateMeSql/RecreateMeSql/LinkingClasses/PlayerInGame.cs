using RecreateMe.Profiles;
using RecreateMe.Scheduling.Games;
using ServiceStack.DataAnnotations;

namespace RecreateMeSql.LinkingClasses
{
    public class PlayerInGame
    {
        [AutoIncrement]
        public int Id { get; set; }

        [References(typeof(Profile))]
        public string PlayerId { get; set; } 

        [References(typeof(PickUpGame))]
        public string GameId { get; set; } 
    }
}