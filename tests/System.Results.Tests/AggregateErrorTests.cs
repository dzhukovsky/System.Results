namespace System.Results.Tests;
public class AggregateErrorTests
{
    [Fact]
    public void AggregateError_ShouldContainAllChildErrors()
    {
        IReadOnlyList<Error> errors = [
               new ("Error 1"),
               new ("Error 2"),
               new ("Error 3"),
               new ("Error 4"),
               new ("Error 5"),
               new ("Error 6"),
            ];

        var error = new AggregateError([
               errors[0],
                new AggregateError([
                    errors[1],
                    errors[2],
                    new AggregateError([
                        errors[3],
                        errors[4],
                    ]),
                ]),
                errors[5],
            ]);

        Assert.Equal(6, error.Errors.Count);
        Assert.Equal(errors, error.Errors);
    }

    [Fact]
    public void AggregateError_Message_ShouldConcatenateAllChildErrorMessages()
    {
        var error = new AggregateError([
               new Error("Error 1"),
                new AggregateError([
                    new Error("Error 2"),
                    new Error("Error 3"),
                    new AggregateError([
                        new Error("Error 4"),
                        new Error("Error 5"),
                    ]),
                ]),
                new Error("Error 6"),
            ]);

        Assert.Equal("""
            Error 1
            Error 2
            Error 3
            Error 4
            Error 5
            Error 6
            """, error.Message);
    }

    [Fact]
    public void AggregateError_ToString_ShouldIncludeTypeAndMessageForEachError()
    {
        var error = new AggregateError([
               new Error("Error 1"),
                new AggregateError([
                    new Error("Error 2"),
                    new Error("Error 3"),
                    new AggregateError([
                        new Error("Error 4"),
                        new Error("Error 5"),
                    ]),
                ]),
                new Error("Error 6"),
            ]);

        Assert.Equal($"""
            [{typeof(Error).FullName}]: Error 1
            [{typeof(Error).FullName}]: Error 2
            [{typeof(Error).FullName}]: Error 3
            [{typeof(Error).FullName}]: Error 4
            [{typeof(Error).FullName}]: Error 5
            [{typeof(Error).FullName}]: Error 6
            """, error.ToString());
    }
}
