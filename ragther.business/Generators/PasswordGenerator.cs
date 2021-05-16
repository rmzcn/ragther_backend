using System;
using System.Linq;

namespace ragther.business.Generators
{
    public class PasswordGenerator
    {
        public virtual string Generate()
        {
            var allChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghıjklmnopqrstuvwxyz0123456789";  
            var random = new Random();  
            var resultToken = new string(  
            Enumerable.Repeat(allChar , 10)
            .Select(token => token[random.Next(token.Length)]).ToArray());   
            
            string newPassword = resultToken;
            return newPassword;
        }
    }
}