using CQRS.Commands;

namespace CQRS.Abstractions
{
    public interface IJoinCommandMediator
    {
        void Register(IJoinHandler commandHandler);
        void Send(JoinChatRoom.Command command);
    }
}