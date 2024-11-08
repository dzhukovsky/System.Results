namespace System.Results.Contracts;
public interface IError
{
    string Message { get; }
    public IReadOnlyList<object?> Metadata { get; }
}
