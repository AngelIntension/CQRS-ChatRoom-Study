using CQRS.Abstractions;
using CQRS.Queries;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CQRS.Tests.QueryHandlerTests
{
    public class QueryHandlerTest
    {
        public class ListParticipants_Handler
        {
            [Fact]
            public void ShouldReturnAllParticipantsFromSpecifiedChatRoom()
            {
                // arrange
                var participant1Mock = new Mock<IParticipant>();
                var participant2Mock = new Mock<IParticipant>();
                var participant3Mock = new Mock<IParticipant>();
                var participants = new List<IParticipant>()
                {
                    participant1Mock.Object,
                    participant2Mock.Object,
                    participant3Mock.Object
                };
                var chatRoomMock = new Mock<IChatRoom>();
                chatRoomMock.Setup(c => c.ListParticipants()).Returns(participants);

                var query = new ListParticipants.Query(chatRoom: chatRoomMock.Object);

                var sut = new ListParticipants.Handler();

                // act
                var result = sut.Handle(query);

                // assert
                chatRoomMock.Verify(c => c.ListParticipants(), Times.Once());
                Assert.Equal(participants, result);
            }
        }

        public class ListMessages_Handler : QueryHandlerTest
        {
            [Fact]
            public void ShouldReturnAllChatMessagesFromSpecifiedChatRoom()
            {
                // arrange
                var senderMock = new Mock<IParticipant>();
                var messages = new List<ChatMessage>()
                {
                    new ChatMessage(senderMock.Object, "test message"),
                    new ChatMessage(senderMock.Object, "another test message"),
                    new ChatMessage(senderMock.Object, "yet another test message")
                };
                var chatRoomMock = new Mock<IChatRoom>();
                chatRoomMock.Setup(c => c.ListMessages()).Returns(messages);

                var query = new ListMessages.Query(chatRoom: chatRoomMock.Object);

                var sut = new ListMessages.Handler();

                // act
                var result = sut.Handle(query);

                // assert
                chatRoomMock.Verify(c => c.ListMessages(), Times.Once());
                Assert.Equal(messages, result);
            }
        }
    }
}
