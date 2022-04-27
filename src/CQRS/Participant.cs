using CQRS.Abstractions;
using System.Collections.Generic;

namespace CQRS
{
    public class Participant : IParticipant
    {
        private IMessageWriter messageWriter;

        public Participant(IMessageWriter messageWriter)
        {
            this.messageWriter = messageWriter;
        }

        public void Join(IChatRoom chatRoom)
        {
            chatRoom.Add(this);
        }

        public void Leave(IChatRoom chatRoom)
        {
            chatRoom.Remove(this);
        }

        public void SendMessageTo(IChatRoom chatRoom, string message)
        {
            chatRoom.Add(new ChatMessage(this, message));
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
