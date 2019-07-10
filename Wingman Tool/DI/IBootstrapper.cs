namespace Wingman.Tool.DI
{
    using Castle.MicroKernel;

    public interface IBootstrapper
    {
        T Resolve<T>();

        T Resolve<T>(Arguments arguments);
    }
}