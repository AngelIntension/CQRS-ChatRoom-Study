namespace CQRS.Abstractions
{
    public interface IQueryMediator
    {
        void Register<TQuery, TReturn>(IQueryHandler<TQuery, TReturn> queryHandler)
            where TQuery : IQuery<TReturn>;

        TReturn Send<TQuery, TReturn>(TQuery query)
            where TQuery : IQuery<TReturn>;
    }
}