using CQRS.Abstractions;
using System;
using System.Collections.Generic;

namespace CQRS
{
    public class QueryMediator
    {
        private readonly Dictionary<Type, object> handlers = new Dictionary<Type, object>();

        public void Register<TQuery, TReturn>(IQueryHandler<TQuery, TReturn> queryHandler)
            where TQuery : IQuery<TReturn>
        {
            var type = typeof(TQuery);
            handlers[type] = queryHandler;
        }

        public TReturn Send<TQuery, TReturn>(TQuery query)
            where TQuery : IQuery<TReturn>
        {
            var type = typeof(TQuery);
            var handler = handlers[type];
            return (handler as IQueryHandler<TQuery, TReturn>).Handle(query);
        }
    }
}
