using System.Diagnostics.CodeAnalysis;

namespace System.Results.Contracts;
public interface IResult
{
    [MemberNotNullWhen(false, nameof(Error))]
    bool IsSuccess { get; }

    [MemberNotNullWhen(true, nameof(Error))]
    bool IsFailure { get; }

    IError? Error { get; }
}

public interface IResult<out T> : IResult
{
    T Value { get; }
}