using CQRS.Abstractions;
using CQRS.Queries;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CQRS.Tests.QueryMediatorTests
{
    public class QueryMediatorTest
    {
        public class Send_ListParticipantsQuery : QueryMediatorTest
        {
            [Fact]
            public void ShouldSendSpecifiedQueryToRegisteredHandler()
            {
                // arrange
                var chatRoomMock = new Mock<IChatRoom>();
                var query = new ListParticipants.Query(chatRoomMock.Object);

                var participant1Mock = new Mock<IParticipant>();
                var participant2Mock = new Mock<IParticipant>();
                var participant3Mock = new Mock<IParticipant>();
                var participants = new List<IParticipant>()
                {
                    participant1Mock.Object,
                    participant2Mock.Object,
                    participant3Mock.Object
                };
                var queryHandlerMock = new Mock<IQueryHandler<ListParticipants.Query, IEnumerable<IParticipant>>>();
                queryHandlerMock.Setup(h => h.Handle(query)).Returns(participants);

                var sut = new QueryMediator();
                sut.Register(queryHandler: queryHandlerMock.Object);

                // act
                var result = sut.Send<ListParticipants.Query, IEnumerable<IParticipant>>(query);

                // arrange
                queryHandlerMock.Verify(h => h.Handle(query), Times.Once());
                Assert.Equal(participants, result);
            }
        }
    }
}
