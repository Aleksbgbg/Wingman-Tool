namespace Wingman.Tool.Generation
{
    using System;

    using Wingman.Tool.Cmd;

    public class ProjectGeneratorFactory : IProjectGeneratorFactory
    {
        public IProjectGenerator CreateGeneratorFor(ProjectType projectType)
        {
            switch (projectType)
            {
                case ProjectType.Wpf:
                    return new WpfProjectGenerator();

                default:
                    throw new ArgumentOutOfRangeException(nameof(projectType), projectType, "Project type not implemented.");
            }
        }
    }
}