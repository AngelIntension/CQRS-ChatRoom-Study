namespace CQRS.Abstractions
{
    public interface IMessageWriter
    {
        void Write(IChatRoom chatRoom, ChatMessage message);
    }
}
