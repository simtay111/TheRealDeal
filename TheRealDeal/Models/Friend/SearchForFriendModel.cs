using System.Collections.Generic;

namespace TheRealDeal.Models.Friend
{
    public class SearchForFriendModel
    {
        public string Location { get; set; }
        public string ProfileName { get; set; }
        public string Sport { get; set; }
        public IList<RecreateMe.Profiles.Profile> Results { get; set; }
    }
}