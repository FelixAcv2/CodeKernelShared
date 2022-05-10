using Acv2.SharedKernel.Infraestructure.Core;
using Acv2.SharedKernel.Infraestructure.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Infraescructure
{
    public static class DependencyInjection
    {


        public static IUnityContainer AddContainerInfraestructureCustomer(this IUnityContainer container)
        {


            container.RegisterType(typeof(IQueryableUnitOfWork), typeof(SharedDbContext));

            container.RegisterType(typeof(DbContext), typeof(SharedDbContext));

            container.RegisterType(typeof(IQueryableUnitOfWork), typeof(CustomerDbContext));


            container.RegisterType(typeof(SharedDbContext), typeof(CustomerDbContext));

            container.RegisterType(typeof(IQueryableUnitOfWork), typeof(CustomerReaderDbContext));


            container.RegisterType(typeof(SharedDbContext), typeof(CustomerReaderDbContext));




            // container.RegisterType(typeof(IProductDataContext), typeof(ProductDataContext));


            return container;

        }

        public static IServiceCollection AddServiceInfraestructureCustomer(this IServiceCollection services)
        {

            services.AddTransient<IQueryableUnitOfWork,SharedDbContext > ();

            services.AddTransient<DbContext, SharedDbContext>();

            services.AddTransient<IQueryableUnitOfWork, CustomerDbContext>();

            services.AddTransient<DbContext, CustomerDbContext>();


            return services;
        }


    }
}
