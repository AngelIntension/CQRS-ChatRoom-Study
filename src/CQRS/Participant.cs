using CQRS.Abstractions;
using CQRS.Commands;
using CQRS.Queries;
using System.Collections.Generic;

namespace CQRS
{
    public class Participant : IParticipant
    {
        private readonly IMessageWriter messageWriter;
        private readonly ICommandMediator commandMediator;
        private readonly IQueryMediator queryMediator;

        public Participant(ICommandMediator commandMediator, IQueryMediator queryMediator, IMessageWriter messageWriter)
        {
            this.messageWriter = messageWriter;
            this.commandMediator = commandMediator;
            this.queryMediator = queryMediator;
        }

        public void Join(IChatRoom chatRoom)
        {
            commandMediator.Send(new JoinChatRoom.Command(this, chatRoom));
        }

        public void Leave(IChatRoom chatRoom)
        {
            commandMediator.Send(new LeaveChatRoom.Command(this, chatRoom));
        }

        public void SendMessageTo(IChatRoom chatRoom, string message)
        {
            var chatMessage = new ChatMessage(this, message);
            commandMediator.Send(new SendChatMessage.Command(chatRoom, chatMessage));
        }

        public void NewMessageReceivedFrom(IChatRoom chatRoom, ChatMessage message)
        {
            messageWriter.Write(chatRoom, message);
        }

        public IEnumerable<IParticipant> ListParticipantsOf(IChatRoom chatRoom)
        {
            return queryMediator.Send<ListParticipants.Query, IEnumerable<IParticipant>>(new ListParticipants.Query(chatRoom));
        }

        public IEnumerable<ChatMessage> ListMessagesOf(IChatRoom chatRoom)
        {
            return queryMediator.Send<ListMessages.Query, IEnumerable<ChatMessage>>(new ListMessages.Query(chatRoom));
        }
    }
}
