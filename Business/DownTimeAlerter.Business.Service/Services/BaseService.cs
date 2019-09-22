using DownTimeAlerter.Business.Service.IServices;
using DownTimeAlerter.Data.EF.IRepositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DownTimeAlerter.Business.Service.Services {

    /// <summary>
    /// Base Service for All Services
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseService<T> : IBaseService<T> where T : class, new() {

        public IBaseRepository<T> Repository { get; set; }
        public ILogger<T> Logger { get; }

        public BaseService(IBaseRepository<T> repository, ILogger<T> logger) {
            Repository = repository;
            Logger = logger;
        }

        /// <summary>
        /// Get All Entities of T
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll() {
            var results = Repository.GetAll();
            return results;
        }

        /// <summary>
        /// Get All Entities of T by Condition 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate) {
            var results = Repository.FindBy(predicate);
            return results;

        }

        /// <summary>
        /// Get Single Entity by Condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T GetBy(Expression<Func<T, bool>> predicate) {
            var result = Repository.GetBy(predicate);
            return result;
        }

        /// <summary>
        /// Get Single Entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(Guid id) {
            return Repository.GetById(id);
        }

        /// <summary>
        /// Get Count By filter
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int CountBy(Expression<Func<T, bool>> predicate) {
            return Repository.CountBy(predicate);
        }

        /// <summary>
        /// Add Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Add(T entity) {
            var result = Repository.Add(entity);
            return result;
        }

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(T entity) {
            var result = Repository.Delete(entity);
            return result > 0;
        }

        /// <summary>
        /// Delete Entity By Id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteById(Guid id) {
            var result = Repository.DeleteById(id);
            return result > 0;
        }

        /// <summary>
        /// Edit Entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Edit(T entity) {
            Repository.Edit(entity);
        }

        /// <summary>
        /// Save All Changes on Entities
        /// </summary>
        /// <returns></returns>
        public bool Save() {
            return Repository.Save();
        }


    }
}
