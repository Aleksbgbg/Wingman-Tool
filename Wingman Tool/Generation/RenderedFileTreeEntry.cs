namespace Wingman.Tool.Generation
{
    public class RenderedFileTreeEntry
    {
        public RenderedFileTreeEntry(string relativePath, bool isDirectory, string contents)
        {
            RelativePath = relativePath;
            IsDirectory = isDirectory;
            Contents = contents;
        }

        public string RelativePath { get; }

        public bool IsDirectory { get; }

        public string Contents { get; }
    }
}