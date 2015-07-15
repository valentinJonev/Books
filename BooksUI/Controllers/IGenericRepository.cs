﻿namespace BooksUI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data.Entity;
    using System.Linq.Expressions;

    public interface IGenericRepository<TEntity>
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "");

        TEntity GetByID(object id);

        void Insert(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);
    }
}
