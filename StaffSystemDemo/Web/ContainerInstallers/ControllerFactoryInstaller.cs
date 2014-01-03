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
            container.Register(Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient());//LifestyleTransient每次都是一个新的示例
            container.Register(Component.For<IControllerFactory>().ImplementedBy<WindsorControllerFactory>().LifestyleSingleton());//LifestyleSingleton单例
            container.Register(Component.For<IDbAccess>().ImplementedBy<DbAccess>().LifestylePerWebRequest());//LifestylePerWebRequest每次请求一个
            container.Register(Component.For<IStaffRepository>().ImplementedBy<StaffRepository>().LifestylePerWebRequest());
            container.Register(Component.For<IStaffService>().ImplementedBy<StaffService>().LifestylePerWebRequest());
            container.Register(Component.For<StaffSystemDBEntities>().LifestyleSingleton());

        }
    }
}
