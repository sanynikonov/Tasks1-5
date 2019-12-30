using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel
{
    public class ProviderRepository : IRepository<Provider>
    {
        private DataContext db;

        public ProviderRepository(DataContext db)
        {
            this.db = db;
        }

        public IEnumerable<Provider> GetAll()
        {
            return db.Providers;
        }

        public Provider Get(int id)
        {
            return db.Providers.Find(id);
        }

        public void Add(Provider item)
        {
            db.Providers.Add(item);
        }

        public void Update(Provider item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Provider provider = db.Providers.Find(id);
            if (provider != null)
                db.Providers.Remove(provider);
        }
    }
}
