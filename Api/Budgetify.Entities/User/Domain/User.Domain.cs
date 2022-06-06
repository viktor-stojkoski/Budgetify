namespace Budgetify.Entities.User.Domain
{
    using Budgetify.Common.Results;
    using Budgetify.Entities.User.ValueObjects;

    public partial class User
    {
        /// <summary>
        /// Updates user.
        /// </summary>
        public Result Update(string? name, string? email)
        {
            Result<UserNameValue> nameValue = UserNameValue.Create(name);
            Result<EmailValue> emailValue = EmailValue.Create(email);

            Result okOrError = Result.FirstFailureNullOrOk(nameValue, emailValue);

            if (okOrError.IsFailureOrNull)
            {
                return okOrError;
            }

            Name = nameValue.Value;
            Email = emailValue.Value;

            MarkModify();

            return Result.Ok();
        }
    }
}
