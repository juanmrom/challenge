using challenge.DAL.Entity;
using challenge.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace challenge.DAL
{
    public interface IRepository<TEntity>
        where TEntity : IBaseEntity        
    {
        
        IEnumerable<TEntity> GetAll();
        TEntity Get(Func<TEntity, bool> expression);
        PagedResult<TEntity> Get(int currentPage, int pageSize, Func<TEntity, bool> expression);
        PagedResult<TEntity> Get(int currentPage, int pageSize, IQueryable<TEntity> query);
        IQueryable<TEntity> GetQuerable();
        TEntity Get(int id);
        TEntity Add(TEntity entity);
        int Add(TEntity[] entities);
        TEntity Update(TEntity entity);
        int Delete(int id);        
    }
}
