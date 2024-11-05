using System.Results.Contracts;

namespace System.Results.Helpers;
internal static class ErrorHelper
{
    public static string FormatToString(IError error)
    {
        return string.Format(Constants.ErrorToStringFormat, error.GetType().FullName, error.Message);
    }
}
