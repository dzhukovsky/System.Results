using System.Results.Contracts;

namespace System.Results;
public static class ResultExtensions
{
    public static bool HasError<T>(this IResult result)
        where T : IError
        => result.Error is T
        || result.Error is AggregateError aggregateError && aggregateError.Errors.OfType<T>().Any();
}
