using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    // The generic constraint where T : class ensures that T is a reference type (class)
    public class Repository<T> : IRepository<T> where T : class

    {
        private readonly ApplicationDbContext _db;
        //The DbSet<T> represents the collection of entities of type T
        //in the context of Entity Framework Core.
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            //This associates the dbSet with the DbSet of entities
            //of type T in the database context.
            dbSet = _db.Set<T>();
            //_db.Categories == dbSet
            
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        // The Get method retrieves a single entity based on a specified filter
        // condition expressed as a lambda expression
        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }
        //The GetAll method retrieves all entities of type T from the database
        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
