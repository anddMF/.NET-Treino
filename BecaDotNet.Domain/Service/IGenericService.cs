using System.Collections.Generic;

namespace BecaDotNet.Domain.Service
{
    public interface IGenericService<T>
    {
        T Create(T toCreate);
        T Get(int id);
        IEnumerable<T> FindBy(T filter);
        T Update(T toUpdate);
        bool Delete(int id);
    }
}
