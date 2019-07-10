namespace Wingman.Tool.Generation
{
    public interface IFileManipulator
    {
        void CreateFile(string path, string contents);
    }
}