namespace Wingman.Tool.Generation
{
    using System.IO;

    public class DirectoryManipulator : IDirectoryManipulator
    {
        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public string PathNameRelativeToDirectory(string directory, string name)
        {
            return Path.Combine(directory, name);
        }
    }
}