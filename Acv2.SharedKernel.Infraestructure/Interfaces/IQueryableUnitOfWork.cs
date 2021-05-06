using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using NPoco;
using NPoco.FluentMappings;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Acv2.SharedKernel.Interfaces;
using Acv2.SharedKernel.Specification;

namespace Acv2.SharedKernel.Infraestructure.Interfaces
{
   public interface IQueryableUnitOfWork :IDisposable, IUnitOfWork, ISql
    {
        void SetDbFactory(DatabaseFactory dbfactory);
        void SetDbFactory(string connectionString, 
                          DatabaseType databaseType,
                          DbProviderFactory dbProviderFactory,
                          FluentConfig FluentConfig=null);

        /// <summary>
        /// Returns a IDbSet instance for access to entities of the given type in the context, 
        /// the ObjectStateManager, and the underlying store. 
        /// </summary>
        /// <typeparam name="TValueObject"></typeparam>
        /// <returns></returns>
        DbSet<TEntity> CreateSet<TEntity>() where TEntity : class;

     
        /// <summary>
        /// Attach this item into "ObjectStateManager"
        /// </summary>
        /// <typeparam name="TValueObject">The type of entity</typeparam>
        /// <param name="item">The item <</param>
        void Attach<TEntity>(TEntity item) where TEntity : class;

        /// <summary>
        /// Set object as modified
        /// </summary>
        /// <typeparam name="TValueObject">The type of entity</typeparam>
        /// <param name="item">The entity item to set as modifed</param>
        void SetModified<TEntity>(TEntity item) where TEntity : class;

        /// <summary>
        /// Apply current values in <paramref name="original"/>
        /// </summary>
        /// <typeparam name="TEntity">The type of entity</typeparam>
        /// <param name="original">The original entity</param>
        /// <param name="current">The current entity</param>
        void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class;


       
        Task<IEnumerable<TEntity>> All<TEntity>() where TEntity : class;
        Task<IEnumerable<TEntity>> AllInclude<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class;
        Task<IEnumerable<TEntity>> FindByInclude<TEntity>(Expression<Func<TEntity, bool>> predicate,
                                          params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class;

        Task<IEnumerable<TEntity>> FindBy<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;


        /// <summary>
        /// Get element by entity key
        /// </summary>
        /// <param name="id">Entity key value</param>
        /// <returns></returns>
        Task<TEntity> FindByKey<TEntity>(int? id) where TEntity : class;

        /// <summary>
        /// Get element by entity key
        /// </summary>
        /// <param name="id">Entity key value</param>
        /// <returns></returns>
        Task<TEntity> FindByKey<TEntity>(Guid id) where TEntity : class;


        /// <summary>
        /// Get all elements of type TEntity that matching a
        /// Specification <paramref name="specification"/>
        /// </summary>
        /// <param name="specification">Specification that result meet</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> AllMatching<TEntity>(ISpecification<TEntity> specification) where TEntity : class;

        /// <summary>
        /// Get all elements of type TEntity in repository
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageCount">Number of elements in each page</param>
        /// <param name="orderByExpression">Order by expression for this query</param>
        /// <param name="ascending">Specify if order is ascending</param>
        /// <returns>List of selected elements</returns>
        Task<IEnumerable<TEntity>> GetPaged<KProperty,TEntity>(int pageIndex, int pageCount, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending) where TEntity : class;

        /// <summary>
        /// Get  elements of type TEntity in repository
        /// </summary>
        /// <param name="filter">Filter that each element do match</param>
        /// <returns>List of selected elements</returns>
        Task<IEnumerable<TEntity>> GetFiltered<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class;




    }
}
