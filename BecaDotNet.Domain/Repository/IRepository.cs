using System.Collections.Generic;
using System.Data;

namespace BecaDotNet.Domain.Repository
{
    public interface IRepository<T>
    {
        T Create(T newEntity);
        T GetSingle(int userId);
        IEnumerable<T> GetMany(T filter);
        bool Remove(int entityId);
        T Update(T updatedEntity);
        T GetEntityFromReader(IDataReader reader);
        IEnumerable<T> GetManyEntityFromReader(IDataReader reader);

        //C   reate
        //R   ecover
        //U   pdate
        //D   elete
    }
}
