using CQRS.Abstractions;
using System.Collections.Generic;

namespace CQRS.Queries
{
    public class ListMessages
    {
        public class Query : IQuery<IEnumerable<ChatMessage>>
        {
            public Query(IChatRoom chatRoom)
            {
                ChatRoom = chatRoom;
            }

            public IChatRoom ChatRoom { get; }
        }

        public class Handler : IQueryHandler<Query, IEnumerable<ChatMessage>>
        {
            public IEnumerable<ChatMessage> Handle(Query query)
            {
                return query.ChatRoom.ListMessages();
            }
        }
    }
}
