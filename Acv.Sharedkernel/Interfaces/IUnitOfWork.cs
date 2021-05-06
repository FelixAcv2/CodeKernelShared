using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Acv2.SharedKernel.Interfaces
{
  
    public interface IUnitOfWork
    {
        /// <summary>
        /// Commit all changes made in a container.
        /// </summary>
        ///<remarks>
        /// If the entity have fixed properties and any optimistic concurrency problem exists,  
        /// then an exception is thrown
        ///</remarks>
        void Commit();

        /// <summary>
        ///  Commit all changes made in a container.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<int> CommitAsync(string userId = null);

        /// <summary>
        /// Commit all changes made in  a container.
        /// </summary>
        ///<remarks>
        /// If the entity have fixed properties and any optimistic concurrency problem exists,
        /// then 'client changes' are refreshed - Client wins
        ///</remarks>
        void CommitAndRefreshChanges();


        /// <summary>
        /// Rollback tracked changes. See references of UnitOfWork pattern
        /// </summary>
        void RollbackChanges();



    }
}
