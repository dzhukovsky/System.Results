using System.Diagnostics.CodeAnalysis;
using System.Results.Contracts;

namespace System.Results;
public class Result : IResult
{
    private static readonly Result ResultOk = new();
    
    public IError? Error { get; }

    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccess => Error == null;

    [MemberNotNullWhen(true, nameof(Error))]
    public bool IsFailure => Error != null;

    public Result()
    {
    }

    public Result(IError error)
    {
        ArgumentNullException.ThrowIfNull(error);
        Error = error;
    }

    public bool HasError<T>() where T : IError => Error is T;
    public bool HasError<T>([NotNullWhen(true)] out T? error) where T : IError
    {
        if (Error is T err)
        {
            error = err;
            return true;
        }

        error = default;
        return false;
    }

    public static Result Ok() => ResultOk;
    public static Result Fail(IError error) => new(error);

    public static Result<T> Ok<T>(T value) => new(value);
    public static Result<T> Fail<T>(IError error) => new(error);

    public static implicit operator Result(Error error) => new(error);
}

public class Result<T> : Result, IResult<T>
{
    private readonly T _value = default!;

    public T Value => IsSuccess
        ? _value
        : throw new InvalidOperationException($"Result is in failed state. Value is not set.");

    public Result(T value)
    {
        _value = value;
    }

    public Result(IError error) : base(error)
    {
    }

    public static implicit operator Result<T>(T value) => new(value);
    public static implicit operator Result<T>(Error error) => new(error);
}