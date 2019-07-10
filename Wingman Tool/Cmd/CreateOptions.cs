namespace Wingman.Tool.Cmd
{
    using CommandLine;

    [Verb("create", HelpText = "Create a Visual Studio project.")]
    public class CreateOptions
    {
        [Option('t', "type", Required = true, HelpText = "The project type (e.g. WPF).")]
        public ProjectType ProjectType { get; set; }
    }
}