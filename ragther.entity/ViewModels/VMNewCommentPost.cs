namespace ragther.entity.ViewModels
{
    public class VMNewCommentPost
    {
        public int TodoId { get; set; }
        public int UserId { get; set; }
        public bool IsOffer { get; set; }
        public string Content { get; set; }
        
        // {
        //     "TodoId":37,
        //     "UserId":6,
        //     "IsOffer": true,
        //     "Content":"İlk yorum benden"
        // }
    }
}