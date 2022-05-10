
using Acv2.SharedKernel.Infraestructure;
using Acv2.SharedKernel.Infraestructure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acv2.Sharedkernel.Domain;

namespace Infraescructure
{
    public class CustomerDbContext : SharedDbContext
    {

        public DbSet<Customer> Customers { get; set; }
        public CustomerDbContext(IConfiguration config, DataBaseConfiguration dataBaseConfiguration, DbContextOptions<SharedDbContext> options) : base(config, dataBaseConfiguration, options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CustomerMapping());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // base.OnConfiguring(optionsBuilder);
           optionsBuilder.SetDataBaseConfiguration(_config.GetConnectionString("LocalConnection"),Acv2.SharedKernel.Infraestructure.Core.Enums.DataBaseTypeConfiguration.SQLSERVERCOMPAT);
        }

    }
}
