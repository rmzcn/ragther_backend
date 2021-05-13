namespace ragther.entity.ViewModels
{
    public class VMProfileDetailUpdatePost
    {
        public int UserId { get; set; }
        public string ProfileDescription { get; set; }
        public string HiddenProfileDescription { get; set; }
        public bool IsHiddenProfile { get; set; }
        public string ProfileImageURL { get; set; }
    }
}