using CQRS.Abstractions;
using CQRS.Commands;
using System.Collections.Generic;

namespace CQRS
{
    public class JoinCommandMediator
    {
        private readonly List<IJoinHandler> handlers = new List<IJoinHandler>();

        public void Register(IJoinHandler commandHandler)
        {
            handlers.Add(commandHandler);
        }

        public void Send(JoinChatRoom.Command command)
        {
            handlers.ForEach(handler => handler.Handle(command));
        }
    }
}
