using Filed_Coding.Data.DBContexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text; 

namespace Filed_Coding.Data.Services
{
    public class Repository<T, TKey> : IRepository<T, TKey>
     where T : class, IEntity<TKey>
     where TKey : IEquatable<TKey>
    {
        private readonly PaymentDbContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(PaymentDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            var data = entities.Where(x => x.IsDeleted == false).ToList();
            return data;
        }

        public T Get(TKey id)
        {
            var result = entities.Find(id);
            return result.IsDeleted ? null : result;
        }

        public void Attach(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            this.entities.Attach(entity);
        }

        public void InsertMany(List<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.AddRange(entity);
            context.SaveChanges();
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void UpdateMany(List<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.UpdateRange(entity);
            context.SaveChanges();
        }

        public void DeleteMany(List<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity = entity.Select(x => { x.IsDeleted = true; return x; }).ToList();
            entities.UpdateRange(entity);
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity.IsDeleted = true;
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
        public void RemoveMany(List<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.RemoveRange(entity);
            context.SaveChanges();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public virtual IQueryable<T> FilteredGetAll(bool includeDeleted = false)
        {
            return includeDeleted ? entities.AsQueryable() : entities.Where(x => x.IsDeleted == false).AsQueryable();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate, bool includeDeleteed = false)
        {
            return includeDeleteed ? entities.Where(predicate) : entities.Where(x => x.IsDeleted == false).Where(predicate);
        }

        public virtual SqlParameter[] ExecuteSPCommands(string sqlQuery, SqlParameter[] parameters)
        {
            try
            {

                _ = this.context.Database.ExecuteSqlRaw(sqlQuery, parameters);
                return parameters.Where(x => x.Direction == System.Data.ParameterDirection.Output).ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IQueryable<T> ReadFromStoredProcedure(string sqlQuery, SqlParameter[] parameters)
        {
            try
            {
                return entities.FromSqlRaw(sqlQuery, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
