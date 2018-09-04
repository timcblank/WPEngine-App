using System.Linq;
using System.Reflection;
using Autofac;
using Services;

namespace WPEngine_App
{
    public class ProcessorRegistrationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Controller"))
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name));
        }

        public void Execute(ContainerBuilder builder)
        {
            // create a singleton instance of the logger class
            builder.RegisterType<Logger>().As<ILogger>().SingleInstance();
            this.Load(builder);
        }
    }
}
