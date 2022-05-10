using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NPoco;
using NPoco.FluentMappings;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Acv2.Sharedkernel;
using Acv2.SharedKernel.Crosscutting.Core.Logging;
using Acv2.SharedKernel.Infraestructure.Core.Interfaces;
using Acv2.SharedKernel.Core.Specification;
using Acv2.Sharedkernel.Core;

namespace Acv2.SharedKernel.Infraestructure.Core
{
    public  class SharedDbContext: DbContext, IQueryableUnitOfWork
    {
        public readonly IConfiguration _config;
        public DatabaseFactory DatabaseFactory { get; set; }
        public IDatabase IntanceDataBaseSql {

            get {

                return DatabaseFactory.GetDatabase();
            }
        
        }

        public DbSet<Audit> AuditLogs { get; set; }

        public DataBaseConfiguration DataBaseConfiguration { get; set; }

        public SharedDbContext(IConfiguration config, DataBaseConfiguration dataBaseConfiguration, DbContextOptions<SharedDbContext> options) : base(options)
        {
            _config = config;
            DataBaseConfiguration = dataBaseConfiguration;
        }

        protected SharedDbContext(IConfiguration config, DataBaseConfiguration dataBaseConfiguration, DbContextOptions contextOptions)
                   : base(contextOptions)
        {
            _config = config;
            DataBaseConfiguration = dataBaseConfiguration;
        }


        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class
        {
            //if it is not attached, attach original and set current values
            base.Entry<TEntity>(original).CurrentValues.SetValues(current);
        }

        public void Commit()
        {
            try
            {
                base.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void CommitAndRefreshChanges()
        {
            bool saveFailed = false;

            do
            {
                try
                {
                    base.SaveChanges();

                    saveFailed = false;

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                              .ForEach(entry =>
                              {
                                  entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                              });

                }
            } while (saveFailed);
        }

        public DbSet<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        [Obsolete]
        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            // return base.Database.ExecuteSqlCommand(sqlCommand, parameters);
           // return base.Database.ExecuteSqlCommand(sqlCommand, parameters);

            throw new NotImplementedException();
        }

        //public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        //{
        //    throw new NotImplementedException();
        //}


        public void RollbackChanges()
        {
            // set all entities in change tracker 
            // as 'unchanged state'
            base.ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        public void SetDbFactory(DatabaseFactory dbfactory)
        {

            if (dbfactory != null)
                DatabaseFactory = dbfactory;
        }

        public void SetDbFactory(string connectionString, DatabaseType databaseType, DbProviderFactory dbProviderFactory, FluentConfig FluentConfig = null)
        {
            var _dbfactory = NPoco.DatabaseFactory.Config(x =>
            {

                x.UsingDatabase(() => new NPoco.Database(connectionString, databaseType, dbProviderFactory));
                if (FluentConfig != null) { x.WithFluentConfig(FluentConfig); }

            });
            DatabaseFactory = _dbfactory;
        }

        public void SetModified<TEntity>(TEntity item) where TEntity : class
        {
            //this operation also attach item in object state manager
            base.Entry<TEntity>(item).State = EntityState.Modified;
        }

        void IQueryableUnitOfWork.Attach<TEntity>(TEntity item)
        {
            //attach and set as unchanged
            base.Entry<TEntity>(item).State = EntityState.Unchanged;
        }

        DbSet<TEntity> GetSet<TEntity>() where TEntity : class
        {
            return CreateSet<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> All<TEntity>() where TEntity : class
        {
            return await Task.FromResult(
                     GetSet<TEntity>().AsNoTracking());
        }

        public async Task<IEnumerable<TEntity>> AllInclude<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class
        {
            return await Task.FromResult(
                    await GetAllIncluding<TEntity>(includeProperties)
                );
        }

        private async Task<IQueryable<TEntity>> GetAllIncluding<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class
        {
            IQueryable<TEntity> queryable = GetSet<TEntity>().AsNoTracking();
            return await Task.FromResult(
                      includeProperties.Aggregate(queryable, (current, includeProperty) => current.Include(includeProperty))
                  );
        }
        public async Task<IEnumerable<TEntity>> FindByInclude<TEntity>(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class
        {
            var query = await GetAllIncluding<TEntity>(includeProperties);
            IEnumerable<TEntity> results = query.Where(predicate).ToList();

            return await Task.FromResult(
                       results
                );
        }

        public async Task<IEnumerable<TEntity>> FindBy<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            IEnumerable<TEntity> _results = GetSet<TEntity>().AsNoTracking().Where(predicate).ToList();

            return await Task.FromResult(
                   _results
                );
        }

        public async Task<TEntity> FindByKey<TEntity>(int? id) where TEntity : class
        {
            Expression<Func<TEntity, bool>> lambda = Utilities.BuildLambdaForFindByKey<TEntity>(id.Value);
            return await Task.FromResult(
                          GetSet<TEntity>().AsNoTracking().SingleOrDefault(lambda)
                );
        }

        public async Task<TEntity> FindByKey<TEntity>(Guid id) where TEntity : class
        {
            Expression<Func<TEntity, bool>> lambda = Utilities.BuildLambdaForFindByKey<TEntity>(id);
            return await Task.FromResult(
                          GetSet<TEntity>().AsNoTracking().SingleOrDefault(lambda)
                );
        }

        public async Task<IEnumerable<TEntity>> AllMatching<TEntity>(ISpecification<TEntity> specification) where TEntity : class
        {

            return await Task.FromResult(
                    GetSet<TEntity>().Where(specification.SatisfiedBy())
                );

        }

        public async Task<IEnumerable<TEntity>> GetPaged<KProperty, TEntity>(int pageIndex, int pageCount, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending) where TEntity : class
        {
            var set = GetSet<TEntity>();

            if (ascending)
            {
                return await Task.FromResult(
                       set.OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount)

                    );
            }
            else
            {
                return await Task.FromResult(
                    set.OrderByDescending(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount)

                    );
            }
        }

        public async  Task<IEnumerable<TEntity>> GetFiltered<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            return await Task.FromResult(
                          GetSet<TEntity>().Where(filter)
                );
        }


        protected  override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // string _stringConnection = string.Empty;
            // base.OnConfiguring(optionsBuilder);



            optionsBuilder.SetDataBaseConfiguration(DataBaseConfiguration);

            //switch (DataBaseConfiguration.DataBaseType)
            //{
            //    case Enums.DataBaseTypeConfiguration.ORACLE:

            //        break;
            //    case Enums.DataBaseTypeConfiguration.POSTGRESQL:
            //        break;
            //    case Enums.DataBaseTypeConfiguration.SQLSERVER:

            //        _stringConnection = $"Server={DataBaseConfiguration.ServerName};Database={DataBaseConfiguration.DataBaseName};User ID={DataBaseConfiguration.UserName};Password={DataBaseConfiguration.Password}; MultipleActiveResultSets=True";
            //        optionsBuilder.UseSqlServer(_stringConnection, options =>
            //        {
            //            options.MigrationsHistoryTable($"__{DataBaseConfiguration.Scherma}MigrationsHistory", DataBaseConfiguration.Scherma);
            //            //options.MigrationsAssembly("InfraEstructure");

            //        });
            //        break;
            //    case Enums.DataBaseTypeConfiguration.MYSQL:

            //        _stringConnection = $"server={DataBaseConfiguration.ServerName};port={DataBaseConfiguration.Port};user={DataBaseConfiguration.UserName};password={DataBaseConfiguration.Password};database={DataBaseConfiguration.DataBaseName}";
            //        optionsBuilder.UseMySQL(_stringConnection, options =>
            //        {
            //            options.MigrationsHistoryTable($"__{DataBaseConfiguration.Scherma}MigrationsHistory", DataBaseConfiguration.Scherma);
            //            // options.MigrationsAssembly("Sharedkernel.Data");
            //        });
            //        break;
            //}

        }


        private Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            foreach (var auditEntry in auditEntries)
            {
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }
                AuditLogs.Add(auditEntry.ToAudit());
            }
            return SaveChangesAsync();
        }

        private List<AuditEntry> OnBeforeSaveChanges(string userId)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntry.UserId = userId;
                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = Enums.AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.AuditType = Enums.AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = Enums.AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }
            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                AuditLogs.Add(auditEntry.ToAudit());
            }
            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        public async Task<int> CommitAsync(string userId = null)
        {
            var _rowAfect =0;
            try
            {
                var auditEntries = OnBeforeSaveChanges(userId);
                _rowAfect = await base.SaveChangesAsync();
                await OnAfterSaveChanges(auditEntries);
                return _rowAfect;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

       
    }
}
