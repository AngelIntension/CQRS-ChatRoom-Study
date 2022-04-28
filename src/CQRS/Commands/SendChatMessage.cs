using CQRS.Abstractions;

namespace CQRS.Commands
{
    public class SendChatMessage
    {
        public class Command : ICommand
        {
            public Command(IChatRoom chatRoom, ChatMessage message)
            {
                ChatRoom = chatRoom;
                Message = message;
            }

            public IChatRoom ChatRoom { get; }
            public ChatMessage Message { get; }
        }

        public class Handler : ICommandHandler<Command>
        {
            public void Handle(Command command)
            {
                var chatRoom = command.ChatRoom;
                chatRoom.Add(command.Message);
                foreach (var participant in chatRoom.ListParticipants())
                {
                    participant.NewMessageReceivedFrom(chatRoom, command.Message);
                }
            }
        }
    }
}
