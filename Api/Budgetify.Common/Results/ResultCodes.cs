namespace Budgetify.Common.Results
{
    public static class ResultCodes
    {
        #region Common

        public const string EmailInvalid = "INVALID_EMAIL";
        public const string EmailInvalidLength = "EMAIL_INVALID_LENGTH";

        #endregion

        #region User

        public const string InvalidUserName = "INVALID_USER_NAME";
        public const string InvalidUserNameLength = "INVALID_USER_NAME_LENGTH";
        public const string UserNotFound = "USER_NOT_FOUND";

        #endregion
    }
}
