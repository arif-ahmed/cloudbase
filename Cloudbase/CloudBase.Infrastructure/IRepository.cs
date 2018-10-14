using System;
using System.Collections.Generic;
using System.Linq;
using Cloudbase.Entities;

namespace CloudBase.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        IQueryable<TEntity> Filter();
        TEntity Find(Guid id);
        bool Insert(TEntity entity);
        bool Update(TEntity entity);
        bool Save();
    }
}