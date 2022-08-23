namespace Budgetify.Common.Results
{
    public static class ResultCodes
    {
        #region Common

        public const string EmailInvalid = "EMAIL_INVALID";
        public const string EmailInvalidLength = "EMAIL_INVALID_LENGTH";

        #endregion

        #region User

        public const string UserNameInvalid = "USER_NAME_INVALID";
        public const string UserNameInvalidLength = "USER_NAME_INVALID_LENGTH";
        public const string UserNotFound = "USER_NOT_FOUND";
        public const string CityInvalid = "CITY_INVALID";
        public const string CityInvalidLength = "CITY_INVALID_LENGTH";

        #endregion
    }
}
