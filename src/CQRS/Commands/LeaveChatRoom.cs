using CQRS.Abstractions;

namespace CQRS.Commands
{
    public class LeaveChatRoom
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
                command.ChatRoom.Remove(command.Requester);
            }
        }
    }
}
