using CQRS.Abstractions;
using CQRS.Commands;
using Moq;
using Xunit;

namespace CQRS.Tests
{
    public class JoinCommandMediatorTest
    {
        public class Send : JoinCommandMediatorTest
        {
            [Fact]
            public void ShouldSendSpecifiedCommandToAllRegisteredHandlers()
            {
                // arrange
                var participantMock = new Mock<IParticipant>();
                var chatRoomMock = new Mock<IChatRoom>();
                var joinHandler1Mock = new Mock<IJoinHandler>();
                var joinHandler2Mock = new Mock<IJoinHandler>();
                var joinHandler3Mock = new Mock<IJoinHandler>();

                var sut = new JoinCommandMediator();
                sut.Register(commandHandler: joinHandler1Mock.Object);
                sut.Register(commandHandler: joinHandler2Mock.Object);
                sut.Register(commandHandler: joinHandler3Mock.Object);

                var command = new Join.Command(sender: participantMock.Object, chatRoom: chatRoomMock.Object);

                // act
                sut.Send(command: command);

                // assert
                joinHandler1Mock.Verify(h => h.Handle(command));
                joinHandler2Mock.Verify(h => h.Handle(command));
                joinHandler3Mock.Verify(h => h.Handle(command));
            }
        }
    }
}
