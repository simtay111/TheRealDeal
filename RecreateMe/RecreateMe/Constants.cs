namespace RecreateMe
{
    public static class Constants
    {
        public const int MaxNumberOfProfilesPerAccount = 3;
        public const int MaxAmountOfTeamsPerGame = 2;
        public const string DefaultTeamName = "My Team";
        public const string DefaultLocationName = "Not Specified";
        public const int DefaultMinNumberOfPlayers = 2;
        public const int DefaultSkillLevel = 1;
        public const int DefaultTeamSize = 1;
        public const int MinPasswordLength = 8;
        public const int MaxPasswordLength = 14;
        public const int MaxAmountOfCreatedGames = 3;
        public const int MaxAmountOfGamesPlayingIn = 4;
        public const string UserNameRegex = @"^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$";
    }
}