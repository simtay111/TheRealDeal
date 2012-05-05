using RecreateMe.Locales;
using RecreateMe.Profiles;
using RecreateMe.Scheduling.Games;
using RecreateMe.Sports;
using ServiceStack.DataAnnotations;

namespace RecreateMeSql.LinkingClasses
{
    public class PlayerLocationLink
    {
        [AutoIncrement]
        public int Id { get; set; }

        [References(typeof(Profile))]
        public string PlayerId { get; set; } 

        [References(typeof(Location))]
        public string Location { get; set; }
    }
}