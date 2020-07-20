using Autofac;
using Autofac.Integration.Mvc;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace DrawingTool.MVC.App_Start
{
    public class AutofacConfig
    {
        public static IContainer Container { get; set; }

        public static T GetInstance<T>()
        {
            return Container.Resolve<T>();
        }

        public static void Configure()
        {
            var builder = new ContainerBuilder();
            var dataAccess = Assembly.GetExecutingAssembly();
            RegisterInvoker(builder, dataAccess);
            RegisterCommands(builder, dataAccess);
            RegisterServices(builder, dataAccess);
            RegisterControllers(builder, dataAccess);
            Container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }


        private static void RegisterServices(ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
        }

        private static void RegisterInvoker(ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Invoker"))
                .AsImplementedInterfaces();
        }

        private static void RegisterCommands(ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Command"))
                .AsImplementedInterfaces();
        }

        private static void RegisterControllers(ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterControllers(assembly);
        }
    }
}