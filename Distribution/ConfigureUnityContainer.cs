using Infraescructure;
using Unity;

namespace Distribution
{
    public static class ConfigureUnityContainer
    {
        static UnityContainer _container;


        public static void Configure() { 
        
             _container = new UnityContainer();

            _container.AddContainerInfraestructureCustomer();


        }

    }
}
