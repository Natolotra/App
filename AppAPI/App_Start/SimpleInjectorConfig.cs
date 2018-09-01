using App.Queries.Trajet;
using SimpleInjector;
using SimpleInjector.Lifestyles;
namespace AppAPI
{
    public class SimpleInjectorConfig
    {
        public static Container BootStrap()
        {
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Register your types, for instance using the scoped lifestyle:
            container.Register<ITrajetQueries>(() => new TrajetQueries(ConfigSettings.ConnectionString));

            return container;
        }
    }
}