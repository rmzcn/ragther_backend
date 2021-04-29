using ragther.entity.Abstract;

namespace ragther.entity.ViewModels
{
    public class VMUserProfileDetailGet:IVModel
    {
        public string ProfileDescription { get; set; }
        public string HiddenProfileDescription { get; set; }
        public bool IsHiddenProfile { get; set; }
        public int ProfileScore { get; set; }
        public int FriendCount { get; set; }
        public int HelpCount { get; set; }
    }
}