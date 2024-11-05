namespace System.Results.Tests;
public class ResultBuilderTests
{
    [Fact]
    public void Build_NoErrors_ShouldReturnOkResult()
    {
        var builder = new ResultBuilder();
        var result = builder.Build();

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Build_SingleError_ShouldReturnFailResultWithThatError()
    {
        var error = new Error("Single error");
        var builder = new ResultBuilder().FailIf(true, error);
        var result = builder.Build();

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);
    }

    [Fact]
    public void Build_WithContinueWhenFailFalse_ShouldReturnFirstErrorOnly()
    {
        var error1 = new Error("Error 1");
        var error2 = new Error("Error 2");
        var builder = new ResultBuilder(false)
            .FailIf(true, error1)
            .FailIf(true, error2);

        var result = builder.Build();

        Assert.True(result.IsFailure);
        Assert.IsNotType<AggregateError>(result.Error);
        Assert.Equal(error1, result.Error);
        Assert.NotEqual(error2, result.Error);
    }


    [Fact]
    public void Build_WithContinueWhenFailTrue_ShouldReturnFailResultWithAggregateError()
    {
        var error1 = new Error("Error 1");
        var error2 = new Error("Error 2");
        var builder = new ResultBuilder(true)
            .FailIf(true, error1)
            .FailIf(true, error2);

        var result = builder.Build();

        Assert.True(result.IsFailure);
        Assert.IsType<AggregateError>(result.Error);
        Assert.Equal(error1, (result.Error as AggregateError)?.Errors[0]);
        Assert.Equal(error2, (result.Error as AggregateError)?.Errors[1]);
    }

    [Fact]
    public void Build_WithValue_NoErrors_ShouldReturnOkResultWithValue()
    {
        var builder = new ResultBuilder();
        var result = builder.Build(123);

        Assert.True(result.IsSuccess);
        Assert.Equal(123, result.Value);
    }
}
