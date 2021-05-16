using ragther.business.Generators.Abstract;

namespace ragther.business.Generators.Base
{
    public class GeneratorBase:IGenerator
    {
        IGenerator _generator;
        public GeneratorBase(IGenerator generator)
        {
            _generator = generator;
        }

        public string Generate()
        {
            return _generator.Generate();
        }
    }
}