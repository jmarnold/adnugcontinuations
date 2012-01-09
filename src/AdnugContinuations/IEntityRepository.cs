using System;
using System.Collections.Generic;

namespace AdnugContinuations
{
    public interface IEntityRepository
    {
        IEnumerable<T> All<T>() where T : class, IEntity;
        T Find<T>(Guid id) where T : class, IEntity;
        void Update<T>(T model) where T : class, IEntity;
    }
}