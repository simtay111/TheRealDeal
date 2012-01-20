namespace RecreateMe.Sports
{
    public class SkillLevel
    {
        public SkillLevel(int level)
        {
            Level = level == 0 ? Constants.DefaultSkillLevel : level;
        }

        public SkillLevel()
        {
            Level = Constants.DefaultSkillLevel;
        }

        public int Level { get; set; }
    }
}