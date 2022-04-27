using System.Collections.Generic;

namespace CQRS.Abstractions
{
    public interface IParticipant
    {
        void Join(IChatRoom chatRoom);
        void Leave(IChatRoom chatRoom);
        IEnumerable<ChatMessage> ListMessagesOf(IChatRoom chatRoom);
        IEnumerable<IParticipant> ListParticipantsOf(IChatRoom chatRoom);
        void NewMessageReceivedFrom(IChatRoom chatRoom, ChatMessage message);
        void SendMessageTo(IChatRoom chatRoom, string message);
    }
}