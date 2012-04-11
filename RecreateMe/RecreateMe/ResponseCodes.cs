using System;

namespace RecreateMe
{
    public enum ResponseCodes
    {
        Success,
        Failure,
        BadPasswordLength,
        UserAlreadyExists,
        BadUserNameFormat,
        LocationNotSpecified,
        SportNotSpecified,
        NameNotSpecified,
        NoResultsFound,
        CannotHaveTeams,
        CouldNotParseDate,
        DateNotSpecified,
        OnlyTeamsCanJoin,
        NoCriteriaSpecified,
        FieldsAreBlank,
        PasswordsDontMatch,
        MaxProfilesReached,
        LocationNotFound,
        ProfileNameAlreadyExists,
        SportAlreadyPlayed,
        LocationAlreadyInProfile,
        AlreadyFriend,
        ProfileIdRequired,
        AlreadyInGame,
        GameIsFull,
        NotCreator,
        AlreadyInLeague,
        DuplicateEntryFound
    }

    public static class ResponseCodeExtensions
    {
        public static string GetMessage(this ResponseCodes responseCode)
        {
            var message = string.Empty;

            switch (responseCode)
            {
                case (ResponseCodes.Failure):
                    return "Major failure occured. BOOOOOOM";
                case (ResponseCodes.FieldsAreBlank):
                    return "One or more of the fields was left blank.";
                case (ResponseCodes.BadUserNameFormat):
                    return "Bad Login Name format: ex: Moo@Moo.com";
                case (ResponseCodes.UserAlreadyExists):
                    return "User already exists with that name.";
                case (ResponseCodes.BadPasswordLength):
                    return String.Format("The password was the incorrect size. Min Length: {0} Max Length {1} ",
                                         Constants.MinPasswordLength, Constants.MaxPasswordLength);
                case (ResponseCodes.PasswordsDontMatch):
                    return "The passwords do not match. Try again";
                case (ResponseCodes.MaxProfilesReached):
                    return "You have reached your maximum amount of profiles for this account";
                case(ResponseCodes.NameNotSpecified):
                    return "You must specify a name for the account";
                case (ResponseCodes.LocationNotFound):
                    return "The location you specified does not exist. Boo!";
                case (ResponseCodes.LocationAlreadyInProfile):
                    return "You already have this location in your profile dummy...";
                case (ResponseCodes.AlreadyInGame):
                    return "You already are a part of this game dummy...";
            }

            return message;
        }
    }
}