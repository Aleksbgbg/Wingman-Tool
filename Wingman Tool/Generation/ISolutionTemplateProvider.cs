﻿namespace Wingman.Tool.Generation
{
    using System.Threading.Tasks;

    public interface ISolutionTemplateProvider
    {
        Task<FileTreeTemplate> TemplateFor(string projectType);

        Task<RenderedFileTreeEntry> RenderFileTreeEntry(string projectType, string projectName, FileTreeEntry entry);
    }
}