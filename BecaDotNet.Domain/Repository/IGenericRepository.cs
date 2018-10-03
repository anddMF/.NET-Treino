using BecaDotNet.Domain.Model;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BecaDotNet.Domain.Repository
{
    public interface IGenericRepository<T> where T: IdentifiedEntity
    {
        T GetSingle(int id);
        void Create(T toCreate);

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes);

        void Update(T toUpdate);
        void Delete(int id);
        void Save();
    }
}
