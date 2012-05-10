using RecreateMe.Profiles;
using RecreateMe.Scheduling.Games;
using ServiceStack.DataAnnotations;

namespace RecreateMeSql.LinkingClasses
{
    public class PlayerInGameLink
    {
        [AutoIncrement]
        public int Id { get; set; }

        [References(typeof(Profile))]
        [Index]
        public string PlayerId { get; set; } 

        [References(typeof(PickUpGame))]
        [Index]
        public string GameId { get; set; } 
    }
}