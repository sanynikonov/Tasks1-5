using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataLevel
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DataContext db;
        private DbSet<T> entities;

        public Repository(DataContext db)
        {
            this.db = db;
            entities = db.Set<T>();
        }

        public void Add(T item)
        {
            entities.Add(item);
        }

        public void Delete(int id)
        {
            T entity = entities.Find(id);
            if (entity != null)
                entities.Remove(entity);
        }

        public T Get(int id)
        {
            return entities.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return entities;
        }

        public void Update(T item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
