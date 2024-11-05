using System.Results.Contracts;
using System.Results.Helpers;
using System.Text;

namespace System.Results;
public class AggregateError : Error
{
    private string? _message;

    public IReadOnlyList<IError> Errors { get; }
    public override string Message => _message ??= GetMessageBuilder().ToString();

    public AggregateError(IEnumerable<IError> errors) : base()
    {
        ThrowHelper.ThrowIfNullOrEmpty(errors);
        Errors = [.. UnwrapAllErrors(errors)];
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        if (Errors.Count == 0)
        {
            return string.Empty;
        }

        sb.Append(ErrorHelper.FormatToString(Errors[0]));
        for (int i = 1; i < Errors.Count; i++)
        {
            sb.Append(Environment.NewLine);
            sb.Append(ErrorHelper.FormatToString(Errors[i]));
        }

        return sb.ToString();
    }

    private StringBuilder GetMessageBuilder()
    {
        var sb = new StringBuilder();
        if (Errors.Count == 0)
        {
            return sb;
        }

        sb.Append(Errors[0].Message);
        for (var i = 1; i < Errors.Count; i++)
        {
            sb.Append(Environment.NewLine);
            sb.Append(Errors[i].Message);
        }

        return sb;
    }

    private static IEnumerable<IError> UnwrapAllErrors(IEnumerable<IError> errors)
    {
        foreach (var error in errors)
        {
            if (error is AggregateError aggregateError)
            {
                foreach (var childError in UnwrapAllErrors(aggregateError.Errors))
                {
                    yield return childError;
                }
            }
            else
            {
                yield return error;
            }
        }
    }
}
