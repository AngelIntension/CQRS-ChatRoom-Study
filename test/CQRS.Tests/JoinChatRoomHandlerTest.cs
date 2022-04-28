using CQRS.Abstractions;
using CQRS.Commands;
using Moq;
using Xunit;

namespace CQRS.Tests
{
    public class JoinChatRoomHandlerTest
    {
        public class Handle : JoinChatRoomHandlerTest
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
    }
}
