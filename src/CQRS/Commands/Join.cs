using CQRS.Abstractions;

namespace CQRS.Commands
{
    public class Join
    {
        public class Command
        {
            public Command(IParticipant sender, IChatRoom chatRoom)
            {
                Sender = sender;
                ChatRoom = chatRoom;
            }

            public IParticipant Sender { get; }
            public IChatRoom ChatRoom { get; }
        }
    }
}
