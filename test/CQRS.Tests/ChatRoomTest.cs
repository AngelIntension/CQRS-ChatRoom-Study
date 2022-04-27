using CQRS.Abstractions;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CQRS.Tests
{
    public class ChatRoomTest
    {
        public class Add_Participant : ChatRoomTest
        {
            [Fact]
            public void ShouldAddSpecifiedParticipant()
            {
                // arrange
                var participant1Mock = new Mock<IParticipant>();
                var participant2Mock = new Mock<IParticipant>();
                var participant3Mock = new Mock<IParticipant>();

                var sut = new ChatRoom("chat room");

                // act
                sut.Add(participant1Mock.Object);
                sut.Add(participant2Mock.Object);
                sut.Add(participant3Mock.Object);

                // assert
                IEnumerable<IParticipant> participants = sut.ListParticipants();
                Assert.Collection(participants,
                    p => Assert.Same(participant1Mock.Object, p),                
                    p => Assert.Same(participant2Mock.Object, p),                
                    p => Assert.Same(participant3Mock.Object, p)
                );
            }
        }

        public class Remove : ChatRoomTest
        {
            [Fact]
            public void ShouldRemoveSpecifiedParticipant()
            {
                // arrange
                var participant1Mock = new Mock<IParticipant>();
                var participant2Mock = new Mock<IParticipant>();
                var participant3Mock = new Mock<IParticipant>();

                var sut = new ChatRoom("chat room");
                sut.Add(participant1Mock.Object);
                sut.Add(participant2Mock.Object);
                sut.Add(participant3Mock.Object);

                // act
                sut.Remove(participant2Mock.Object);

                // assert
                IEnumerable<IParticipant> participants = sut.ListParticipants();
                Assert.Collection(participants,
                    p => Assert.Same(participant1Mock.Object, p),
                    p => Assert.Same(participant3Mock.Object, p)
                );
            }
        }

        public class Add_ChatMessage : ChatRoomTest
        {
            [Fact]
            public void ShouldAddTheSpecifiedChatMessage()
            {
                // arrange
                var senderMock = new Mock<IParticipant>();

                var message1 = new ChatMessage(sender: senderMock.Object, content: "test message");
                var message2 = new ChatMessage(sender: senderMock.Object, content: "another test message");
                var message3 = new ChatMessage(sender: senderMock.Object, content: "yet another test message");

                var sut = new ChatRoom("chat room");

                // act
                sut.Add(message1);
                sut.Add(message2);
                sut.Add(message3);

                // assert
                IEnumerable<ChatMessage> messages = sut.ListMessages();
                Assert.Collection(messages,
                    m => Assert.Same(message1, m),
                    m => Assert.Same(message2, m),
                    m => Assert.Same(message3, m)
                );
            }
        }

        public class Constructor : ChatRoomTest
        {
            [Fact]
            public void SetSpecifiedName()
            {
                // act
                var sut = new ChatRoom(name: "test name");

                // assert
                Assert.Equal("test name", sut.Name);
            }
        }
    }
}
