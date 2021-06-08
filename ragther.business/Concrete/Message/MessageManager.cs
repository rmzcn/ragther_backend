using ragther.business.Abstract;
using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using ragther.business.Constants;
using ragther.Core.Utilities.Results;
using ragther.data.Abstract;
using ragther.data.MessagesForRelations;
using ragther.entity.ViewModels;

namespace ragther.business.Concrete.Message
{
    public class MessageManager : IMessageService
    {
        IUserRepository _userRepository;
        IChatRepository _chatRepository;
        IMessageRepository _messageRepository;

        public MessageManager(IUserRepository userRepository, IChatRepository chatRepository, IMessageRepository messageRepository){
            _userRepository = userRepository;
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
        }
        public IDataResult<List<VMChatMessageGet>> GetMessages(int chatId, string requesterUserName)
        {
            // returns all mesages of current chat
            var chat = _chatRepository.Get( c => c.ChatId == chatId);
            if (chat == null)
            {
                return new ErrorDataResult<List<VMChatMessageGet>>(Messages.ChatNotFound);
            }

            List<VMChatMessageGet> result = new List<VMChatMessageGet>();
            
            var messages = _messageRepository.GetListByFilterOrAll( m => m.ChatId == chat.ChatId ); 
            foreach (var message in messages)
            {
                result.Add( new VMChatMessageGet{
                    ChatId = message.ChatId,
                    Content = message.Content,
                    CreatedAt = message.CreatedAt,
                    isRead = message.isRead,
                    MessageId = message.MessageId,
                    authorUserName = _userRepository.Get( u => u.UserId == message.UserId).UserName
                });
            }

            result.Sort((x, y) => DateTime.Compare(x.CreatedAt, y.CreatedAt));

            ReadAllMesages(chatId, requesterUserName);

            return new SuccessDataResult<List<VMChatMessageGet>>(result);
        }

        public IDataResult<List<VMChatMessageGet>> GetUnreadMessages(int chatId, string requesterUserName)
        {
            var chat = _chatRepository.Get( c => c.ChatId == chatId);
            if (chat == null)
            {
                return new ErrorDataResult<List<VMChatMessageGet>>(Messages.ChatNotFound);
            }
            
            List<VMChatMessageGet> result = new List<VMChatMessageGet>();
            var requesterUser = _userRepository.Get(u => requesterUserName == u.UserName);
            var messages = _messageRepository.GetListByFilterOrAll( m => m.ChatId == chat.ChatId && m.isRead == false && m.UserId != requesterUser.UserId); 
            foreach (var message in messages)
            {
                result.Add( new VMChatMessageGet{
                    ChatId = message.ChatId,
                    Content = message.Content,
                    CreatedAt = message.CreatedAt,
                    isRead = message.isRead,
                    MessageId = message.MessageId,
                    authorUserName = _userRepository.Get( u => u.UserId == message.UserId).UserName
                });
            }

            // set to read messages after sending to user
            foreach (var unreadMessage in messages)
            {
                unreadMessage.isRead = true;
                _messageRepository.Update(unreadMessage);
            }

            if (result.Count == 0)
            {
                return new ErrorDataResult<List<VMChatMessageGet>>(Messages.NoNewMessages);
            }

            result.Sort((x, y) => DateTime.Compare(x.CreatedAt, y.CreatedAt));

            return new SuccessDataResult<List<VMChatMessageGet>>(result);
            
        }

        public IResult SendMessage(string senderUserName, int chatId, string content)
        {
            var chat = _chatRepository.Get( c => c.ChatId == chatId);
            if (chat == null)
            {
                return new ErrorDataResult<List<VMChatMessageGet>>(Messages.ChatNotFound);
            }

            entity.Message newMessage = new entity.Message(){
                UserId = _userRepository.Get( u => u.UserName == senderUserName).UserId,
                ChatId = chatId,
                isRead = false,
                CreatedAt = DateTime.Now,
                Content = content
            };

            _messageRepository.Add(newMessage);

            return new SuccessResult(Messages.MessageSended);

        }

        public void ReadAllMesages(int chatId, string requesterUserName)
        {
            var chat = _chatRepository.Get( c => c.ChatId == chatId);
            if (chat != null)
            {
                var user = _userRepository.Get( u => u.UserName == requesterUserName);
                var messages = _messageRepository.GetListByFilterOrAll( m => m.ChatId == chatId && m.UserId != user.UserId );

                foreach (var message in messages)
                {
                    message.isRead = true;
                    _messageRepository.Update(message);
                }
            }
        }
    }
}