using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace RecreateMe.Sports
{
    public class SkillLevelProvider
    {
        public IList<string> GetListOfAvailableSkillLevels()
        {
            var skillLevels = ConfigurationManager.AppSettings[AppConfigConstants.SkillLevels];

            return skillLevels.Split(',').Select(x => x.Trim()).ToList();
        }
    }
}