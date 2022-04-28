using CQRS.Abstractions;

namespace CQRS.Commands
{
    public class JoinChatRoom
    {
        public class Command : ICommand
        {
            public Command(IParticipant requester, IChatRoom chatRoom)
            {
                Requester = requester;
                ChatRoom = chatRoom;
            }

            public IParticipant Requester { get; }
            public IChatRoom ChatRoom { get; }
        }

        public class Handler : ICommandHandler<Command>
        {
            public void Handle(Command command)
            {
                command.ChatRoom.Add(command.Requester);
            }
        }
    }
}
