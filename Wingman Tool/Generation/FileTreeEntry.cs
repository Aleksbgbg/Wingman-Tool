namespace Wingman.Tool.Generation
{
    public class FileTreeEntry
    {
        public FileTreeEntry(string relativePath, bool isDirectory)
        {
            RelativePath = relativePath;
            IsDirectory = isDirectory;
        }

        public string RelativePath { get; }

        public bool IsDirectory { get; }
    }
}