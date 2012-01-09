using System;
using System.Collections.Generic;
using System.Linq;
using FubuCore;

namespace AdnugContinuations
{
    public class EntityRepository : IEntityRepository
    {
        private readonly IList<object> _objects = new List<object>();

        public IEnumerable<T> All<T>() where T : class, IEntity
        {
            return _objects.OfType<T>();
        }

        public void ClearAll<T>()
        {
            _objects.RemoveAll(x => x.GetType().CanBeCastTo<T>());
        }

        public T Find<T>(Guid id)
            where T : class, IEntity
        {
            return All<T>().SingleOrDefault(a => a.Id == id);
        }

        public void Update<T>(T model) where T : class, IEntity
        {
            if (model.Id == default(Guid) || model.Id == Guid.Empty)
            {
                model.Id = Guid.NewGuid();
            }
            else
            {
                var existing = Find<T>(model.Id);
                _objects.Remove(existing);
            }

            

            _objects.Add(model);
        }

    }
}