﻿using System.Diagnostics.CodeAnalysis;
using System.Results.Contracts;

namespace System.Results;
public class ErrorCollector
{
    private readonly List<IError> _errors = [];
    public IReadOnlyList<IError> Errors { get; }

    public ErrorCollector()
    {
        Errors = _errors.AsReadOnly();
    }

    public ErrorCollector WithResult(IResult result)
    {
        ArgumentNullException.ThrowIfNull(result);
        if (result.IsFailure)
        {
            _errors.Add(result.Error);
        }
        return this;
    }

    public ErrorCollector WithResults(IEnumerable<IResult> results)
    {
        ArgumentNullException.ThrowIfNull(results);
        WithErrors(results.Where(x => x.IsFailure).Select(x => x.Error!));
        return this;
    }

    public ErrorCollector WithError(IError error)
    {
        ArgumentNullException.ThrowIfNull(error);
        _errors.Add(error);
        return this;
    }

    public ErrorCollector WithErrors(IEnumerable<IError> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);
        _errors.AddRange(errors.Where(x => x != null));
        return this;
    }

    public bool HasError<T>() where T : IError
    {
        for (int i = 0; i < _errors.Count; i++)
        {
            if (_errors[i] is T)
            {
                return true;
            }
        }

        return false;
    }

    public bool HasError<T>([NotNullWhen(true)] out T? error) where T : IError
    {
        for (int i = 0; i < _errors.Count; i++)
        {
            if (_errors[i] is T err)
            {
                error = err;
                return true;
            }
        }

        error = default;
        return false;
    }

    public bool HasErrors<T>(out IEnumerable<T> errors) where T : IError
    {
        errors = Errors.OfType<T>();
        return HasError<T>();
    }
}