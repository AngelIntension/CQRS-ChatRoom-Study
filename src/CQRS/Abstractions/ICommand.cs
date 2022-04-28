namespace CQRS.Abstractions
{
    public interface ICommand { }

    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        void Handle(TCommand command);
    }

    public interface ICommandMediator
    {
        void Register<TCommand>(ICommandHandler<TCommand> commandHandler)
            where TCommand : ICommand;
        void Send<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}