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
    public const string AccountInvalidForDeletion = "ACCOUNT_INVALID_FOR_DELETION";

    #endregion

    #region Category

    public const string CategoryNameInvalid = "CATEGORY_NAME_INVALID";
    public const string CategoryNameInvalidLength = "CATEGORY_NAME_INVALID_LENGTH";
    public const string CategoryTypeInvalid = "CATEGORY_TYPE_INVALID";
    public const string CategoryNotFound = "CATEGORY_NOT_FOUND";
    public const string CategoryWithSameNameAlreadyExist = "CATEGORY_WITH_SAME_NAME_ALREADY_EXIST";
    public const string CategoryInvalidForDeletion = "CATEGORY_INVALID_FOR_DELETION";

    #endregion

    #region Merchant

    public const string MerchantNameInvalid = "MERCHANT_NAME_INVALID";
    public const string MerchantNameInvalidLength = "MERCHANT_NAME_INVALID_LENGTH";
    public const string MerchantNotFound = "MERCHANT_NOT_FOUND";
    public const string MerchantWithSameNameAlreadyExist = "MERCHANT_WITH_SAME_NAME_ALREADY_EXIST";
    public const string MerchantInvalidForDeletion = "MERCHANT_INVALID_FOR_DELETION";
    public const string MerchantCategoryTypeInvalid = "MERCHANT_CATEGORY_TYPE_INVALID";

    #endregion

    #region Transaction

    public const string TransactionTypeInvalid = "TRANSACTION_TYPE_INVALID";
    public const string TransactionEmptyMerchantTypeInvalid = "TRANSACTION_EMPTY_MERCHANT_TYPE_INVALID";
    public const string TransactionNotFound = "TRANSACTION_NOT_FOUND";
    public const string TransactionNotVerifiedCannotUpdateAccountBalance = "TRANSACTION_NOT_VERIFIED_CANNOT_UPDATE_ACCOUNT_BALANCE";
    public const string TransactionNotVerifiedCannotUpdateBudgetAmountSpent = "TRANSACTION_NOT_VERIFIED_CANNOT_UPDATE_BUDGET_AMOUNT_SPENT";
    public const string TransactionInvalidForVerification = "TRANSACTION_INVALID_FOR_VERIFICATION";
    public const string TransactionTypeAndCategoryMismatch = "TRANSACTION_TYPE_AND_CATEGORY_MISMATCH";
    public const string TransactionTypeNotCompatibleWithMerchant = "TRANSACTION_TYPE_NOT_COMPATIBLE_WITH_MERCHANT";
    public const string TransactionTypeTransferMissingAccounts = "TRANSACTION_TYPE_TRANSFER_MISSING_ACCOUNTS";
    public const string TransactionCategoryMissing = "TRANSACTION_CATEGORY_MISSING";
    public const string TransactionTypeTransferCannotHaveCategory = "TRANSACTION_TYPE_TRANSFER_CANNOT_HAVE_CATEGORY";
    public const string TransactionTypeTransferCannotHaveEqualAccounts = "TRANSACTION_TYPE_TRANSFER_CANNOT_HAVE_EQUAL_ACCOUNTS";

    #endregion

    #region Exchange Rate

    public const string ExchangeRateFromAndToCurrencyCannotBeEqual = "EXCHANGE_RATE_FROM_AND_TO_CURRENCY_CANNOT_BE_EQUAL";
    public const string ExchangeRateFromDateCannotBeGreaterThanToDate = "EXCHANGE_RATE_FROM_DATE_CANNOT_BE_GREATER_THAN_TO_DATE";
    public const string ExchangeRateNotFound = "EXCHANGE_RATE_NOT_FOUND";
    public const string ExchangeRateExistsFromDateCannotBeEmpty = "EXCHANGE_RATE_EXISTS_FROM_DATE_CANNOT_BE_EMPTY";
    public const string ExchangeRateClosed = "EXCHANGE_RATE_CLOSED";

    #endregion

    #region Transaction Attachment

    public const string TransactionAttachmentFilePathInvalid = "TRANSACTION_ATTACHMENT_FILE_PATH_INVALID";
    public const string TransactionAttachmentNameInvalid = "TRANSACTION_ATTACHMENT_NAME_INVALID";
    public const string TransactionAttachmentNameInvalidLength = "TRANSACTION_ATTACHMENT_NAME_INVALID_LENGTH";
    public const string TransactionAttachmentNotFound = "TRANSACTION_ATTACHMENT_NOT_FOUND";

    #endregion

    #region Budget

    public const string BudgetNameInvalid = "BUDGET_NAME_INVALID";
    public const string BudgetNameInvalidLength = "BUDGET_NAME_INVALID_LENGTH";
    public const string BudgetWithSameNameAlreadyExist = "BUDGET_WITH_SAME_NAME_ALREADY_EXIST";
    public const string BudgetStartDateCannotBeGreaterThanEndDate = "BUDGET_START_DATE_CANNOT_BE_GREATER_THAN_END_DATE";
    public const string BudgetNotFound = "BUDGET_NOT_FOUND";
    public const string BudgetCategoryTypeInvalid = "BUDGET_CATEGORY_TYPE_INVALID";

    #endregion
}
