using CQRS.Commands;

namespace CQRS.Abstractions
{
    public interface IJoinHandler
    {
        void Handle(Join.Command command);
    }
}
