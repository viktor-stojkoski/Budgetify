namespace Budgetify.Common.Commands
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;

    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [DisplayName("Command: {0}")]
        public async Task<CommandResult<TValue>> ExecuteAsync<TValue>(ICommand<TValue> command)
        {
            Type? handlerType = typeof(ICommandHandler<,>)
                .MakeGenericType(command.GetType(), typeof(TValue));

            dynamic? handler = _serviceProvider.GetService(handlerType);

            return await handler?.ExecuteAsync((dynamic)command);
        }
    }
}
