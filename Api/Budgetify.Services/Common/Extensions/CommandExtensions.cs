namespace Budgetify.Services.Common.Extensions;

using Budgetify.Common.Results;

using VS.Commands;

public static class CommandExtensions
{
    public static CommandResult<TValue> FailWith<TValue>(
        this CommandResultBuilder<TValue> commandResultBuilder,
        Result result)
    {
        commandResultBuilder.SetMessage(result.Message);
        commandResultBuilder.SetStatusCode(result.HttpStatusCode);

        return commandResultBuilder.Build();
    }
}
