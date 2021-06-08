using System;

namespace ragther.business.Helpers
{
    public static class JSONHelper
    {
        public static string ConvertMessageToJSONFormat(string title, string message){
            //String.Format does not working
            return "{ "+'"'+title+'"'+" : "+'"'+message+'"'+ " }";
        }

        public static string ConvertMessageToJSONFormat(string title, int no){
            //String.Format does not working
            return "{ "+'"'+title+'"'+" : "+no+" }";
        }
    }
}