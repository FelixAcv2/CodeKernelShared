using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Acv2.SharedKernel.Core.Interfaces;
using Acv2.SharedKernel.Core.Specification;

namespace Acv2.SharedKernel.Core.Interfaces
{
  public  interface ISharedRepository<TEntity>:IDisposable where TEntity:class
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<TEntity>> All();
        Task<IEnumerable<TEntity>> AllInclude(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IEnumerable<TEntity>> FindByInclude(Expression<Func<TEntity, bool>> predicate,
                                          params Expression<Func<TEntity, object>>[] includeProperties);

        Task<IEnumerable<TEntity>> FindBy(Expression<Func<TEntity, bool>> predicate);


        /// <summary>
        /// Get element by entity key
        /// </summary>
        /// <param name="id">Entity key value</param>
        /// <returns></returns>
        Task<TEntity> FindByKey(int? id);

        /// <summary>
        /// Get element by entity key
        /// </summary>
        /// <param name="id">Entity key value</param>
        /// <returns></returns>
        Task<TEntity> FindByKey(Guid id);

        /// <summary>
        /// Set item as modified
        /// </summary>
        /// <param name="entity">Item to modify</param>
        void Modify(TEntity entity);

        /// <summary>
        ///Track entity into this repository, really in UnitOfWork. 
        ///In EF this can be done with Attach and with Update in NH
        /// </summary>
        /// <param name="entity">Item to attach</param>
        void TrackItem(TEntity entity);

        /// <summary>
        /// Sets modified entity into the repository. 
        /// When calling Commit() method in UnitOfWork 
        /// these changes will be saved into the storage
        /// </summary>
        /// <param name="persisted">The persisted item</param>
        /// <param name="current">The current item</param>
        void Merge(TEntity persisted, TEntity current);


        /// <summary>
        /// Add item into repository
        /// </summary>
        /// <param name="entity">Item to add to repository</param>
        void Add(TEntity entity);

        /// <summary>
        /// Update item
        /// </summary>
        /// <param name="entity">Item to add to repository</param>
        void Update(TEntity entity);

        /// <summary>
        /// Delete item 
        /// </summary>
        /// <param name="entity">Item to delete</param>
        void Remove(TEntity entity);

        /// <summary>
        /// Delete id
        /// </summary>
        /// <param name="id">Item to delete</param>
        void Remove(int? id);

        /// <summary>
        /// Delete id
        /// </summary>
        /// <param name="id">Item to delete</param>
        void Remove(Guid id);


        /// <summary>
        /// Get all elements of type TEntity that matching a
        /// Specification <paramref name="specification"/>
        /// </summary>
        /// <param name="specification">Specification that result meet</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> AllMatching(ISpecification<TEntity> specification);

        /// <summary>
        /// Get all elements of type TEntity in repository
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageCount">Number of elements in each page</param>
        /// <param name="orderByExpression">Order by expression for this query</param>
        /// <param name="ascending">Specify if order is ascending</param>
        /// <returns>List of selected elements</returns>
        Task<IEnumerable<TEntity>> GetPaged<KProperty>(int pageIndex, int pageCount, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending);

        /// <summary>
        /// Get  elements of type TEntity in repository
        /// </summary>
        /// <param name="filter">Filter that each element do match</param>
        /// <returns>List of selected elements</returns>
        Task<IEnumerable<TEntity>> GetFiltered(Expression<Func<TEntity, bool>> filter);



    }
}
