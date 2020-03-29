using challenge.DAL.Entity;
using challenge.Utils;
using challenge.Utils.Exception;
using challenge.Utils.Helpers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace challenge.DAL
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IBaseEntity        
    {

        protected ChallengeContext _context;

        public GenericRepository(ChallengeContext context)
        {
            _context = context;
        }

        //protected DbSet<TEntity> CurrentDbSet
        //{
        //    get
        //    {
        //        return _context.Set<TEntity>();
        //    }
        //}

        public TEntity Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public virtual int Add(params TEntity[] entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            
            return _context.SaveChanges();
        }

        public virtual int Delete(int id)
        {
            var entity = _context.Set<TEntity>().FirstOrDefault(e => e.Id == id);
            if (entity == null)
            {
                var message = $"The entity with id {id}, not exist.\nEntity: {entity.ToString()}";
                 
                RepositoryExceptionHelper.ThrowRepositoryException(message, "REP-001");
            }
            _context.Set<TEntity>().Remove(entity);

            return _context.SaveChanges();
        }

        public virtual TEntity Get(Func<TEntity, bool> expression)
        {
            return _context.Set<TEntity>().Where(expression).FirstOrDefault();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }
       
        public virtual PagedResult<TEntity> Get(int currentPage, int pageSize, IQueryable<TEntity> query)
        {
            pageSize = pageSize == 0 ? 10 : pageSize;
            currentPage = currentPage == 0 ? 1 : currentPage;
            var result = new PagedResult<TEntity>();
            result.CurrentPage = currentPage;
            result.PageSize = pageSize;
            result.RowCount = query.Count();


            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (currentPage - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }

        public virtual IQueryable<TEntity> GetQuerable()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public virtual TEntity Update(TEntity entity)
        {
            var updateEntity = _context.Set<TEntity>().FirstOrDefault(e => e.Id == entity.Id);
            if (entity == null)
            {
                var message = $"The entity with id {entity.Id}, not exist.\nEntity: {entity.ToString()}";

                RepositoryExceptionHelper.ThrowRepositoryException(message, "REP-001");
            }
            updateEntity = entity;
            _context.SaveChanges();

            return updateEntity;
        }

        public virtual PagedResult<TEntity> Get(int currentPage, int pageSize, Func<TEntity, bool> expression)
        {
            var query = _context.Set<TEntity>();
            pageSize = pageSize == 0 ? 10 : pageSize;
            currentPage = currentPage == 0 ? 1 : currentPage;
            var result = new PagedResult<TEntity>();
            result.CurrentPage = currentPage;
            result.PageSize = pageSize;                       

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (currentPage - 1) * pageSize;

            result.Results = query.Where(expression).Skip(skip).Take(pageSize).ToList();

            result.RowCount = _context.Set<TEntity>().Count(expression);

            return result;
        }

        public virtual TEntity Get(int id)
        {
            return _context.Set<TEntity>().FirstOrDefault(e => e.Id == id);
        }

        public void Delete(int[] ids)
        {
            var entities = _context.Set<TEntity>().Where(d => ids.Contains(d.Id));
            _context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
