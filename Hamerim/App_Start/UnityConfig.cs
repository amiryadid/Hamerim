using System.Web.Mvc;
using Hamerim.Services;
using Unity;
using Unity.Mvc5;

namespace Hamerim
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IStatisticsService, StatisticsService>();
            container.RegisterType<IPermissionsService, PermissionsService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}