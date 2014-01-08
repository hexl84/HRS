using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Repository.Repository;
using StaffSystemData.DataBase;
using StaffSystemData.DataContext;
using StaffSystemService.Service;

namespace StaffSystemDemo.Web.ContainerInstallers
{
    public class ControllerFactoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IControllerFactory>().ImplementedBy<WindsorControllerFactory>().LifestyleSingleton());
        }
    }
}
