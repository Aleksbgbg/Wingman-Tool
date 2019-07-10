namespace Wingman.Tool.Handlers
{
    using System.Collections.Generic;

    using CommandLine;

    public class ErrorHandler : IErrorHandler
    {
        public int HandleAndReturnExitCode(IEnumerable<Error> errors)
        {
            return -1;
        }
    }
}