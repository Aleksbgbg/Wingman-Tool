namespace Wingman.Tool.Handlers
{
    using System.Collections.Generic;

    using CommandLine;

    public interface IErrorHandler
    {
        int HandleAndReturnExitCode(IEnumerable<Error> errors);
    }
}