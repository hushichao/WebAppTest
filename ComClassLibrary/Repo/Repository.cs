using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ComClassLibrary.Repo
{
    public class Repo<TEntity, TContext> : IRepository<TEntity, TContext>, IDisposable
        where TEntity : class
        where TContext : DbContext
    {
        public DbSet<TEntity> DbSet { get; set; }
        //public TContext DbContext { get; set; }

        private readonly DbContext DbContext;


        public Repo(TContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        /// <summary>
        /// for non-query commands
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paramsObjects"></param>
        /// <returns></returns>
        public async Task ExecuteSqlCommand(string sql, object[] paramsObjects)
        {
            await DbContext.Database.ExecuteSqlCommandAsync(sql, paramsObjects);
        }

        /// <summary>
        /// 默认automatically tracked by the database context unless you turn tracking off. (AsNoTracking method.)
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paramsObjects"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetRawSql(string sql, object[] paramsObjects)
        {
            return DbSet.SqlQuery(sql, paramsObjects);
        }

        public IQueryable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<int> CountAysnc(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate).CountAsync();
        }

        public async Task EditAsync(TEntity entity)
        {
         
            DbContext.Entry(entity).State = EntityState.Modified;

            await DbContext.SaveChangesAsync();
        }

        public async Task EditIncludeFieldsAsync(TEntity entity, params string[] includeFields)
        {
            DbEntityEntry entry = DbContext.Entry<TEntity>(entity);
            entry.State = System.Data.Entity.EntityState.Unchanged;

  
           
            foreach (string proName in includeFields)
                entry.Property(proName).IsModified = true;
            await DbContext.SaveChangesAsync();
        }

        public async Task EditExceptFieldsAsync(TEntity entity, params string[] exceptFields)
        {
            DbEntityEntry entry = DbContext.Entry<TEntity>(entity);
            entry.State = System.Data.Entity.EntityState.Modified;

        
            foreach (string proName in exceptFields)
                entry.Property(proName).IsModified = false;
            await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 此方法调用，需要确保将实现ICreatedId，ICreatedOn,IIp等接口的属性赋值后插入。
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task InsertRangeAsync(IList<TEntity> entities)
        {
            DbSet.AddRange(entities);
            await DbContext.SaveChangesAsync();
        }

        public async Task InsertAsync(TEntity entity)
        {
         
          
          
            DbSet.Add(entity);

            DbContext.Entry(entity).State = EntityState.Added;

            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var range = DbSet.Where(predicate);
            DbSet.RemoveRange(range);
            await DbContext.SaveChangesAsync();

        }

        public async Task DeleteRangeAsync(IList<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
            await DbContext.SaveChangesAsync();
        }
        public async Task DeleteByIdAsync(int id)
        {
            var entity = await DbSet.FindAsync(id);
            DbSet.Remove(entity);
            await DbContext.SaveChangesAsync();
        }
        public async Task<List<TEntity>> SqlQuery(string sql, params SqlParameter[] pars)
        {
            return await DbContext.Database.SqlQuery<TEntity>(sql, pars).ToListAsync();
        }
        public async Task<TType> ExcuteSclare<TType>(string sql, params SqlParameter[] pars)
        {
            return await DbContext.Database.SqlQuery<TType>(sql, pars).SingleAsync();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
