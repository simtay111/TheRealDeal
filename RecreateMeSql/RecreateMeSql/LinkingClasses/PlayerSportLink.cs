using RecreateMe.Profiles;
using RecreateMe.Scheduling.Games;
using RecreateMe.Sports;
using ServiceStack.DataAnnotations;

namespace RecreateMeSql.LinkingClasses
{
    public class PlayerSportLink
    {
        [AutoIncrement]
        public int Id { get; set; }

        [References(typeof(Profile))]
        public string PlayerId { get; set; } 

        [References(typeof(Sport))]
        public string Sport { get; set; }

        public int Skill { get; set; }
    }
}