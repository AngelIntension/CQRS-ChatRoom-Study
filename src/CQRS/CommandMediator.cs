using CQRS.Abstractions;
using System;
using System.Collections.Generic;

namespace CQRS
{
    public class CommandMediator : ICommandMediator
    {
        private readonly Dictionary<Type, List<object>> handlers = new Dictionary<Type, List<object>>();

        public void Register<TCommand>(ICommandHandler<TCommand> commandHandler)
            where TCommand : ICommand
        {
            var type = typeof(TCommand);
            EnforceTypeEntry(type);
            var registeredHandlers = handlers[type];
            registeredHandlers.Add(commandHandler);
        }

        public void Send<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            var type = typeof(TCommand);
            EnforceTypeEntry(type);
            var registeredHandlers = handlers[type];
            registeredHandlers.ForEach(handler => (handler as ICommandHandler<TCommand>).Handle(command));
        }

        private void EnforceTypeEntry(Type type)
        {
            if (!handlers.ContainsKey(type))
            {
                handlers.Add(type, new List<object>());
            }
        }
    }
}
