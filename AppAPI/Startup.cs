using System.Net.Http.Headers;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

[assembly: OwinStartup(typeof(AppAPI.Startup))]

namespace AppAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // This must happen FIRST otherwise CORS will not work.
            app.UseCors(CorsOptions.AllowAll);

            // Get your HttpConfiguration. In OWIN, you'll create one
            // rather than using GlobalConfiguration.
            var config = new HttpConfiguration();

            // Itinéraires de l'API Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            SwaggerConfig.Register(config);

            // Configuration et services API Web
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));

            var container = SimpleInjectorConfig.BootStrap();

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(config);

            container.Verify();

            config.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            // OWIN WEB API SETUP:
            app.UseWebApi(config);
        }
    }
}
