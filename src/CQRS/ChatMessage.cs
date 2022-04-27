using CQRS.Abstractions;

namespace CQRS
{
    public class ChatMessage
    {
        public ChatMessage(IParticipant sender, string content)
        {
            Sender = sender;
            Content = content;
        }

        public IParticipant Sender { get; }
        public string Content { get; }
    }
}
