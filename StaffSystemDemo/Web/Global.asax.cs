using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using StaffSystemDemo.Web.App_Start;
using Web;
using Castle.MicroKernel.Registration;
using StaffSystemData.DataContext;
using Repository.Repository;
using StaffSystemService.Service;
using StaffSystemData.DataBase;

namespace StaffSystemDemo.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer _container;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
    

            _container = new WindsorContainer();
            _container.Install(FromAssembly.This());

            _container.Register(Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient());
            _container.Register(Component.For<IDbAccess>().ImplementedBy<DbAccess>().LifestylePerWebRequest());
            _container.Register(Component.For<IStaffRepository>().ImplementedBy<StaffRepository>().LifestylePerWebRequest());
            _container.Register(Component.For<IStaffService>().ImplementedBy<StaffService>().LifestylePerWebRequest());
            _container.Register(Component.For<StaffSystemDBEntities>().LifestyleSingleton());

            var controllerFactory = new WindsorControllerFactory(_container);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        protected void Application_End()
        {
            if (_container != null)
            {
                _container.Dispose();
            }

        }

    }
}