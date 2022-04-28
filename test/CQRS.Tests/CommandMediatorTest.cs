using CQRS.Abstractions;
using CQRS.Commands;
using Moq;
using Xunit;

namespace CQRS.Tests.CommandMediatorTests
{
    public class CommandMediatorTest
    {
        public class Send_JoinChatRoomCommand : CommandMediatorTest
        {
            [Fact]
            public void ShouldSendSpecifiedCommandToAllRegisteredHandlers()
            {
                // arrange
                var participantMock = new Mock<IParticipant>();
                var chatRoomMock = new Mock<IChatRoom>();
                var joinHandler1Mock = new Mock<ICommandHandler<JoinChatRoom.Command>>();
                var joinHandler2Mock = new Mock<ICommandHandler<JoinChatRoom.Command>>();
                var joinHandler3Mock = new Mock<ICommandHandler<JoinChatRoom.Command>>();

                var sut = new CommandMediator();
                sut.Register(commandHandler: joinHandler1Mock.Object);
                sut.Register(commandHandler: joinHandler2Mock.Object);
                sut.Register(commandHandler: joinHandler3Mock.Object);

                var command = new JoinChatRoom.Command(requester: participantMock.Object, chatRoom: chatRoomMock.Object);

                // act
                sut.Send(command: command);

                // assert
                joinHandler1Mock.Verify(h => h.Handle(command), Times.Once());
                joinHandler2Mock.Verify(h => h.Handle(command), Times.Once());
                joinHandler3Mock.Verify(h => h.Handle(command), Times.Once());
            }
        }
    }
}
