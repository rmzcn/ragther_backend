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


namespace ragther.business.Concrete.Chat
{
    public class ChatManager : IChatService
    {

        IUserRepository _userRepository;
        IFriendshipRepository _friendshipRepository;
        INoticeService _noticeService;
        IChatRepository _chatRepository;
        IMessageRepository _messageRepository;

        public ChatManager(IUserRepository userRepository, IFriendshipRepository friendshipRepository, INoticeService noticeService, IChatRepository chatRepository, IMessageRepository messageRepository){
            _userRepository = userRepository;
            _friendshipRepository = friendshipRepository;
            _noticeService = noticeService;
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
        }
        public IResult CreateChat(string requesterUserName, string secondUserName)
        {
            var requesterUserModel = _userRepository.Get(u => u.UserName == requesterUserName);
            var secondUserModel = _userRepository.Get(u => u.UserName == secondUserName);
            if (requesterUserModel == null || secondUserModel == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            entity.Chat newChat = new entity.Chat(){
                FirstUserId = requesterUserModel.UserId,
                SecondUserId = secondUserModel.UserId,               
            };

            _chatRepository.Add(newChat);

            _noticeService.CreateNotice(requesterUserModel.UserId, secondUserModel.UserId, NoticeTypes.NewChat, newChat.ChatId.ToString());

            return new SuccessResult(Messages.ChatCreated);
        }

        public IDataResult<VMChatHeadModelGet> GetChatHead(string requesterUserName, int chatId)
        {
            var chat = _chatRepository.Get(c => c.ChatId == chatId);
            if (chat == null)
            {
                return new ErrorDataResult<VMChatHeadModelGet>(Messages.ChatNotFound);
            }

            var user = _userRepository.Get(u => u.UserName == requesterUserName);

            entity.User resultUser;


            if (chat.FirstUserId == user.UserId)
            {
                resultUser = _userRepository.Get( u =>  u.UserId == chat.SecondUserId);
            }
            
            else
            {
                resultUser = _userRepository.Get( u =>  u.UserId == chat.FirstUserId);
            }

            VMChatHeadModelGet result = new VMChatHeadModelGet{
                ChatId = chatId,
                FirstName = resultUser.FirstName,
                LastName = resultUser.LastName,
                UserName = resultUser.UserName,
                UserProfileImageURL = resultUser.ProfileImageURL
            };

            return new SuccessDataResult<VMChatHeadModelGet>(result);
            
        }

        public IDataResult<List<VMChatModelGet>> GetChats(string requesterUserName)
        {
            var requesterUserModel = _userRepository.Get(u => u.UserName == requesterUserName);
            if (requesterUserModel == null)
            {
                return new ErrorDataResult<List<VMChatModelGet>>(Messages.UserNotFound);
            }

            var chats = _chatRepository.GetListByFilterOrAll(c => c.FirstUserId == requesterUserModel.UserId || c.SecondUserId == requesterUserModel.UserId);
            

            List<VMChatModelGet> result = new List<VMChatModelGet>();
            entity.User user;
            VMChatModelGet model;
            foreach (var chat in chats)
            {

                if (chat.FirstUserId == requesterUserModel.UserId)
                {
                    user = _userRepository.Get( u =>  u.UserId == chat.SecondUserId);
                }
                
                else
                {
                    user = _userRepository.Get( u =>  u.UserId == chat.FirstUserId);
                }

                model = new VMChatModelGet{
                    ChatId = chat.ChatId,
                    LastMessage = "___development.progress___",
                    SecondUserName = user.UserName,
                    SecondUserProfileImageURL = user.ProfileImageURL
                };
                result.Add(model);
            }
            return new SuccessDataResult<List<VMChatModelGet>>(result);
        }
    }
}