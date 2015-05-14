using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComClassLibrary.Repo
{
    public interface IRepository<TEntity, TContext> : IDisposable
        where TEntity : class
        where TContext : DbContext
    {
        //TContext DbContext { get; set; }
        DbSet<TEntity> DbSet { get; set; }
        Task<TEntity> GetByIdAsync(int id);

        /// <summary>
        /// 返回实体的Linq查询数据源
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 异步返回实体集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetAll();

        Task EditAsync(TEntity entity);
        /// <summary>
        /// 指定列进行编辑
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="includeFields">编辑哪些列的列明</param>
        /// <returns></returns>
        Task EditIncludeFieldsAsync(TEntity entity, params string[] includeFields);

        /// <summary>
        /// 排除指定的列进行编辑
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="exceptFields">不需要编辑的列</param>
        /// <returns></returns>
        Task EditExceptFieldsAsync(TEntity entity, params string[] exceptFields);
        /// <summary>
        /// 此方法调用，需要确保没有实现ICreatedOn,IIp等接口
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task InsertRangeAsync(IList<TEntity> entities);
        Task InsertAsync(TEntity entity);
        //Task InsertRangeAsync(IList<TEntity> entities);
        Task DeleteAsync(TEntity entity);
        Task DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate);
        Task DeleteRangeAsync(IList<TEntity> entities);
        Task DeleteByIdAsync(int id);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAysnc(Expression<Func<TEntity, bool>> predicate);
        Task ExecuteSqlCommand(string sql, object[] paramsObjects);
        IEnumerable<TEntity> GetRawSql(string sql, object[] paramsObjects);
        /// <summary>
        /// 使用sql语句查询返回结果集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paramsObjects"></param>
        /// <returns></returns>
        Task<List<TEntity>> SqlQuery(string sql, params SqlParameter[] paramsObjects);

        /// <summary>
        /// 根据sql语句查询返回第一行第一列
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="sql"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        Task<TType> ExcuteSclare<TType>(string sql, params SqlParameter[] pars);
    }
}
