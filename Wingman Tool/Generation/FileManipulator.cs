namespace Wingman.Tool.Generation
{
    using System.IO;

    public class FileManipulator : IFileManipulator
    {
        public void CreateFile(string path, string contents)
        {
            File.WriteAllText(path, contents);
        }
    }
}