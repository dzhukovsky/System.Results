using System.Results.Contracts;
using System.Results.Helpers;

namespace System.Results;
public class Error : IError
{
    public virtual string Message { get; }

    public Error(string message)
    {
        Message = message;
    }

    protected Error()
    {
        Message = string.Empty;
    }

    public override string ToString() =>
        ErrorHelper.FormatToString(this);
}
