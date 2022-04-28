using CQRS.Abstractions;

namespace CQRS.Commands
{
    public class JoinChatRoom
    {
        public class Command
        {
            public Command(IParticipant requester, IChatRoom chatRoom)
            {
                Requester = requester;
                ChatRoom = chatRoom;
            }

            public IParticipant Requester { get; }
            public IChatRoom ChatRoom { get; }
        }

        public class Handler : IJoinHandler
        {
            public void Handle(Command command)
            {
                command.ChatRoom.Add(command.Requester);
            }
        }
    }
}
