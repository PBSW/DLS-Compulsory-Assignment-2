using System.Runtime.CompilerServices;
using Serilog;

namespace Shared.Monitoring;

public static class LoggerExtensions
{
    public static ILogger Here(this ILogger logger,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0)
    {
        return logger
            .ForContext("MemberName", memberName)
            .ForContext("FilePath", filePath)
            .ForContext("LineNumber", lineNumber);
    }
}