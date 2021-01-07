using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIJWTLayerProj.Services
{
    interface ICRUD<T> where T:class
    {
        void Delete(int id);
        void Delete(T entity);
        IEnumerable<T> Get();
        void Insert(T entity);
        void Update(T entity);
       
    }
}
