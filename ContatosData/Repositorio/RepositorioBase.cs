using ContatosData.Contexto;
using ContatosDomain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContatosData.Repositorio
{
    public class RepositorioBase<TEntity> : IRepositorioBase<TEntity> where TEntity : class
    {
        protected readonly MainContext Db;

        protected DbSet<TEntity> DbSet;

        public RepositorioBase(MainContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();                                        
        }

        public void Delete(TEntity obj)
        {
            DbSet.Remove(obj);
            Db.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll()
        {
           return DbSet.AsNoTracking().ToList();
        }

        public TEntity GetById(object id)
        {
            return DbSet.Find(id);
        }

        public void Insert(TEntity obj)
        {
            DbSet.Add(obj);
            Db.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            Db.Entry(obj).State = EntityState.Modified;
            Db.SaveChanges();
        }
    }
}
