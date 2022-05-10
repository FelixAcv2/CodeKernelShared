using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Acv2.SharedKernel.Crosscutting.Core.Logging;
using Acv2.SharedKernel.Infraestructure.Core.Interfaces;
using Acv2.SharedKernel.Infraestructure.Core.Resources;
using Acv2.SharedKernel.Core.Interfaces;
using Acv2.SharedKernel.Core.Specification;

namespace Acv2.SharedKernel.Infraestructure
{
   public class SharedRepositoryTest<TEntity> where TEntity : class
    {
        internal  DbContext _context;
        internal DbSet<TEntity> GetSet;
        #region Members

        IQueryableUnitOfWork _UnitOfWork;

        #endregion

        public SharedRepositoryTest(DbContext context)
        {
            _context = context;
            GetSet = context.Set<TEntity>();
           // _UnitOfWork = unitOfWork;
        }

        #region ISharedRepository Members


        /// <summary>
        /// <see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/>
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _UnitOfWork;
            }
        }


        /// <summary>
        /// <see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/>
        /// </summary>
        /// <param name="entity"><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></param>
        public void Add(TEntity entity)
        {
            if (entity != (TEntity)null)
                GetSet.Add(entity); // add new item in this set
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(Messages.info_CannotAddNullEntity, typeof(TEntity).ToString());

            }
        }


        /// <summary>
        /// <see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/>
        /// </summary>
        /// <returns><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></returns>
        public async Task<IEnumerable<TEntity>> All()
        {
            return await Task.FromResult(
                    GetSet.AsNoTracking());

        }

        /// <summary>
        /// <see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/>
        /// </summary>
        ///  /// <param name="includeProperties"><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></param>
        /// <returns><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></returns>
        public async Task<IEnumerable<TEntity>> AllInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await Task.FromResult(
                    await GetAllIncluding(includeProperties)
                );
        }


        private async Task<IQueryable<TEntity>> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = GetSet.AsNoTracking();
            return await Task.FromResult(
                      includeProperties.Aggregate(queryable, (current, includeProperty) => current.Include(includeProperty))
                  );
        }

        public void Dispose()
        {
            if (_UnitOfWork != null)
                _UnitOfWork.Dispose();
        }


        /// <summary>
        /// <see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/>
        /// </summary>
        /// <param name="predicate"><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></param>
        /// <returns><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></returns>
        public async Task<IEnumerable<TEntity>> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> _results = GetSet.AsNoTracking().Where(predicate).ToList();

            return await Task.FromResult(
                   _results
                );
        }

        /// <summary>
        /// <see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/>
        /// </summary>
        /// <param name="predicate"><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></param>
        ///  /// <param name="includeProperties"><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></param>
        /// <returns><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></returns>
        public async Task<IEnumerable<TEntity>> FindByInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = await GetAllIncluding(includeProperties);
            IEnumerable<TEntity> results = query.Where(predicate).ToList();

            return await Task.FromResult(
                       results
                );
        }

        /// <summary>
        /// <see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/>
        /// </summary>
        /// <param name="id"><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></param>
        /// <returns><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></returns>
        public async Task<TEntity> FindByKey(int? id)
        {
            Expression<Func<TEntity, bool>> lambda = Utilities.BuildLambdaForFindByKey<TEntity>(id.Value);
            return await Task.FromResult(
                          GetSet.AsNoTracking().SingleOrDefault(lambda)
                );

        }
        /// <summary>
        /// <see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/>
        /// </summary>
        /// <param name="id"><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></param>
        /// <returns><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></returns>
        public async Task<TEntity> FindByKey(Guid id)
        {
            Expression<Func<TEntity, bool>> lambda = Utilities.BuildLambdaForFindByKey<TEntity>(id);
            return await Task.FromResult(
                          GetSet.AsNoTracking().SingleOrDefault(lambda)
                );

        }


        /// <summary>
        /// <see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/>
        /// </summary>
        /// <param name="filter"><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></param>
        /// <returns><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></returns>
        public async Task<IEnumerable<TEntity>> GetFiltered(Expression<Func<TEntity, bool>> filter)
        {
            return await Task.FromResult(
                          GetSet.Where(filter)
                );
        }


        /// <summary>
        /// <see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/>
        /// </summary>
        /// <typeparam name="S"><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></typeparam>
        /// <param name="pageIndex"><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></param>
        /// <param name="pageCount"><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></param>
        /// <param name="orderByExpression"><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></param>
        /// <param name="ascending"><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></param>
        /// <returns><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></returns>
        public async Task<IEnumerable<TEntity>> GetPaged<KProperty>(int pageIndex, int pageCount, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending)
        {
            var set = GetSet;

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


        /// <summary>
        /// <see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/>
        /// </summary>
        /// <param name="persisted"><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></param>
        /// <param name="current"><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></param>
        public void Merge(TEntity persisted, TEntity current)
        {
            _UnitOfWork.ApplyCurrentValues(persisted, current);
        }


        /// <summary>
        /// <see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/>
        /// </summary>
        /// <param name="entity"><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></param>
        public void Modify(TEntity entity)
        {
            if (entity != (TEntity)null)
                _UnitOfWork.SetModified(entity);

            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(Messages.info_CannotRemoveNullEntity, typeof(TEntity).ToString());
            }
        }

        /// <summary>
        /// <see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/>
        /// </summary>
        /// <param name="entity"><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></param>
        public void TrackItem(TEntity entity)
        {
            if (entity != (TEntity)null)
                _UnitOfWork.Attach<TEntity>(entity);
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(Messages.info_CannotRemoveNullEntity, typeof(TEntity).ToString());


            }
        }

        /// <summary>
        /// <see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/>
        /// </summary>
        /// <param name="entity"><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></param>
        public void Update(TEntity entity)
        {
            if (entity != (TEntity)null)
                GetSet.Update(entity);

            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(Messages.info_CannotRemoveNullEntity, typeof(TEntity).ToString());
            }
        }

        /// <summary>
        /// <see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/>
        /// </summary>
        /// <param name="entity"><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></param>
        public void Remove(TEntity entity)
        {
            if (entity != (TEntity)null)
            {
                //attach item if not exist
                _UnitOfWork.Attach(entity);

                //set as "removed"
                GetSet.Remove(entity);
            }
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(Messages.info_CannotRemoveNullEntity, typeof(TEntity).ToString());
            }
        }
        /// <summary>
        /// <see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/>
        /// </summary>
        /// <param name="id"><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></param>

        public async void Remove(int? id)
        {
            var entity = await FindByKey(id);

            if (entity != (TEntity)null)
            {
                //attach item if not exist
                _UnitOfWork.Attach(entity);

                //set as "removed"
                GetSet.Remove(entity);

                // GetSet().Remove(entity);
                //_context.SaveChanges();
            }
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(Messages.info_CannotRemoveNullEntity, typeof(TEntity).ToString());
            }
        }

        /// <summary>
        /// <see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/>
        /// </summary>
        /// <param name="id"><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></param>
        public async void Remove(Guid id)
        {
            var entity = await FindByKey(id);

            if (entity != (TEntity)null)
            {
                //attach item if not exist
                _UnitOfWork.Attach(entity);

                //set as "removed"
                GetSet.Remove(entity);

                // GetSet().Remove(entity);
                //_context.SaveChanges();
            }
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(Messages.info_CannotRemoveNullEntity, typeof(TEntity).ToString());
            }
        }



        /// <summary>
        /// <see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/>
        /// </summary>
        /// <param name="specification"><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></param>
        /// <returns><see cref="Acv2.SharedKernel.Interfaces.ISharedRepository{TValueObject}"/></returns>
        public async Task<IEnumerable<TEntity>> AllMatching(ISpecification<TEntity> specification)
        {

            return await Task.FromResult(
                    GetSet.Where(specification.SatisfiedBy())
                );

        }








        #endregion

    }
}
