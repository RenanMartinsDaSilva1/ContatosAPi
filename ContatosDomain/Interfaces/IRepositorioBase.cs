

namespace ContatosDomain.Interfaces
{
    public interface IRepositorioBase<TEntity> where TEntity : class
    {
        void Insert(TEntity obj);
        void Update(TEntity obj);
        void Delete(TEntity obj);
        TEntity GetById(object id);
        IEnumerable<TEntity> GetAll();
    }
}
