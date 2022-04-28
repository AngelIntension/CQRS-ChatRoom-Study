using CQRS.Abstractions;
using CQRS.Commands;
using Moq;
using Xunit;

namespace CQRS.Tests.CommandHandlerTests
{
    public class CommandHandlerTest
    {
        public class JoinChatRoom_Handler : CommandHandlerTest
        {
            [Fact]
            public void ShouldAddSpecifiedRequesterToSpecifiedChatRoom()
            {
                // arrange
                var requesterMock = new Mock<IParticipant>();
                var chatRoomMock = new Mock<IChatRoom>();
                var command = new JoinChatRoom.Command(requesterMock.Object, chatRoomMock.Object);

                var sut = new JoinChatRoom.Handler();

                // act
                sut.Handle(command);

                // assert
                chatRoomMock.Verify(c => c.Add(requesterMock.Object), Times.Once());
            }
        }

        public class LeaveChatRoom_Handler : CommandHandlerTest
        {
            [Fact]
            public void ShouldRemoveSpecifiedRequesterFromSpecifiedChatRoom()
            {
                // arrange
                var requesterMock = new Mock<IParticipant>();
                var chatRoomMock = new Mock<IChatRoom>();
                var command = new LeaveChatRoom.Command(requester: requesterMock.Object,chatRoom: chatRoomMock.Object);

                var sut = new LeaveChatRoom.Handler();

                // act
                sut.Handle(command);

                // assert
                chatRoomMock.Verify(c => c.Remove(requesterMock.Object), Times.Once());
            }
        }

        public class SendChatMessage_Handler : CommandHandlerTest
        {
            [Fact]
            public void ShouldSendSpecifiedMessageToSpecifiedChatRoom()
            {
                // arrange
                var chatRoomMock = new Mock<IChatRoom>();
                var senderMock = new Mock<IParticipant>();

                var message = new ChatMessage(senderMock.Object, "test message");
                var command = new SendChatMessage.Command(chatRoom: chatRoomMock.Object, message: message);

                var sut = new SendChatMessage.Handler();

                // act
                sut.Handle(command);

                // assert
                chatRoomMock.Verify(c => c.Add(message), Times.Once());
            }
        }
    }
}
