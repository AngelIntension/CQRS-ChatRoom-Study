namespace CQRS.Abstractions
{
    public interface IMediator
    {
        void Register<TCommand>(ICommandHandler<TCommand> handler)
            where TCommand : ICommand;

        void Register<TQuery, TResult>(IQueryHandler<TQuery, TResult> handler)
            where TQuery : IQuery<TResult>;

        void Send<TCommand>(TCommand command)
            where TCommand : ICommand;

        TReturn Send<TQuery, TReturn>(TQuery query)
            where TQuery : IQuery<TReturn>;
    }
}