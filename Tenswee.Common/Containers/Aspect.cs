namespace Tenswee.Common.Containers
{
    public interface IAspect
    {
        IContainer Container { get; set; }
    }

    public class Aspect : IAspect
    {
        public IContainer Container { get; set; }
    }
}
