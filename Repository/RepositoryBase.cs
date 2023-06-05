using Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
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
        =>
            !trackChanges ? Context.Set<T>().Where(expresion).AsNoTracking() :
            Context.Set<T>().Where(expresion);

        public void Update(T entity)
        {
            Context.Set<T>().Update(entity);
        }
    }
}
