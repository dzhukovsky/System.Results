using System.Diagnostics.CodeAnalysis;
using System.Results.Contracts;

namespace System.Results;
public readonly struct Result : IResult
{
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccess => Error == null;

    [MemberNotNullWhen(true, nameof(Error))]
    public bool IsFailure => Error != null;

    public IError? Error { get; }

    public Result()
    {
    }

    public Result(IError error)
    {
        Error = error;
    }

    public static Result Ok() => default;
    public static Result<T> Ok<T>(T value) => new(value);
    public static Result Fail(IError error) => new(error);
    public static Result<T> Fail<T>(IError error) => new(error);
    public static ResultBuilder Builder() => new();

    public static implicit operator Result(Error error) => new(error);
}

public readonly struct Result<T> : IResult<T>
{
    private readonly T _value = default!;

    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccess => Error == null;

    [MemberNotNullWhen(true, nameof(Error))]
    public bool IsFailure => Error != null;

    public IError? Error { get; }
    public T Value => IsSuccess
        ? _value
        : throw new InvalidOperationException($"Result is in failed state. Value is not set. {Error}");

    public Result(T value)
    {
        _value = value;
    }

    public Result(IError error)
    {
        Error = error;
    }

    public static implicit operator Result<T>(T value) => new(value);
    public static implicit operator Result<T>(Error error) => new(error);
    public static implicit operator Result(Result<T> result) => new(result.Error!);
}