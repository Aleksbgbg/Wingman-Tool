namespace Wingman.Tool.Handlers.Options
{
    using CommandLine;

    [Verb("create", HelpText = "Create a Visual Studio project.")]
    public class CreateOptions
    {
        [Option('n', "name", Required = true, HelpText = "The project's name.")]
        public string Name { get; set; }

        [Option('t', "type", Required = true, HelpText = "The project type (Available: WPF).")]
        public string ProjectType { get; set; }

        [Option('u', "unit-test", HelpText = "Whether to add a unit test project to the solution.")]
        public bool UnitTest { get; set; }

        [Option('g', "init-git", HelpText = "Whether to initialize a git repository.")]
        public bool InitGit { get; set; }

        [Option('e', "use-git-metadata", HelpText = "Whether to create .gitattributes and .gitignore metadata files.")]
        public bool UseGitMetadata { get; set; }

        [Option('d', "desc", HelpText = "The description to add to the git README file.")]
        public string ReadmeDescription { get; set; }

        [Option('m', "message", HelpText = "The commit message to use for the initial git commit.")]
        public string CommitMessage { get; set; }

        [Option('o', "git-origin", HelpText = "The URL to point the git origin remote to.")]
        public string GitRemote { get; set; }

        [Option('p', "push", HelpText = "Whether to push the initial commit to the origin remote.")]
        public bool Push { get; set; }
    }
}