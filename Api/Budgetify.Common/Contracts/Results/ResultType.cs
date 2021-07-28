namespace Budgetify.Common.Contracts.Results
{
    public enum ResultType
    {
        InternalError = 0,
        Ok = 1,
        NotFound = 2,
        Forbidden = 3,
        Conflicted = 4,
        Invalid = 5,
        Unauthorized = 6
    }
}
