﻿using System;

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
        PasswordsDontMatch
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

            }

            return message;
        }
    }
}