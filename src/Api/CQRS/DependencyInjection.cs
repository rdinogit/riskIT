using Api.CQRS.Behaviours;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace Api.CQRS
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCqrsHandlers(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(c =>
            {
                c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                c.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });

            return services;
        }
    }
}
