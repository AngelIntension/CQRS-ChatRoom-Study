using System.Collections.Generic;

namespace CQRS.Abstractions
{
    public interface IChatRoom
    {
        string Name { get; }

        void Add(ChatMessage message);
        IEnumerable<ChatMessage> ListMessages();

        void Add(IParticipant participant);
        void Remove(IParticipant participant);
        IEnumerable<IParticipant> ListParticipants();
    }
}