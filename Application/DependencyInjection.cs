using Acv2.SharedKernel.Application.Core.Behaviours;
using Acv2.SharedKernel.Crosscutting.Core.Adapter;
using Acv2.SharedKernel.Crosscutting.Core.Logging;
using Acv2.SharedKernel.Crosscutting.Core.Validator;
using Acv2.SharedKernel.Crosscutting.NetFramerwork.Core.Adapter;
using Acv2.SharedKernel.Crosscutting.NetFramerwork.Core.Logging;
using Acv2.SharedKernel.Crosscutting.NetFramerwork.Core.Validator;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace Application
{
    public static class DependencyInjection
    {

        public static IUnityContainer AddContainerApplicationCustomer(this IUnityContainer container)
        {

            //-> Adapters
            container.RegisterType<ITypeAdapterFactory, AutomapperTypeAdapterFactory>(new ContainerControlledLifetimeManager());

            LoggerFactory.SetCurrent(new TraceSourceLogFactory());
            EntityValidatorFactory.SetCurrent(new DataAnnotationsEntityValidatorFactory());
            //  AutomapperTypeAdapter

            var typeAdapterFactory = container.Resolve<ITypeAdapterFactory>();
            TypeAdapterFactory.SetCurrent(typeAdapterFactory);

            return container;

        }

        public static IServiceCollection AddServiceApplicationCustomer(this IServiceCollection services)
        {



            services.AddMediatR(Assembly.GetExecutingAssembly());
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }


    }
}
