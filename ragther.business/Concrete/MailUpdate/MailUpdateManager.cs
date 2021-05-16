using System;
using ragther.business.Abstract;
using ragther.business.Constants;
using ragther.business.Generators;
using ragther.Core.Utilities.Results;
using ragther.data.Abstract;
using ragther.data.MessagesForRelations;
using ragther.business.Generators.Abstract;
using ragther.business.Generators.Base;
using ragther.business.Helpers;

namespace ragther.business.Concrete.MailUpdate
{
    public class MailUpdateManager : IMailUpdateService
    {
        IUserRepository _userRepository;
        IMailUpdateRepository _mailUpdateRepository;
        ITokenConditionRepository _tokenConditionRepository;
        INoticeService _noticeService;
        public MailUpdateManager(IUserRepository userRepository, IMailUpdateRepository mailUpdateRepository, ITokenConditionRepository tokenConditionRepository, INoticeService noticeService)
        {
            _userRepository = userRepository;
            _mailUpdateRepository = mailUpdateRepository;
            _tokenConditionRepository = tokenConditionRepository;
            _noticeService = noticeService;
        }
        public IResult CreateUpdateRequest(string requesterUserName, string newMailAdress)
        {
            entity.User user = _userRepository.Get( u => u.UserName == requesterUserName);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            
            //if there is waiting mail update request in db already, break the request
            var pastUpdateRequests = _mailUpdateRepository.Get( mu => mu.UserId == user.UserId && mu.TokenConditionId == TokenConditions.Waiting);
            if (pastUpdateRequests != null)
            {
                return new ErrorResult(Messages.MailUpdateRequestAlreadyExist);
            }

            IGenerator mailGenerator = new GeneratorBase(new MailTokenCreator());
            string mailUpdateToken = mailGenerator.Generate();
            entity.MailUpdate mailUpdate = new entity.MailUpdate(){
                CreatedAt = DateTime.Now,
                UserId = user.UserId,
                TokenConditionId = TokenConditions.Waiting,
                Token = mailUpdateToken,
                MailWillUpdate = newMailAdress
            }; 

            _mailUpdateRepository.Add(mailUpdate);

            //mail linki güncelleme kullanıcıya gönderiliyor
            var result = MailHelper.SendEmail(user.Email, MailTemplate.GetEmailUpdateBody(user.UserName, newMailAdress, Links.EmailUpdateLink+mailUpdateToken),MailSubjects.EmailUpdate);

            return new SuccessResult(Messages.MailUpdateRequestCreated);
        }

        public IResult UpdateEmail(string token)
        {
            var mailUpdateRequest = _mailUpdateRepository.Get( mu => mu.Token == token);
            
            if (mailUpdateRequest == null)
            {
                return new ErrorResult(Messages.MailUpdateTokenNotFound);
            }

            if ((DateTime.Now - mailUpdateRequest.CreatedAt).TotalDays > DateInfos.ValidityDaysOfMailUpdateToken)
            {
                mailUpdateRequest.TokenConditionId = TokenConditions.Expired;
            }

            if (mailUpdateRequest.TokenConditionId == TokenConditions.Used)
            {
                return new ErrorResult(Messages.MailUpdateTokenAlreadyUsed);
            }
            else if (mailUpdateRequest.TokenConditionId == TokenConditions.Expired)
            {
                return new ErrorResult(Messages.MailUpdateTokenExpired);
            }
            else
            {
                string mailWillUpdated = mailUpdateRequest.MailWillUpdate;
                entity.User user = _userRepository.Get( u => u.UserId == mailUpdateRequest.UserId);
                //updating users mail address
                user.Email = mailWillUpdated;
                _userRepository.Update(user);
                
                //creating mail update notice
                _noticeService.CreateNotice(user.UserId, user.UserId, NoticeTypes.MailUpdated);

                mailUpdateRequest.TokenConditionId = TokenConditions.Used;
                _mailUpdateRepository.Update(mailUpdateRequest);
                return new SuccessResult(Messages.MailUpdateSuccess);
            }
            
        }
    }
}