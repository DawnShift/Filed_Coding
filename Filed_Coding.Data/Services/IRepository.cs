using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Filed_Coding.Data.Services
{
    public interface IRepository<T, TKey>
 where T : class, IEntity<TKey>
    {
        IEnumerable<T> GetAll();
        IQueryable<T> FilteredGetAll(bool includeDeleted = false);
        SqlParameter[] ExecuteSPCommands(string sqlQuery, SqlParameter[] parameters);
        T Get(TKey id);
        void Insert(T entity);
        void InsertMany(List<T> entity);
        void Update(T entity);
        void UpdateMany(List<T> entity);
        void Delete(T entity);
        void DeleteMany(List<T> entity);
        void Remove(T entity);
        void SaveChanges();
        IQueryable<T> ReadFromStoredProcedure(string sqlQuery, SqlParameter[] parameters);
        void Attach(T entity);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate, bool includeDeleteed = false);
    }
    public interface IEntity<TKey>
    {

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
