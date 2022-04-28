using CQRS.Abstractions;
using CQRS.Commands;
using Moq;
using Xunit;

namespace CQRS.Tests
{
    public class CommandHandlerTest
    {
        public class JoinChatRoom_Handle : CommandHandlerTest
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

        public class LeaveChatRoom_Handle : CommandHandlerTest
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
                chatRoomMock.Verify(c => c.Remove(requesterMock.Object));
            }
        }
    }
}
