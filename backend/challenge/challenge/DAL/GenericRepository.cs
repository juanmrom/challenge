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

        protected readonly DbContext _context;

        public GenericRepository(ChallengeContext context)
        {
            _context = context;
        }

        protected DbSet<TEntity> CurrentDbSet
        {
            get
            {
                return _context.Set<TEntity>();
            }
        }

        public TEntity Add(TEntity entity)
        {
            CurrentDbSet.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public int Add(params TEntity[] entities)
        {
            CurrentDbSet.AddRange(entities);
            
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            var entity = CurrentDbSet.FirstOrDefault(e => e.Id == id);
            if (entity == null)
            {
                var message = $"The entity with id {id}, not exist.\nEntity: {entity.ToString()}";
                 
                RepositoryExceptionHelper.ThrowRepositoryException(message, "REP-001");
            }
            CurrentDbSet.Remove(entity);

            return _context.SaveChanges();
        }

        public TEntity Get(Func<TEntity, bool> expression)
        {
            return CurrentDbSet.Where(expression).FirstOrDefault();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return CurrentDbSet.ToList();
        }
       
        public PagedResult<TEntity> Get(int currentPage, int pageSize, IQueryable<TEntity> query)
        {
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

        public IQueryable<TEntity> GetQuerable()
        {
            return CurrentDbSet.AsQueryable();
        }

        public TEntity Update(TEntity entity)
        {
            var updateEntity = CurrentDbSet.FirstOrDefault(e => e.Id == entity.Id);
            if (entity == null)
            {
                var message = $"The entity with id {entity.Id}, not exist.\nEntity: {entity.ToString()}";

                RepositoryExceptionHelper.ThrowRepositoryException(message, "REP-001");
            }
            updateEntity = entity;
            _context.SaveChanges();

            return updateEntity;
        }

        public PagedResult<TEntity> Get(int currentPage, int pageSize, Func<TEntity, bool> expression)
        {
            var result = new PagedResult<TEntity>();
            result.CurrentPage = currentPage;
            result.PageSize = pageSize;
            result.RowCount = CurrentDbSet.Count();

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (currentPage - 1) * pageSize;

            result.Results = CurrentDbSet.Where(expression).Skip(skip).Take(pageSize).ToList();

            return result;
        }

        public TEntity Get(int id)
        {
            return CurrentDbSet.FirstOrDefault(e => e.Id == id);
        }
    }
}
