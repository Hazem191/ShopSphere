using Microsoft.EntityFrameworkCore;
using ShopSphere.Data;
using System.Linq.Expressions;

namespace ShopSphere.Repository
{
    public class EntityRepo<T, TKey> : IEntityRepo<T, TKey> where T : class
    {
        #region Fields & Constructor
        private readonly ShopSphereDB context;
        private readonly DbSet<T> dbSet;

        public EntityRepo(ShopSphereDB context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }
        #endregion

        #region CRUD Operations
        public virtual List<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T GetById(TKey id)
        {
            return dbSet.Find(id);
        }

        public List<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate).ToList();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }

        public void Delete(TKey id)
        {
            var entity = dbSet.Find(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
            }
            else
            {
                throw new ArgumentException($"Entity with id {id} not found.");
            }
        }
        #endregion

        #region Save Changes
        public void SaveChanges()
        {
            context.SaveChanges();
        }
        #endregion
    }
}
