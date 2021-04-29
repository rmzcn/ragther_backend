namespace ragther.Core.Utilities.Results
{
    public interface IDataResult<T> : IResult where T : class
    {
        T Data { get; }
    }
}
