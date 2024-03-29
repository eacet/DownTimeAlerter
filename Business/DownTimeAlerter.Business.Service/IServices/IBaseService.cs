﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DownTimeAlerter.Business.Service.IServices {
    /// <summary>
    /// Base Service for All Services
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseService<T> {
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

        /// <summary>
        /// Get Count By Filter
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
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
        bool Delete(T entity);

        /// <summary>
        /// Delete Entity By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteById(Guid id);

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
