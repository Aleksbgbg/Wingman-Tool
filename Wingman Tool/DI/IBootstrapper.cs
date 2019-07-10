namespace Wingman.Tool.DI
{
    public interface IBootstrapper
    {
        T Resolve<T>();
    }
}