using CQRS.Abstractions;
using System.Collections.Generic;

namespace CQRS.Queries
{
    public class ListParticipants
    {
        public class Query : IQuery<IEnumerable<IParticipant>>
        {
            public Query(IChatRoom chatRoom)
            {
                ChatRoom = chatRoom;
            }

            public IChatRoom ChatRoom { get; }
        }

        public class Handler : IQueryHandler<Query, IEnumerable<IParticipant>>
        {
            public IEnumerable<IParticipant> Handle(Query query)
            {
                return query.ChatRoom.ListParticipants();
            }
        }
    }
}
