using Acv2.Sharedkernel.Domain;
using Acv2.SharedKernel.Infraestructure;
using Acv2.SharedKernel.Infraestructure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraescructure
{
    public class CustomerReaderDbContext : SharedDbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public CustomerReaderDbContext(IConfiguration config, DataBaseConfiguration dataBaseConfiguration, DbContextOptions<SharedDbContext> options) : base(config, dataBaseConfiguration, options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().ToTable(nameof(Customers));

        }

    }
}
