namespace ragther.data.MessagesForRelations
{
    public class FriendshipAndFriendshipCondition
    {
        // example: if friend = friendship condition table value = 1
        public static int NotExistTableValue = 0;
        public static int Friend = 1;
        public static int RequestWaiting = 2;
        public static string getFriendshipConditionNameByID(int conID){
            switch (conID)
            {
                case 0:
                    return "none";
                case 1:
                    return "friends";
                case 2:
                    return "waiting";
                default:
                    return "none";
            }
        }
        public static int getFriendshipConditionIdByName(string conditionName){
            switch (conditionName)
            {
                case "none":
                    return 0;
                case "friends":
                    return 1;
                case "waiting":
                    return 2;
                default:
                    return 0;
            }
        }
    }

}