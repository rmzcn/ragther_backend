using System;
using System.Linq;
using ragther.business.Generators.Abstract;

namespace ragther.business.Generators
{
    public class MailTokenCreator:AbstractRandomKeyGenerator,IGenerator
    {
        public MailTokenCreator() { }

        public override string Generate()
        {
            //other mail thinks
            return base.Generate();
        }
    }
}