namespace Wingman.Tool.Generation.FileIO
{
    public interface IDirectoryManipulator
    {
        void CreateDirectory(string path);

        string PathNameRelativeToDirectory(string directory, string name);
    }
}