using CQRS.Queries;

namespace CQRS.Abstractions
{
    public interface IQueryHandler<TQuery, TReturn>
        where TQuery : IQuery<TReturn>
    {
        TReturn Handle(TQuery query);
    }
}
