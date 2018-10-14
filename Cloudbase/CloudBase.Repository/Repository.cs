using System;
using System.Collections.Generic;
using System.Linq;
using Cloudbase.Entities;
using Cloudbase.Entities.SecurityModels;
using CloudBase.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CloudBase.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly IdentityDbContext<User> _context;

        protected Repository(IdentityDbContext<User> context)
        {
            _context = context;
        }

        public IQueryable<TEntity> Filter()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public TEntity Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

    }
}