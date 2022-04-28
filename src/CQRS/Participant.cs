using CQRS.Abstractions;
using CQRS.Commands;
using CQRS.Queries;
using System.Collections.Generic;

namespace CQRS
{
    public class Participant : IParticipant
    {
        private readonly IMessageWriter messageWriter;
        private readonly IMediator mediator;

        public Participant(IMediator mediator, IMessageWriter messageWriter)
        {
            this.messageWriter = messageWriter;
            this.mediator = mediator;
        }

        public void Join(IChatRoom chatRoom)
        {
            mediator.Send(new JoinChatRoom.Command(this, chatRoom));
        }

        public void Leave(IChatRoom chatRoom)
        {
            mediator.Send(new LeaveChatRoom.Command(this, chatRoom));
        }

        public void SendMessageTo(IChatRoom chatRoom, string message)
        {
            var chatMessage = new ChatMessage(this, message);
            mediator.Send(new SendChatMessage.Command(chatRoom, chatMessage));
        }

        public void NewMessageReceivedFrom(IChatRoom chatRoom, ChatMessage message)
        {
            messageWriter.Write(chatRoom, message);
        }

        public IEnumerable<IParticipant> ListParticipantsOf(IChatRoom chatRoom)
        {
            return mediator.Send<ListParticipants.Query, IEnumerable<IParticipant>>(new ListParticipants.Query(chatRoom));
        }

        public IEnumerable<ChatMessage> ListMessagesOf(IChatRoom chatRoom)
        {
            return mediator.Send<ListMessages.Query, IEnumerable<ChatMessage>>(new ListMessages.Query(chatRoom));
        }
    }
}
