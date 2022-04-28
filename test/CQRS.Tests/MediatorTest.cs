using CQRS.Abstractions;
using CQRS.Commands;
using CQRS.Queries;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CQRS.Tests.MediatorTests
{
    public class MediatorTest
    {
        public class Send_Command : MediatorTest
        {
            [Fact]
            public void ShouldSendSpecifiedCommandToAllRegisteredHandlers()
            {
                // arrange
                var requesterMock = new Mock<IParticipant>();
                var chatRoomMock = new Mock<IChatRoom>();
                var command = new JoinChatRoom.Command(requesterMock.Object, chatRoomMock.Object);

                var commandHandlerMock1 = new Mock<ICommandHandler<JoinChatRoom.Command>>();
                var commandHandlerMock2 = new Mock<ICommandHandler<JoinChatRoom.Command>>();
                var commandHandlerMock3 = new Mock<ICommandHandler<JoinChatRoom.Command>>();

                var sut = new Mediator();
                sut.Register(handler: commandHandlerMock1.Object);
                sut.Register(handler: commandHandlerMock2.Object);
                sut.Register(handler: commandHandlerMock3.Object);

                // act
                sut.Send(command: command);

                // assert
                commandHandlerMock1.Verify(h => h.Handle(command), Times.Once());
                commandHandlerMock2.Verify(h => h.Handle(command), Times.Once());
                commandHandlerMock3.Verify(h => h.Handle(command), Times.Once());
            }
        }

        public class Send_Query : MediatorTest
        {
            [Fact]
            public void ShouldSendSpecifiedQueryToRegisteredHandler()
            {
                // arrange
                var chatRoomMock = new Mock<IChatRoom>();
                var query = new ListParticipants.Query(chatRoomMock.Object);

                var participantMock1 = new Mock<IParticipant>();
                var participantMock2 = new Mock<IParticipant>();
                var participantMock3 = new Mock<IParticipant>();
                var participants = new List<IParticipant>()
                {
                    participantMock1.Object,
                    participantMock2.Object,
                    participantMock3.Object
                };
                var queryHandlerMock = new Mock<IQueryHandler<ListParticipants.Query, IEnumerable<IParticipant>>>();
                queryHandlerMock.Setup(h => h.Handle(query)).Returns(participants);

                var sut = new Mediator();
                sut.Register(handler: queryHandlerMock.Object);

                // act
                var result = sut.Send<ListParticipants.Query, IEnumerable<IParticipant>>(query);

                // assert
                queryHandlerMock.Verify(h => h.Handle(query), Times.Once());
                Assert.Equal(participants, result);
            }
        }
    }
}
