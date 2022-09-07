using Autofac;
using FluentValidation;
using MediatR;
using UserService.API.Common.Behaviors;
using System.Reflection;

namespace UserService.API.Infrastructure.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(MediatorModule).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            //Register the Portion's Validators
            builder
                .RegisterAssemblyTypes(typeof(MediatorModule).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });

            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}
