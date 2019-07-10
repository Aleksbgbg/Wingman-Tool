namespace Wingman.Tool.Cmd
{
    using CommandLine;

    [Verb("create", HelpText = "Create a Visual Studio project.")]
    public class CreateOptions
    {
        [Option('n', "name", Required = true, HelpText = "The project's name.")]
        public string Name { get; set; }

        [Option('t', "type", Required = true, HelpText = "The project type (e.g. WPF).")]
        public ProjectType ProjectType { get; set; }

        [Option('g', "use-git", HelpText = "Whether or not to initialize a git repository.")]
        public bool UseGit { get; set; }

        [Option('d', "desc", HelpText = "The description to add to the git README file.")]
        public string ReadmeDescription { get; set; }

        [Option('r', "git-remote", HelpText = "The remote to add to the git repository.")]
        public string GitRemote { get; set; }
    }
}