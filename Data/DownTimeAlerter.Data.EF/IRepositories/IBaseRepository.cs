using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DownTimeAlerter.Data.EF.IRepositories {

    /// <summary>
    /// Base Repository Interface for Base Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T> {
        /// <summary>
        /// Get All Entities of T
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Get All Entities of T by Condition 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get Single Entity by Condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T GetBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get Single Entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(Guid id);

        int CountBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Add Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Add(T entity);

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Delete(T entity);

        /// <summary>
        /// Delete Entity By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteById(Guid id);

        /// <summary>
        /// Edit Entity
        /// </summary>
        /// <param name="entity"></param>
        void Edit(T entity);

        /// <summary>
        /// Save All Changes on Entities
        /// </summary>
        /// <returns></returns>
        bool Save();
    }
}
