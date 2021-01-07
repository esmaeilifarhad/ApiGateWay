
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Context
{
    public class UnitOfWork<TEntity> where TEntity : class/*, IDisposable*/
    {
        MyContext db = new MyContext();
        private MyGenericRepository<TEntity> repository;
        public MyGenericRepository<TEntity> Repository
        { 
            get 
            {
                if (repository == null)
                {
                    repository = new MyGenericRepository<TEntity>(db);
                }
                return repository;
            }
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
