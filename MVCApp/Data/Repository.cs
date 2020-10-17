using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal AppDBContext _db;
        internal DbSet<T> dbSet;

        public Repository(AppDBContext appDBContext)
        {
            _db = appDBContext;
            dbSet = _db.Set<T>();
        }
        public void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _db.Set<T>().Update(entity);
        }
        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _db.Set<T>().AsEnumerable();
        }
    }
}
