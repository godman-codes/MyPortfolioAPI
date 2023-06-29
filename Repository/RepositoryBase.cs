using Contracts;
using Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utilities.Queries;

namespace Repository
{
    public class RepositoryBase<T, R> : IRepositoryBase<T, R> where T : class, IEntityBase<R>, new()
    {
        protected MyProjectDbContext Context;

        public RepositoryBase(MyProjectDbContext context)
        {
            Context = context;
        }
        public void Create(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll(bool trackChanges)
         => !trackChanges ? Context.Set<T>()
                .AsNoTracking() :
                Context.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expresion, bool trackChanges)
        {
            Expression<Func<T, bool>> whereClause1 = (p => !p.IsDeleted);
            Expression<Func<T, bool>> where = QueryCombinator.MergeWithAnd<T>(expresion, whereClause1);
         return  !trackChanges ? Context.Set<T>().Where(where).AsNoTracking() :
            Context.Set<T>().Where(where);
        }

        public void Update(T entity)
        {
            Context.Set<T>().Update(entity);
        }
    }
}
