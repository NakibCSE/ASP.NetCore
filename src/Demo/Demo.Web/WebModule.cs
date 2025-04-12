using Autofac;
using Demo.Web.Models;

namespace Demo.Web
{
    public class WebModule :Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Item>().As<IItem>().InstancePerLifetimeScope();
            //builder.RegisterType<Item>().AsSelf().InstancePerLifetimeScope(); // OR
            base.Load(builder);
        }
    }
}
