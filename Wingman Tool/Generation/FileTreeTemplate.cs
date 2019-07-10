namespace Wingman.Tool.Generation
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