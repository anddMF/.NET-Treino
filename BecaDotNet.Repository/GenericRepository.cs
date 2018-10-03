using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using BecaDotNet.Domain.Model;
using BecaDotNet.Domain.Repository;

namespace BecaDotNet.Repository
{
    public abstract class GenericRepository<C, T> :
        IGenericRepository<T> where T : IdentifiedEntity
        where C : DbContext, new()
    {
        private C _ctx = new C();
        public C Context { get => _ctx; set=>_ctx=value; }

        public void Create(T toCreate) => _ctx.Set<T>().Add(toCreate);

        public void Delete(int id)
        {
            var toDelete = this.GetSingle(id);
            toDelete.IsActive = false;
            this.Update(toDelete);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> currentSet = _ctx.Set<T>();
            foreach (var inc in includes)
                currentSet.Include(inc);

            return currentSet.Where(predicate);
        }

        public T GetSingle(int id) => _ctx.Set<T>().SingleOrDefault(f => f.Id == id);

        public void Save()
        {
            _ctx.SaveChanges();
        }

        public void Update(T toUpdate) => _ctx.Entry(toUpdate).State = System.Data.Entity.EntityState.Modified;
    }
}
