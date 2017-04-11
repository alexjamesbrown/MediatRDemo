using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using CashJobSite.Application.Features.AddJob;
using CashJobSite.Application.Logging;
using CashJobSite.Application.Services;
using CashJobSite.Data;
using MediatR;
using MediatR.Pipeline;

namespace CashJobSite.Web
{
    public class AutofacConfig
    {
        public static void BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<CashJobSiteDbContext>().As<ICashJobSiteDbContext>().InstancePerRequest();

            builder.RegisterType<EmailService>().As<IEmailService>();
            builder.RegisterType<Logger>().As<ILogger>();

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            //this is probably also causing issues with behaviours
            builder.RegisterAssemblyTypes(typeof(AddJobCommand).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            //behaviours
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            //registering them individually seems to cause issues
            //https://github.com/jbogard/MediatR/issues/128
            //builder.RegisterType<AddJobApplicationLoggingHandler>().As<IPipelineBehavior<AddJobApplicationCommand, Unit>>();
            //builder.RegisterType<AddJobValidationHandler>().As<IRequestPreProcessor<AddJobCommand>>();

            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t =>
                {
                    object o;
                    return c.TryResolve(t, out o) ? o : null;
                };
            });

            builder.Register<MultiInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            });

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}