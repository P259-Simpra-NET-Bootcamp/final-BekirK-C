namespace Base.Utilities.Results;

public interface IDataResult<T> : IResult
{
    T Data { get; }
}
