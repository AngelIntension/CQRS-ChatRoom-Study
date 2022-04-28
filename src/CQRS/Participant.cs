using CQRS.Abstractions;
using CQRS.Commands;
using System.Collections.Generic;

namespace CQRS
{
    public class Participant : IParticipant
    {
        private IMessageWriter messageWriter;
        private ICommandMediator commandMediator;

        public Participant(ICommandMediator commandMediator, IMessageWriter messageWriter)
        {
            this.messageWriter = messageWriter;
            this.commandMediator = commandMediator;
        }

        public void Join(IChatRoom chatRoom)
        {
            commandMediator.Send(new JoinChatRoom.Command(this, chatRoom));
        }

        public void Leave(IChatRoom chatRoom)
        {
            commandMediator.Send(new LeaveChatRoom.Command(this, chatRoom));
        }

        public void SendMessageTo(IChatRoom chatRoom, string message)
        {
            var chatMessage = new ChatMessage(this, message);
            commandMediator.Send(new SendChatMessage.Command(chatRoom, chatMessage));
        }

        public void NewMessageReceivedFrom(IChatRoom chatRoom, ChatMessage message)
        {
            messageWriter.Write(chatRoom, message);
        }

        public IEnumerable<IParticipant> ListParticipantsOf(IChatRoom chatRoom)
        {
            return chatRoom.ListParticipants();
        }

        public IEnumerable<ChatMessage> ListMessagesOf(IChatRoom chatRoom)
        {
            return chatRoom.ListMessages();
        }
    }
}
