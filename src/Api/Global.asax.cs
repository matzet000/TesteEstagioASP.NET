using Api.App_Start;
using Business.Intefaces;
using Business.Services;
using Data.Context;
using Data.Repository;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.Register<DataDbContext>(Lifestyle.Scoped);
            container.Register<IAlunoRepository, AlunoRepository>(Lifestyle.Scoped);
            container.Register<ITurmaRepository, TurmaRepository>(Lifestyle.Scoped);
            container.Register<IUsuarioRepository, UsuarioRepository>(Lifestyle.Scoped);
            container.Register<IAlunoTurmaRepository, AlunoTurmaRepository>(Lifestyle.Scoped);
            container.Register<IAlunoService, AlunoService>(Lifestyle.Scoped);
            container.Register<ITurmaService, TurmaService>(Lifestyle.Scoped);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);


            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
