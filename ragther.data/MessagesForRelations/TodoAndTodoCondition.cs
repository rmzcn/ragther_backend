namespace ragther.data.MessagesForRelations
{
    public class TodoAndTodoCondition
    {
        public static int working = 1;
        public static int cancelled = 2;
        public static int expired = 3;

        public static string getTodoConditionByID(int conID){
            switch (conID)
            {
                case 1:
                    return "working";
                case 2:
                    return "cancelled";
                case 3:
                    return "expired";
                default:
                    return "none";
            }
        }
    }
}