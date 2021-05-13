namespace ragther.data.MessagesForRelations
{
    public static class NoticeTypes
    {
        public static int Comment = 1;
        public static int Like = 2;
        public static int FriendshipRequest = 3;
        public static int Remind = 4;
        public static int FriendshipReject = 5;
        public static int FriendshipAcceppted = 6;
        public static int OfferAccepted = 7;
        public static int OfferRejected = 8;
        public static int LookedProfile = 9;

        public static string getNoticeTypeByID(int conID){
            switch (conID)
            {
                case 1:
                    return "comment";
                case 2:
                    return "like";
                case 3:
                    return "friendship";
                case 4:
                    return "remind";
                case 5:
                    return "friendship reject";
                case 6:
                    return "friendship accepted";
                case 7:
                    return "offer accepted";
                case 8:
                    return "offer rejected";
                case 9:
                    return "looked profile";
                default:
                    return "none";
            }
        }
    }
}