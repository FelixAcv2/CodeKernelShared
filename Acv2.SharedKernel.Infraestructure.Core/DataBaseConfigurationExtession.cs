using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acv2.SharedKernel.Infraestructure.Core.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace Acv2.SharedKernel.Infraestructure.Core
{
    public static class DataBaseConfigurationExtession
    {
        public static IConfiguration _config;


        public static void SetDataBaseConfiguration(this DbContextOptionsBuilder optionsBuilder, DataBaseConfiguration dataBaseConfiguration) {


            string _stringConnection = string.Empty;
            // base.OnConfiguring(optionsBuilder);
            switch (dataBaseConfiguration.DataBaseType)
            {
                case Enums.DataBaseTypeConfiguration.ORACLE:
                    _stringConnection= $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST= {dataBaseConfiguration.ServerName})(PORT=1521))(CONNECT_DATA=(SERVICE_NAME={dataBaseConfiguration.DataBaseName}))); User Id={dataBaseConfiguration.UserName};Password={dataBaseConfiguration.Password};Enlist=false;Pooling=true;Statement Cache Size=50;";
                    optionsBuilder.UseOracle(_stringConnection, options =>
                    {
                        options.MigrationsHistoryTable($"__{dataBaseConfiguration.Scherma}MigrationsHistory", dataBaseConfiguration.Scherma);

                    });

                    break;
                case Enums.DataBaseTypeConfiguration.POSTGRESQL:
                    _stringConnection= $"Host={dataBaseConfiguration.ServerName};Username={dataBaseConfiguration.UserName};Password={dataBaseConfiguration.Password};Database={dataBaseConfiguration.DataBaseName}";
                    optionsBuilder.UseNpgsql(_stringConnection, options =>
                    {
                       // options.MigrationsHistoryTable($"__{dataBaseConfiguration.Scherma}MigrationsHistory", dataBaseConfiguration.Scherma);

                    });
                    break;
                case Enums.DataBaseTypeConfiguration.SQLSERVER:

                    _stringConnection = $"Server={dataBaseConfiguration.ServerName};Database={dataBaseConfiguration.DataBaseName};User ID={dataBaseConfiguration.UserName};Password={dataBaseConfiguration.Password}; MultipleActiveResultSets=True";
                    optionsBuilder.UseSqlServer(_stringConnection, options =>
                    {
                        options.MigrationsHistoryTable($"__{dataBaseConfiguration.Scherma}MigrationsHistory", dataBaseConfiguration.Scherma);
                        //options.MigrationsAssembly("InfraEstructure");

                    });
                    break;
                case Enums.DataBaseTypeConfiguration.MYSQL:

                    _stringConnection = $"server={dataBaseConfiguration.ServerName};port={dataBaseConfiguration.Port};user={dataBaseConfiguration.UserName};password={dataBaseConfiguration.Password};database={dataBaseConfiguration.DataBaseName}";
                    optionsBuilder.UseMySQL(_stringConnection, options =>
                    {
                        //options.EnableRetryOnFailure();
                        options.MigrationsHistoryTable($"__{dataBaseConfiguration.Scherma}MigrationsHistory", dataBaseConfiguration.Scherma);
                        // options.MigrationsAssembly("Sharedkernel.Data");
                    });
                    break;

                case Enums.DataBaseTypeConfiguration.SQLSERVERCOMPAT:

                    _stringConnection = $"Server={dataBaseConfiguration.ServerName}\\MSSQLLocalDB;Database={dataBaseConfiguration.DataBaseName};Trusted_Connection=True; MultipleActiveResultSets=True";

                   // "Server=(localdb)\\MSSQLLocalDB;Database=CustomilyData;Trusted_Connection=True;MultipleActiveResultSets=true"


                    optionsBuilder.UseSqlServer(_stringConnection, options =>
                    {
                        options.MigrationsHistoryTable($"__{dataBaseConfiguration.Scherma}MigrationsHistory", dataBaseConfiguration.Scherma);
                        //options.MigrationsAssembly("InfraEstructure");

                    });
                    break;
            }

        }

        public static void SetDataBaseConfiguration(this DbContextOptionsBuilder optionsBuilder, string connectionString,DataBaseTypeConfiguration dataBaseType)
        {



            switch (dataBaseType)
            {
                case Enums.DataBaseTypeConfiguration.ORACLE:

                    optionsBuilder.UseOracle(connectionString, options =>
                    {

                    });

                    break;
                case Enums.DataBaseTypeConfiguration.POSTGRESQL:

                    optionsBuilder.UseNpgsql(connectionString, options =>
                    {

                    });

                    break;
                case Enums.DataBaseTypeConfiguration.SQLSERVER:

                  
                    optionsBuilder.UseSqlServer(connectionString, options =>
                    {
                       
                    });
                    break;
                case Enums.DataBaseTypeConfiguration.MYSQL:

                
                    optionsBuilder.UseMySQL(connectionString, options =>
                    {
                        
                    });
                   
                    break;

                case Enums.DataBaseTypeConfiguration.SQLSERVERCOMPAT:


                    optionsBuilder.UseSqlServer(connectionString, options =>
                    {

                    });
                    break;
            }


        }

        public static void SetDataBaseConfiguration(this IServiceCollection services, DataBaseTypeConfiguration dataBaseType) {

           // var _options = new DbContextOptionsBuilder();




        }

    }
}
