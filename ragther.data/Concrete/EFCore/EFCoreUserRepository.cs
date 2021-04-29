using System.Collections.Generic;
using System.Linq;
using ragther.core.DataAccess.EFCore;
using ragther.data.Abstract;
using ragther.core;
using ragther.entity;
using ragther.entity.ViewModels;
using AutoMapper;
using System;

namespace ragther.data.Concrete.EFCore
{
    public class EFCoreUserRepository : EFCoreGenericRepository<User, RagtherDbContext>, IUserRepository
    {
        
        IMapper _mapper;
        public EFCoreUserRepository(IMapper mapper){
            _mapper = mapper;
        }
        public VMUserProfileGet GetUserProfile(string userName)
        {
            using (var db = new RagtherDbContext())
            {
                VMUserProfileGet userProfileModel = db.Users.Select(
                    u => new VMUserProfileGet {
                        UserId = u.UserId,
                        UserName = u.UserName,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        CreatedAt = u.CreatedAt,
                        ProfileDetail = _mapper.Map<VMUserProfileDetailGet>(u.ProfileDetail)
                    }
                )
                .Where(u => u.UserName == userName)
                .FirstOrDefault();
                return userProfileModel;
            }
        }

        public List<VMUserSearchResultGet> GetUsersBySearchFilterString(string filterString)
        {
            using (var db = new RagtherDbContext())
            {
                List<VMUserSearchResultGet> result = db.Users.Select(
                    u => new VMUserSearchResultGet{
                        UserId = u.UserId,
                        UserName = u.UserName,
                        ProfileImageURL = u.ProfileImageURL,
                        FirstName = u.FirstName,
                        LastName = u.LastName
                    }
                )
                .Where( u => u.UserName.Contains(filterString) 
                    || u.FirstName.Contains(filterString)
                    || u.LastName.Contains(filterString))
                .ToList();

                return result;
            }
        }

        public VMUserLoggedinGet Login(string userName, string password)
        {
            using (var db = new RagtherDbContext())
            {
                VMUserLoggedinGet result = db.Users
                    .Where(u => u.UserName == userName && u.Password == password)
                    .Select(
                        u => new VMUserLoggedinGet{
                            UserId = u.UserId,
                            UserName = u.UserName,
                            SessionToken = "development-token"
                        }
                    ).FirstOrDefault();
                
                return result;
            }
        }

        // public User GetUserByUserName(string userName)
        // {
        //     using (var db = new RagtherDbContext())
        //     {               
        //         var user = db.Users
        //             .Where(u => u.UserName == userName)
        //             .FirstOrDefault();
        //         user.ProfileDetail = db.ProfileDetails
        //             .Where(pd => pd.UserId == user.UserId)
        //             .FirstOrDefault();
        //         return user;
        //     }
        // }

        // public bool IsMailRegistered(string email)
        // {
        //     using (var db = new RagtherDbContext())
        //     {
        //         var user = db.Users
        //             .Where(u => u.Email == email)
        //             .Select(u => new { u.Email })
        //             .FirstOrDefault();
        //         if (user == null)
        //         {
        //             return false;
        //         }
        //         return true;
        //     }
        // }

        // public bool IsUserNameRegistered(string userName)
        // {
        //     using (var db = new RagtherDbContext())
        //     {
        //         var user = db.Users
        //             .Where(u => u.UserName == userName)
        //             .Select(u => new { u.UserName })
        //             .FirstOrDefault();
        //         if (user == null)
        //         {
        //             return false;
        //         }
        //         return true;
        //     }
        // }

        // public User Signin(string userName, string password)
        // {
        //     using (var db = new RagtherDbContext())
        //     {
        //         var user = db.Users
        //             .Where(u => u.UserName==userName && u.Password == password)
        //             .FirstOrDefault();

        //         if (user==null)
        //         {
        //             return null;
        //         }
        //         return user;
        //     }
        // }
    }
}