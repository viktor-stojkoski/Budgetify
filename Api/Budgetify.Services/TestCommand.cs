namespace Budgetify.Services.Test
{
    using System.Threading.Tasks;

    using Budgetify.Common.Commands;

    public record TestCommand(string Value) : ICommand<string>;

    public class TestCommandHandler : ICommandHandler<TestCommand, string>
    {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<CommandResult<string>> ExecuteAsync(TestCommand command)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            CommandResultBuilder<string> result = new();

            return result.FailWith("GRESKA");

            //result.SetValue(command.Value);

            //return result.Build();
        }
    }
}
