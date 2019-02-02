using System.Collections.Generic;

namespace MVC5_Ref.DAL.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(object id);

        IEnumerable<TEntity> GetAll();

        void Insert(TEntity entity);

        void InsertAll(IEnumerable<TEntity> entityList);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void DeleteByID(object id);

        void DeleteAll(IEnumerable<TEntity> entityList);
    }
}