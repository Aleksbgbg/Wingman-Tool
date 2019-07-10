namespace Wingman.Tool.Generation
{
    public interface IDirectoryManipulator
    {
        void CreateDirectory(string path);

        string PathNameRelativeToDirectory(string directory, string name);
    }
}