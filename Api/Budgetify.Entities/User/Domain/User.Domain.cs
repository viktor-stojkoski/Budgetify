namespace Budgetify.Entities.User.Domain
{
    using Budgetify.Common.Results;
    using Budgetify.Entities.User.ValueObjects;

    public partial class User
    {
        /// <summary>
        /// Updates User.
        /// </summary>
        public Result Update(string? name, string? email)
        {
            Result<UserNameValue> nameValue = UserNameValue.Create(name);
            Result<EmailValue> emailValue = EmailValue.Create(email);

            Result firstFailureNullOrOk = Result.FirstFailureNullOrOk(nameValue, emailValue);

            if (firstFailureNullOrOk.IsFailureOrNull)
            {
                return firstFailureNullOrOk;
            }

            Name = nameValue.Value;
            Email = emailValue.Value;

            MarkModify();

            return Result.Ok();
        }
    }
}
