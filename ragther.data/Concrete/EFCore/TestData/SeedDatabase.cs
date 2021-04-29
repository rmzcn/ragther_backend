using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ragther.entity;

namespace ragther.data.Concrete.EFCore.TestData
{
    public static class SeedDatabase
    {
        public static void Seed(){
            var context = new RagtherDbContext();
            //if there is no Migrations
            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context.Users.Count() == 0)
                {
                    
                }
            }
        }

        private static User[] Users = {
            new User(){UserId= 1, Email="ramazancangolgen@gmail.com", UserName="admin", FirstName="Ramazan Can", LastName= "Gölgen", Password="123456789", CreatedAt= new DateTime(2021, 4, 20, 6, 36, 0)},
            new User(){UserId= 2, Email="ali@gmail.com", UserName="aliylmz", FirstName="Ali", LastName= "Yılaz", Password="123456789", CreatedAt= new DateTime(2021, 5, 10, 7, 50, 0)},
            new User(){UserId= 3, Email="ayse@gmail.com", UserName="ayseylmz", FirstName="Ayşe", LastName= "Yılmaz", Password="123456789", CreatedAt= new DateTime(2021, 7, 20, 16, 36, 0)},
        };

        private static ProfileDetail[] ProfileDetails = {
            new ProfileDetail{ProfileDetailId=1, UserId=1, ProfileDescription="Sistem admini", HiddenProfileDescription="Hesabım gizlidir. Lütfen gönderilerimi görmek için beni takip edin...", IsHiddenProfile=true, ProfileScore=48, FriendCount=0, HelpCount=0 },
            new ProfileDetail{ProfileDetailId=2, UserId=2, ProfileDescription="Genellikle matematik ödevlerine yardım edebilirim. Ücretli özel dersler de vermekteyim. Numaram: 1234567", HiddenProfileDescription="Gizli hesap. Uza.", IsHiddenProfile=false, ProfileScore=0, FriendCount=0, HelpCount=0 },
            new ProfileDetail{ProfileDetailId=3, UserId=3, ProfileDescription="Arabam ile çok seyehat ediyorum.", HiddenProfileDescription="Hesabım gizli. Çok istiyosan takip et. :/", IsHiddenProfile=true, ProfileScore=0, FriendCount=0, HelpCount=0 }
        };

        private static FriendshipCondition[] FriendshipConditions = {
            new FriendshipCondition(){ConditionId= 1, Description="reject"},
            new FriendshipCondition(){ConditionId= 2, Description="ok"},
            new FriendshipCondition(){ConditionId= 3, Description="waiting"}
        };

        private static Friendship[] Friendships = {
            new Friendship{FriendshipKey = 1, SenderUserId=2, RecipientUserId= 3, CreatedAt= new DateTime(2021, 4, 22, 16, 36, 0),FriendshipConditionId=1},
            new Friendship{FriendshipKey = 2, SenderUserId=1, RecipientUserId= 3, CreatedAt= new DateTime(2021, 1, 10, 16, 36, 0),FriendshipConditionId=2},
            new Friendship{FriendshipKey = 3, SenderUserId=3, RecipientUserId= 2, CreatedAt= new DateTime(2021, 5, 5, 16, 36, 0),FriendshipConditionId=3},
            new Friendship{FriendshipKey = 4, SenderUserId=1, RecipientUserId= 2, CreatedAt= new DateTime(2021, 8, 2, 16, 36, 0),FriendshipConditionId=2},
        };

        private static Tag[] Tags = {
            new Tag(){TagId=1, Name="Matematik",CreatorUserId=2, CreatedAt= new DateTime(2021, 5, 2, 16, 36, 0)},
            new Tag(){TagId=2, Name="Ödev",CreatorUserId=2, CreatedAt= new DateTime(2021, 4, 2, 16, 36, 0)},
            new Tag(){TagId=3, Name="Ücretli",CreatorUserId=1, CreatedAt= new DateTime(2021, 6, 2, 16, 36, 0)},
            new Tag(){TagId=4, Name="Yolculuk",CreatorUserId=1, CreatedAt= new DateTime(2021, 7, 2, 16, 36, 0)},
            new Tag(){TagId=5, Name="Yazılım",CreatorUserId=2, CreatedAt= new DateTime(2021, 8, 2, 16, 36, 0)},
            new Tag(){TagId=6, Name="Tamir/Tadilat",CreatorUserId=2, CreatedAt= new DateTime(2021, 9, 2, 16, 36, 0)},
            new Tag(){TagId=7, Name="Otostop",CreatorUserId=3, CreatedAt= new DateTime(2021, 10, 2, 16, 36, 0)},
            new Tag(){TagId=8, Name="Bedava",CreatorUserId=3, CreatedAt= new DateTime(2021, 12, 2, 16, 36, 0)},
        };

        private static TodoCondition[] TodoConditions = {
            new TodoCondition(){ConditionId = 1, Name="continued"},
            new TodoCondition(){ConditionId = 2, Name="cancelled"},
            new TodoCondition(){ConditionId = 3, Name="expired"},
            new TodoCondition(){ConditionId = 4, Name="done"}
        };

    }
}