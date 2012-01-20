namespace RecreateMe.Sports
{
    public class SportWithSkillLevel : Sport
    {
        public SportWithSkillLevel()
        {
            SkillLevel = new SkillLevel();
        }

        public SkillLevel SkillLevel { get; set; }
    }
}