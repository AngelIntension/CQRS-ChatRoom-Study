using CQRS.Abstractions;
using System.Collections.Generic;

namespace CQRS.Queries
{
    public class ListParticipants
    {
        public class Query : IQuery<IEnumerable<IParticipant>>
        {

        }
    }
}
