using Autofac;

namespace WPEngine_App
{

    class Program
    {
        // Autofac container to register classes for the builder for dependency injection
        static private IContainer RegisterationRoot(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>().WithParameter(new NamedParameter("args", args));

            // this is a reflection service that will loop through asemblies in the application and register them with autofac
            var reflectionBuilder = new ProcessorRegistrationModule();
            reflectionBuilder.Execute(builder);
            return builder.Build();
        }

        static void Main(string[] args)
        {
            RegisterationRoot(args).Resolve<Application>().Run();
        }
    }
}
