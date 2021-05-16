using System;
using System.Linq;
using ragther.business.Generators.Abstract;

namespace ragther.business.Generators
{
    public abstract class AbstractRandomKeyGenerator : IGenerator
    {
        public virtual string Generate()
        {
            var allChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";  
            var random = new Random();  
            var resultToken = new string(  
            Enumerable.Repeat(allChar , 32)
            .Select(token => token[random.Next(token.Length)]).ToArray());   
            
            string mailUpdateToken = resultToken.ToString()+DateTime.Now.ToString().Replace(' ','-');
            return mailUpdateToken;
        }
    }
}