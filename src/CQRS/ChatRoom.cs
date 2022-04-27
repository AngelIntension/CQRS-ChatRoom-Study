using CQRS.Abstractions;
using System.Collections.Generic;

namespace CQRS
{
    public class ChatRoom : IChatRoom
    {
        private readonly List<IParticipant> participants = new List<IParticipant>();
        private readonly List<ChatMessage> messages = new List<ChatMessage>();

        public ChatRoom(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public void Add(IParticipant participant)
        {
            participants.Add(participant);
        }

        public IEnumerable<IParticipant> ListParticipants()
        {
            return participants.AsReadOnly();
        }

        public void Remove(IParticipant participant)
        {
            participants.Remove(participant);
        }

        public void Add(ChatMessage message)
        {
            messages.Add(message);
        }

        public IEnumerable<ChatMessage> ListMessages()
        {
            return messages.AsReadOnly();
        }
    }
}
