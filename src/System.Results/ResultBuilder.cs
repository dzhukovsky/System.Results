using System.Results.Contracts;

namespace System.Results;
public class ResultBuilder(bool continueWhenFail = false)
{
    private readonly IList<IError> _errors = [];

    protected bool CanContinue => _errors.Count == 0 || continueWhenFail;

    public ResultBuilder FailIf(bool condition, IError error)
    {
        ArgumentNullException.ThrowIfNull(error);

        if (CanContinue && condition)
        {
            _errors.Add(error);
        }

        return this;
    }

    public ResultBuilder FailIf(Func<bool> condition, IError error)
    {
        ArgumentNullException.ThrowIfNull(condition);
        ArgumentNullException.ThrowIfNull(error);

        if (CanContinue && condition())
        {
            _errors.Add(error);
        }

        return this;
    }

    public ResultBuilder FailIf<T>(T argument, Func<T, bool> condition, IError error)
    {
        ArgumentNullException.ThrowIfNull(condition);
        ArgumentNullException.ThrowIfNull(error);

        if (CanContinue && condition(argument))
        {
            _errors.Add(error);
        }

        return this;
    }

    public ResultBuilder WithResult(Result result)
    {
        if (CanContinue && result.IsFailure)
        {
            _errors.Add(result.Error!);
        }

        return this;
    }

    public ResultBuilder WithResult<T>(Result<T> result)
    {
        if (CanContinue && result.IsFailure)
        {
            _errors.Add(result.Error!);
        }

        return this;
    }

    public ResultBuilder WithResult(Func<Result> resultFunc)
    {
        ArgumentNullException.ThrowIfNull(resultFunc);

        if (!CanContinue)
            return this;

        var result = resultFunc();
        if (result.IsFailure)
        {
            _errors.Add(result.Error!);
        }

        return this;
    }

    public ResultBuilder WithResult<T>(Func<Result<T>> resultFunc)
    {
        ArgumentNullException.ThrowIfNull(resultFunc);

        if (!CanContinue)
            return this;

        var result = resultFunc();
        if (result.IsFailure)
        {
            _errors.Add(result.Error!);
        }

        return this;
    }

    public Result Build()
    {
        return _errors.Count switch
        {
            0 => Result.Ok(),
            1 => Result.Fail(_errors[0]),
            _ => new AggregateError(_errors)
        };
    }

    public Result<T> Build<T>(T value)
    {
        return _errors.Count switch
        {
            0 => Result.Ok(value),
            1 => Result.Fail<T>(_errors[0]),
            _ => new AggregateError(_errors)
        };
    }
}
