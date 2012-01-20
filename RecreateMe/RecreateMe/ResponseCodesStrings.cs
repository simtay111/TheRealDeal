using System;

namespace RecreateMe
{
    public static class ResponseCodesStrings
    {
         public static string GetStringReponseForResponseCode(ResponseCodes code)
         {
             var message = string.Empty;

             switch (code)
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