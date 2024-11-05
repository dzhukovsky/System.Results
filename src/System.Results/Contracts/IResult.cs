using System.Diagnostics.CodeAnalysis;

namespace System.Results.Contracts;
public interface IResult
{
    [MemberNotNullWhen(false, nameof(Error))]
    bool IsSuccess => Error == null;

    [MemberNotNullWhen(true, nameof(Error))]
    bool IsFailure => !IsSuccess;

    IError? Error { get; }
}

public interface IResult<out T> : IResult
{
    T Value { get; }
}