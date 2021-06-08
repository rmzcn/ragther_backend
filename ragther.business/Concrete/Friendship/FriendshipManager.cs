using System;
using System.Collections.Generic;
using ragther.business.Abstract;
using ragther.business.Constants;
using ragther.Core.Utilities.Results;
using ragther.data.Abstract;
using ragther.data.MessagesForRelations;
using ragther.entity.ViewModels;

namespace ragther.business.Concrete.Friendship
{
    public class FriendshipManager : IFriendshipService
    {
        IUserRepository _userRepository;
        IFriendshipRepository _friendshipRepository;
        INoticeService _noticeService;
        IProfileDetailService _profileDetailService;

        public FriendshipManager(IUserRepository userRepository, IFriendshipRepository friendshipRepository, INoticeService noticeService, IProfileDetailService profileDetailService){
            _userRepository = userRepository;
            _friendshipRepository = friendshipRepository;
            _noticeService = noticeService;
            _profileDetailService = profileDetailService;
        }

        public IResult CreateFriendshipRequest(string senderUserName, string recipientUserName)
        {
            
            entity.User senderUserModel = _userRepository.Get(u => u.UserName == senderUserName);
            entity.User recipientUserModel = _userRepository.Get(u => u.UserName == recipientUserName);
            if (recipientUserModel == null || senderUserModel == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            if (this.isFriends(senderUserName, recipientUserName).Success)
            {
                return new ErrorResult(Messages.AlreadyFriends);
            }

            
            var conditionResult = this.GetFriendshipCondition(senderUserName, recipientUserName);
            if (conditionResult.Success
                &&
                FriendshipAndFriendshipCondition.getFriendshipConditionIdByName(conditionResult.Data) 
                    == 
                FriendshipAndFriendshipCondition.NotExistTableValue
                )
            
            {
                entity.Friendship newFriendshipRequest = new entity.Friendship(){
                    FriendshipConditionId = FriendshipAndFriendshipCondition.RequestWaiting,
                    CreatedAt = DateTime.Now,
                    SenderUserId = senderUserModel.UserId,
                    RecipientUserId = recipientUserModel.UserId
                };
                _friendshipRepository.Add(newFriendshipRequest);

                // notificaiton --START--
                _noticeService.CreateNotice(senderUserModel.UserId,recipientUserModel.UserId,NoticeTypes.FriendshipRequest);
                // notificaiton --END--

                return new SuccessResult(Messages.FriendshipSended);
            }

            else if (conditionResult.Success
                    &&
                    FriendshipAndFriendshipCondition.getFriendshipConditionIdByName(conditionResult.Data) 
                    == 
                    FriendshipAndFriendshipCondition.RequestWaiting)
            {
                return new ErrorResult(Messages.FriendshipAlreadySended);
            }
            
            return new ErrorResult(conditionResult.Message);

        }

        public IDataResult<String> GetFriendshipCondition(string requesterUserName, string targetUserName)
        {
            var requesterUserModel = _userRepository.Get(u => u.UserName == requesterUserName);
            var targetUserModel = _userRepository.Get(u => u.UserName == targetUserName);
            if (requesterUserModel == null || targetUserModel == null)
            {
                return new ErrorDataResult<String>(null, Messages.UserNotFound);
            }

            var friendshipValue = _friendshipRepository.Get(fr => 
                (fr.SenderUserId == requesterUserModel.UserId || fr.RecipientUserId == requesterUserModel.UserId)
                    &&
                (fr.SenderUserId == targetUserModel.UserId || fr.RecipientUserId == targetUserModel.UserId)
            
            );

            if (friendshipValue == null)
            {
                return new SuccessDataResult<String>(FriendshipAndFriendshipCondition.getFriendshipConditionNameByID(FriendshipAndFriendshipCondition.NotExistTableValue));
            }
            
            return new SuccessDataResult<String>(FriendshipAndFriendshipCondition.getFriendshipConditionNameByID(friendshipValue.FriendshipConditionId));
            

        }
        
        public IResult RejectFriendshipRequest(string rejecterUserName, string senderUserName)
        {
            entity.User senderUserModel = _userRepository.Get(u => u.UserName == senderUserName);
            entity.User rejecterUserModel = _userRepository.Get(u => u.UserName == rejecterUserName);
            if (rejecterUserModel == null || senderUserModel == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            if (this.isFriends(senderUserName, rejecterUserName).Success)
            {
                return new ErrorResult(Messages.UsersAreFriends);
            }

            var conditionResult = this.GetFriendshipCondition(rejecterUserName, senderUserName);

            if (conditionResult.Success
                &&
                FriendshipAndFriendshipCondition.getFriendshipConditionIdByName(conditionResult.Message) 
                    == 
                FriendshipAndFriendshipCondition.RequestWaiting)
            {
                //reddetme ve bildirim işlemleri
                _noticeService.CreateNotice(rejecterUserModel.UserId, senderUserModel.UserId, NoticeTypes.FriendshipReject);

                var requestWillRejected = _friendshipRepository.Get( fr => fr.SenderUserId == senderUserModel.UserId && rejecterUserModel.UserId == fr.RecipientUserId);
                if (requestWillRejected == null)
                {
                    return new ErrorResult(Messages.FriendshipNotFound);
                }
                _friendshipRepository.Delete(requestWillRejected);

                //profil puanı güncelleme
                _profileDetailService.SetProfileScore(rejecterUserModel.UserId, Score.FriendshipRejectScore);

                return new SuccessResult(Messages.FriendshipRejected);
            }

            else if (conditionResult.Success
                    &&
                    FriendshipAndFriendshipCondition.getFriendshipConditionIdByName(conditionResult.Message) 
                    == 
                    FriendshipAndFriendshipCondition.NotExistTableValue)
            {
                return new ErrorResult(Messages.FriendshipNotFound);
            }
            return new ErrorResult(conditionResult.Message);

        }

        public IResult RevokeFriendshipRequest(string revokerUserName, string recipientUserName)
        {
            // unfriend process
            entity.User revokerUserModel = _userRepository.Get(u => u.UserName == revokerUserName);
            entity.User recipientUserModel = _userRepository.Get(u => u.UserName == recipientUserName);
            if (revokerUserModel == null || recipientUserModel == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            if (!this.isFriends(revokerUserName, recipientUserName).Success)
            {
                return new ErrorResult(Messages.UsersAreNotFriends);
            }

            var conditionResult = this.GetFriendshipCondition(revokerUserName, recipientUserName);

            if (conditionResult.Success
                &&
                FriendshipAndFriendshipCondition.getFriendshipConditionIdByName(conditionResult.Message) 
                    == 
                FriendshipAndFriendshipCondition.Friend)
            {
                //reddetme işlemleri
                var friendshipEntity = _friendshipRepository.Get( fr => 
                    (fr.SenderUserId == revokerUserModel.UserId || revokerUserModel.UserId == fr.RecipientUserId)
                    &&
                    (fr.SenderUserId == recipientUserModel.UserId || recipientUserModel.UserId == fr.RecipientUserId)
                );
                _friendshipRepository.Delete(friendshipEntity);

                //profil bilgilerini güncelleme
                _profileDetailService.SetProfileScore(revokerUserModel.UserId, Score.FriendshipRevokeScore);
                //arkadaşlıktan çıkaran kişinin arkadaşlık sayısını düşürmek
                _profileDetailService.SetFriendCount(revokerUserModel.UserId,false);
                //arkadaşlıktan çıkarılan kişinin arkadaşlık sayısını düşürmek
                _profileDetailService.SetFriendCount(recipientUserModel.UserId,false);
                return new SuccessResult(Messages.FriendshipRevoked);

            }

            else if (conditionResult.Success
                    &&
                    FriendshipAndFriendshipCondition.getFriendshipConditionIdByName(conditionResult.Message) 
                    == 
                    FriendshipAndFriendshipCondition.NotExistTableValue)
            {
                return new ErrorResult(Messages.FriendshipNotFound);
            }
            return new ErrorResult(conditionResult.Message);

        }

        public IResult CreateFriendship(string senderUserName, string recipientUserName)
        {
            //senderUserName --> user that create friendship request 
            //recipientUserName --> user that friendship request recipient
            entity.User senderUserModel = _userRepository.Get(u => u.UserName == senderUserName);
            entity.User recipientUserModel = _userRepository.Get(u => u.UserName == recipientUserName);
            if (senderUserModel == null || recipientUserModel == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            if (this.isFriends(senderUserName, recipientUserName).Success)
            {
                return new ErrorResult(Messages.UsersAreFriends);
            }

            var conditionResult = this.GetFriendshipCondition(senderUserName, recipientUserName);
            if (conditionResult.Success
                &&
                FriendshipAndFriendshipCondition.getFriendshipConditionIdByName(conditionResult.Message) 
                    == 
                FriendshipAndFriendshipCondition.RequestWaiting)
            {
                //kabul etme ve bildirim işlemleri
                _noticeService.CreateNotice(recipientUserModel.UserId, senderUserModel.UserId,NoticeTypes.FriendshipAcceppted);

                //profil bilgilerini güncelleme
                _profileDetailService.SetProfileScore(senderUserModel.UserId, Score.FriendshipScore);
                _profileDetailService.SetProfileScore(recipientUserModel.UserId, Score.FriendshipScore);
                //arkadaşlılık isteğinde bulunan kişinin arkadaşlık sayısını arttırmak
                _profileDetailService.SetFriendCount(senderUserModel.UserId);
                //arkadaşlılık isteğinde bulunulan kişinin arkadaşlık sayısını arttırmak
                _profileDetailService.SetFriendCount(recipientUserModel.UserId);

                var friendshipEntity = _friendshipRepository.Get( fr => 
                    fr.SenderUserId == senderUserModel.UserId && recipientUserModel.UserId == fr.RecipientUserId
                );

                //arkadaşlık durumun güncelleme
                friendshipEntity.FriendshipConditionId = FriendshipAndFriendshipCondition.Friend;

                _friendshipRepository.Update(friendshipEntity);
                return new SuccessResult(Messages.FriendshipAcceppted);
            }

            else if (conditionResult.Success
                    &&
                    FriendshipAndFriendshipCondition.getFriendshipConditionIdByName(conditionResult.Message) 
                    == 
                    FriendshipAndFriendshipCondition.NotExistTableValue)
            {
                return new ErrorResult(Messages.FriendshipNotFound);
            }
            return new ErrorResult(conditionResult.Message);
        }

        public IResult isFriends(string user1, string user2)
        {
            var user1model = _userRepository.Get(u => u.UserName == user1);
            var user2model = _userRepository.Get(u => u.UserName == user2);
            if (user1model == null || user2model == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            var friendship = _friendshipRepository.GetListByFilterOrAll(fr => 
                fr.FriendshipConditionId == FriendshipAndFriendshipCondition.Friend 
                &&
                ((fr.SenderUserId == user1model.UserId || fr.RecipientUserId == user1model.UserId)
                    &&
                (fr.SenderUserId == user2model.UserId || fr.RecipientUserId == user2model.UserId))
            );
            if (friendship == null || friendship.Count == 0)
            {
                return new ErrorResult(Messages.UsersAreNotFriends);
            }
            return new SuccessResult(Messages.UsersAreFriends);
        }

        public IResult GetFriendship(string requesterUserName, string targetUserName)
        {
            var requesterUserModel = _userRepository.Get(u => u.UserName == requesterUserName);
            var targetUserModel = _userRepository.Get(u => u.UserName == targetUserName);
            if (requesterUserModel == null || targetUserModel == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            if (requesterUserName == targetUserName)
            {
                return new SuccessResult(GetFriendshipDataForApiResponse.Friends.ToString());
            }

            var friendship = _friendshipRepository.Get(fr => 
                (fr.SenderUserId == requesterUserModel.UserId || fr.RecipientUserId == requesterUserModel.UserId)
                    &&
                (fr.SenderUserId == targetUserModel.UserId || fr.RecipientUserId == targetUserModel.UserId)
            );
            
            if (friendship == null)
            {
                return new SuccessResult(GetFriendshipDataForApiResponse.NotFriends.ToString());
            }

            if (friendship.SenderUserId == requesterUserModel.UserId && friendship.FriendshipConditionId == FriendshipAndFriendshipCondition.RequestWaiting)
            {
                return new SuccessResult(GetFriendshipDataForApiResponse.Sender_RequestWaiting.ToString());
            }
            else if (friendship.RecipientUserId == requesterUserModel.UserId && friendship.FriendshipConditionId == FriendshipAndFriendshipCondition.RequestWaiting )
            {
                return new SuccessResult(GetFriendshipDataForApiResponse.Recipient_RequestWaiting.ToString());
            }

            if (friendship.FriendshipConditionId == FriendshipAndFriendshipCondition.Friend)
            {
                return new SuccessResult(GetFriendshipDataForApiResponse.Friends.ToString());
            }

            return new ErrorResult();
        }

        public IDataResult<List<VMInnerUserInfo>> GetFriendsForChatService(string requesterUserName)
        {
            var userModel = _userRepository.Get(u => u.UserName == requesterUserName);
            if (userModel == null)
            {
                return new ErrorDataResult<List<VMInnerUserInfo>>(Messages.UserNotFound);
            }

            List<entity.Friendship> friends = _friendshipRepository.GetListByFilterOrAll(fr => 
                (fr.SenderUserId == userModel.UserId || fr.RecipientUserId == userModel.UserId)
                    &&
                (fr.FriendshipConditionId == FriendshipAndFriendshipCondition.Friend)
            );

            List<VMInnerUserInfo> usersFriends = new List<VMInnerUserInfo>();
            int friendUserId;
            entity.User friendUser;
            foreach (var friend in friends)
            {  
                if (friend.RecipientUserId == userModel.UserId)
                {
                    friendUserId = friend.SenderUserId;
                }
                else
                {
                    friendUserId = friend.RecipientUserId;
                }
                friendUser = _userRepository.Get( u => u.UserId == friendUserId);
                usersFriends.Add( new VMInnerUserInfo{
                    FirstName = friendUser.FirstName,
                    LastName = friendUser.LastName,
                    ProfileImageURL = friendUser.ProfileImageURL,
                    UserId = friendUser.UserId,
                    UserName = friendUser.UserName
                });
            }
            return new SuccessDataResult<List<VMInnerUserInfo>>(usersFriends);
        }
    }
}