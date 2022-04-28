using CQRS.Abstractions;
using System.Collections.Generic;

namespace CQRS
{
    public class CommandMediator : ICommandMediator
    {
        private readonly List<object> handlers = new List<object>();

        public void Register<TCommand>(ICommandHandler<TCommand> commandHandler)
            where TCommand : ICommand
        {
            handlers.Add(commandHandler);
        }

        public void Send<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            handlers.ForEach(handler => (handler as ICommandHandler<TCommand>).Handle(command));
        }
    }
}
