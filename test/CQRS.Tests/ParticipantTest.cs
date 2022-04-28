using CQRS.Abstractions;
using CQRS.Commands;
using CQRS.Queries;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CQRS.Tests.ParticipantTests
{
    public class ParticipantTest
    {
        private readonly Mediator mediator;

        public ParticipantTest()
        {
            mediator = new Mediator();
            mediator.Register(new JoinChatRoom.Handler());
            mediator.Register(new LeaveChatRoom.Handler());
            mediator.Register(new SendChatMessage.Handler());
            mediator.Register(new ListParticipants.Handler());
            mediator.Register(new ListMessages.Handler());
        }

        public class Join : ParticipantTest
        {
            [Fact]
            public void ShouldAddSelfToSpecifiedChatRoom()
            {
                // arrange
                var chatRoomMock = new Mock<IChatRoom>();
                var writerMock = new Mock<IMessageWriter>();

                var sut = new Participant(mediator, writerMock.Object);

                // act
                sut.Join(chatRoomMock.Object);

                // assert
                chatRoomMock.Verify(c => c.Add(sut), Times.Once());
            }
        }

        public class Leave : ParticipantTest
        {
            [Fact]
            public void ShouldRemoveSelfFromSpecifiedChatRoom()
            {
                // arrange
                var chatRoomMock = new Mock<IChatRoom>();
                var writerMock = new Mock<IMessageWriter>();

                var sut = new Participant(mediator, writerMock.Object);

                // act
                sut.Leave(chatRoomMock.Object);

                // assert
                chatRoomMock.Verify(c => c.Remove(sut), Times.Once());
            }
        }

        public class SendMessageTo : ParticipantTest
        {
            [Fact]
            public void ShouldAddSpecifiedMessageToSpecifiedChatRoom()
            {
                // arrange
                var writerMock = new Mock<IMessageWriter>();

                var sut = new Participant(mediator, writerMock.Object);

                var chatRoomMock = new Mock<IChatRoom>();
                chatRoomMock.Setup(c => c.Add(It.IsAny<ChatMessage>()))
                    .Callback<ChatMessage>(message =>
                    {
                        Assert.Same(sut, message.Sender);
                        Assert.Equal("test message", message.Content);
                    });

                // act
                sut.SendMessageTo(chatRoom: chatRoomMock.Object, message: "test message");

                // assert
                chatRoomMock.Verify(c => c.Add(It.IsAny<ChatMessage>()), Times.Once());
            }
        }

        public class NewMessageReceivedFrom : ParticipantTest
        {
            [Fact]
            public void ShouldWriteReceivedMessage()
            {
                // arrange
                var writerMock = new Mock<IMessageWriter>();
                var chatRoomMock = new Mock<IChatRoom>();
                var senderMock = new Mock<IParticipant>();

                var message = new ChatMessage(senderMock.Object, "test Message");

                var sut = new Participant(mediator, messageWriter: writerMock.Object);

                // act
                sut.NewMessageReceivedFrom(chatRoom: chatRoomMock.Object, message: message);

                // assert
                writerMock.Verify(w => w.Write(chatRoomMock.Object, message));
            }
        }

        public class ListParticipantsOf : ParticipantTest
        {
            [Fact]
            public void ShouldReturnAllParticipantsOfSpecifiedChatRoom()
            {
                // arrange
                var writerMock = new Mock<IMessageWriter>();
                var data = new List<Participant>()
                {
                    new Participant(mediator, writerMock.Object),
                    new Participant(mediator, writerMock.Object),
                    new Participant(mediator, writerMock.Object)
                };
                var chatRoomMock = new Mock<IChatRoom>();
                chatRoomMock.Setup(c => c.ListParticipants()).Returns(data);

                var sut = new Participant(mediator, writerMock.Object);

                // act
                IEnumerable<IParticipant> participants = sut.ListParticipantsOf(chatRoom: chatRoomMock.Object);

                // assert
                Assert.Equal(data, participants);
            }
        }

        public class ListMessagesOf : ParticipantTest
        {
            [Fact]
            public void ShouldReturnAllMessagesFromSpecifiedChatRoom()
            {
                // arrange
                var writerMock = new Mock<IMessageWriter>();
                var sut = new Participant(mediator, writerMock.Object);

                var data = new List<ChatMessage>()
                {
                    new ChatMessage(sut, "test message"),
                    new ChatMessage(sut, "another test message"),
                    new ChatMessage(sut, "yet another test message")
                };
                var chatRoomMock = new Mock<IChatRoom>();
                chatRoomMock.Setup(c => c.ListMessages()).Returns(data);

                // act
                IEnumerable<ChatMessage> messages = sut.ListMessagesOf(chatRoom: chatRoomMock.Object);

                // assert
                Assert.Equal(data, messages);
            }
        }
    }
}
