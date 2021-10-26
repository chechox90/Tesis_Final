using System;
using ConductorEnRed.Controllers;
using Microsoft.Practices.Unity;
using WebApplication1.Controllers;

namespace WebApplication1.App_Start
{
    public class UnityConfig
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            IUnityContainer container = new UnityContainer();
            //var mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            //container.RegisterInstance<IMapper>(mapper);

            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            // Registrar todos los Controladores que tendrán Constructor
            ContainerBootstrapper.RegisterTypes(container);
            container.RegisterType<AccountController>();
            container.RegisterType<ReprogramacionController>();
            container.RegisterType<MantenedorController>();
            // ==================================================== 

        }
    }
}