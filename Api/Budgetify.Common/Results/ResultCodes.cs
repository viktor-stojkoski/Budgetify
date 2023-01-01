namespace Budgetify.Common.Results;

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
    public const string UserAlreadyExists = "USER_ALREADY_EXISTS";

    #endregion

    #region Currency

    public const string CurrencyNameInvalid = "CURRENCY_NAME_INVALID";
    public const string CurrencyNameInvalidLength = "CURRENCY_NAME_INVALID_LENGTH";
    public const string CurrencyCodeInvalid = "CURRENCY_CODE_INVALID";
    public const string CurrencySymbolInvalidLength = "CURRENCY_SYMBOL_INVALID_LENGTH";
    public const string CurrencyNotFound = "CURRENCY_NOT_FOUND";

    #endregion

    #region Account

    public const string AccountNameInvalid = "ACCOUNT_NAME_INVALID";
    public const string AccountNameInvalidLength = "ACCOUNT_NAME_INVALID_LENGTH";
    public const string AccountTypeInvalid = "ACCOUNT_TYPE_INVALID";
    public const string AccountNotFound = "ACCOUNT_NOT_FOUND";
    public const string AccountWithSameNameAlreadyExist = "ACCOUNT_WITH_SAME_NAME_ALREADY_EXIST";

    #endregion

    #region Category

    public const string CategoryNameInvalid = "CATEGORY_NAME_INVALID";
    public const string CategoryNameInvalidLength = "CATEGORY_NAME_INVALID_LENGTH";
    public const string CategoryTypeInvalid = "CATEGORY_TYPE_INVALID";
    public const string CategoryNotFound = "CATEGORY_NOT_FOUND";
    public const string CategoryWithSameNameAlreadyExist = "CATEGORY_WITH_SAME_NAME_ALREADY_EXIST";

    #endregion

    #region Merchant

    public const string MerchantNameInvalid = "MERCHANT_NAME_INVALID";
    public const string MerchantNameInvalidLength = "MERCHANT_NAME_INVALID_LENGTH";
    public const string MerchantNotFound = "MERCHANT_NOT_FOUND";
    public const string MerchantWithSameNameAlreadyExist = "MERCHANT_WITH_SAME_NAME_ALREADY_EXIST";

    #endregion

    #region Transaction

    public const string TransactionTypeInvalid = "TRANSACTION_TYPE_INVALID";
    public const string TransactionEmptyMerchantTypeInvalid = "TRANSACTION_EMPTY_MERCHANT_TYPE_INVALID";
    public const string TransactionNotFound = "TRANSACTION_NOT_FOUND";

    #endregion

    #region Exchange Rate

    public const string ExchangeRateFromAndToCurrencyCannotBeEqual = "EXCHANGE_RATE_FROM_AND_TO_CURRENCY_CANNOT_BE_EQUAL";
    public const string ExchangeRateFromDateCannotBeGreaterThanToDate = "EXCHANGE_RATE_FROM_DATE_CANNOT_BE_GREATER_THAN_TO_DATE";
    public const string ExchangeRateNotFound = "EXCHANGE_RATE_NOT_FOUND";
    public const string ExchangeRateExistsFromDateCannotBeEmpty = "EXCHANGE_RATE_EXISTS_FROM_DATE_CANNOT_BE_EMPTY";
    public const string ExchangeRateClosed = "EXCHANGE_RATE_CLOSED";

    #endregion
}
