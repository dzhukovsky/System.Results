namespace System.Results.Tests;
public class ResultTests
{
    #region Result

    [Fact]
    public void Ok_ShouldReturnSuccessResult()
    {
        var result = Result.Ok();

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Null(result.Error);
    }

    [Fact]
    public void Fail_ShouldReturnFailureResult_WithSpecifiedError()
    {
        var error = new Error("Failure");
        var result = Result.Fail(error);

        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.Equal(error, result.Error);
    }

    [Fact]
    public void ImplicitConversionFromError_ShouldCreateFailureResult()
    {
        var error = new Error("Implicit Error");
        Result result = error;

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);
    }

    [Fact]
    public void HasError_ShouldReturnTrue_IfErrorOfTypeExists()
    {
        var error = new Error("Test error");
        var result = Result.Fail(error);

        Assert.True(result.HasError<Error>());
    }

    [Fact]
    public void HasError_ShouldReturnFalse_IfErrorOfTypeDoesNotExist()
    {
        var error = new Error("Test error");
        var result = Result.Fail(error);

        Assert.False(result.HasError<AggregateError>());
    }

    [Fact]
    public void HasError_ShouldReturnTrue_IfErrorExistsInAggregateError()
    {
        var error1 = new Error("Error 1");
        var error2 = new Error("Error 2");
        var aggregateError = new AggregateError([error1, error2]);
        var result = Result.Fail(aggregateError);

        Assert.True(result.HasError<Error>());
    }

    #endregion

    #region Result<T>

    [Fact]
    public void Ok_WithValue_ShouldReturnSuccessResult_WithCorrectValue()
    {
        var value = 123;
        var result = Result.Ok(value);

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void Fail_WithValue_ShouldReturnFailureResult_WithSpecifiedError()
    {
        var error = new Error("Failure");
        var result = Result.Fail<int>(error);

        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.Equal(error, result.Error);
    }

    [Fact]
    public void ImplicitConversionValueResultFromValue_ShouldCreateSuccessResult()
    {
        int value = 100;
        Result<long> result = value;

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void ImplicitConversionValueResultFromError_ShouldCreateFailureResult()
    {
        Error error = new("Implicit Error");
        Result<int> result = error;

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);
    }

    [Fact]
    public void Value_ShouldThrowInvalidOperationException_WhenAccessedOnFailure()
    {
        var error = new Error("Failure Error");
        var result = Result.Fail<string>(error);

        Assert.Throws<InvalidOperationException>(() => result.Value);
    }

    [Fact]
    public void Value_ShouldReturnProvidedValue_WhenResultIsSuccess()
    {
        var value = "TestValue";
        var result = Result.Ok(value);

        Assert.Equal(value, result.Value);
    }

    #endregion
}
