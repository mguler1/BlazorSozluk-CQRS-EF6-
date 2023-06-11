using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Application.Interfaces.Repositories;
using BlazorSozluk.InfastructurePersistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.InfastructurePersistance.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly BlazorSozlukContext _dbContext;
        protected DbSet<TEntity> entity => _dbContext.Set<TEntity>();
        public GenericRepository(BlazorSozlukContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public int Add(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(TEntity entity)
        {
            await this.AddAsync(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public Task<int> AddAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public int AddOrUpdate(TEntity entity)
        {
            if (!this.entity.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id))) _dbContext.Update(entity);
            return _dbContext.SaveChanges();
        }

        public Task<int> AddOrUpdateAsync(TEntity entity)
        {
            if(!this.entity.Local.Any(i=>EqualityComparer<Guid>.Default.Equals(i.Id,entity.Id))) _dbContext.Update(entity);
            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<TEntity> AsQueryable()
        {
            throw new NotImplementedException();
        }

        public Task BulkAdd(IEnumerable<TEntity> entities)
        {
           if (entities != null && !entities.Any())
                return Task.CompletedTask;
            foreach (var entityItem in entities)
            {
                entity.Add(entityItem);
            }
            return _dbContext.SaveChangesAsync();
        }
        public Task<int> SaveChangeAsync()
        {
            return _dbContext.SaveChangesAsync();   
        }
        public int SaveChanges() 
        {
            return _dbContext.SaveChanges();
        }

        public Task BulkDelete(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task BulkDelete(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task BulkDeleteById(IEnumerable<Guid> ids)
        {
            if (ids != null && ids.Any())
                return Task.CompletedTask;
            _dbContext.RemoveRange(entity.Where(x => ids.Contains(x.Id)));
            return _dbContext.SaveChangesAsync();
            
        }

        public Task BulkUpdate(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public int Delete(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                this.entity.Attach(entity);
            }
            this.entity.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(int Guid)
        {

            var entity = this.entity.Find(Guid);
            return Delete(entity);
        }

        public Task<int> DeleteAsync(TEntity entity)
        {
            if (_dbContext.Entry(entity).State==EntityState.Detached)
            {
                this.entity.Attach(entity);
            }
            this.entity.Remove(entity);
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(Guid id)
        {
          var entity = this.entity.Find(id);
            return DeleteAsync(entity);
        }

        public bool DeleteRange(Expression<Func<TEntity, bool>> predicate)
        {
            _dbContext.RemoveRange(predicate);
            return _dbContext.SaveChanges() > 0;
        }

        public  async Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
        {
            _dbContext.RemoveRange(predicate);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public Task<List<TEntity>> GetAll(bool noTracking = true)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            TEntity found=await entity.FindAsync(id);
            if (found == null)
                return null;
            if (noTracking)
                _dbContext.Entry(found).State = EntityState.Detached;
            foreach(Expression<Func<TEntity,object>>include in includes)
            {
                _dbContext.Entry(found).Reference(include).Load();
            }
            return found;
        }

        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, Func<IQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = entity;
            if (predicate!=null)
            {
                query = query.Where(predicate);
            }
            foreach (Expression<Func<TEntity,object>>include in includes)
            {
                query=query.Include(include);
            }
         
            if (noTracking)
           
                query = query.AsNoTracking();
                return  await query.ToListAsync();
            
        }

        public int Update(TEntity entity)
        {
            this.entity.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return _dbContext.SaveChanges();
        }

        public Task<int> UpdateAsync(TEntity entity)
        {
            this.entity.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return _dbContext.SaveChangesAsync();
        }
    }
}
