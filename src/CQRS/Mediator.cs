using CQRS.Abstractions;

namespace CQRS
{
    public class Mediator : IMediator
    {
        private readonly CommandMediator commandMediator = new CommandMediator();
        private readonly QueryMediator queryMediator = new QueryMediator();

        public void Register<TCommand>(ICommandHandler<TCommand> handler)
            where TCommand : ICommand
        {
            commandMediator.Register(handler);
        }

        public void Register<TQuery, TResult>(IQueryHandler<TQuery, TResult> handler)
            where TQuery : IQuery<TResult>
        {
            queryMediator.Register(handler);
        }

        public void Send<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            commandMediator.Send(command);
        }

        public TReturn Send<TQuery, TReturn>(TQuery query)
            where TQuery : IQuery<TReturn>
        {
            return queryMediator.Send<TQuery, TReturn>(query);
        }
    }
}
