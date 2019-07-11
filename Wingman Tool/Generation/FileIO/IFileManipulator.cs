namespace Wingman.Tool.Generation.FileIO
{
    public interface IFileManipulator
    {
        void CreateFile(string path, string contents);
    }
}