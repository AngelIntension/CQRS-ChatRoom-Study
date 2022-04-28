namespace CQRS.Abstractions
{
    public interface IQuery<TResult> { }

    public interface IQueryHandler<TQuery, TReturn>
        where TQuery : IQuery<TReturn>
    {
        TReturn Handle(TQuery query);
    }

    public interface IQueryMediator
    {
        void Register<TQuery, TReturn>(IQueryHandler<TQuery, TReturn> queryHandler)
            where TQuery : IQuery<TReturn>;

        TReturn Send<TQuery, TReturn>(TQuery query)
            where TQuery : IQuery<TReturn>;
    }
}
