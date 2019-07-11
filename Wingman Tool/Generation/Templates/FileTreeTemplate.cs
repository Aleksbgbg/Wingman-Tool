namespace Wingman.Tool.Generation.Templates
{
    public class FileTreeTemplate
    {
        public FileTreeTemplate(FileTreeEntry[] entries)
        {
            Entries = entries;
        }

        public FileTreeEntry[] Entries { get; }
    }
}