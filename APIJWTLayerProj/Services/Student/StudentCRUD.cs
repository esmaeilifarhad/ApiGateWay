using DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIJWTLayerProj.Services.Student
{
    public class StudentCRUD : ICRUD<DomainClass.Model.Student>
    {
        UnitOfWork<DomainClass.Model.Student> db = new UnitOfWork<DomainClass.Model.Student>();
        public void Delete(int id)
        {
            db.Repository.Delete(id);
            db.Repository.Save();
        }

        public void Delete(DomainClass.Model.Student entity)
        {
            db.Repository.Delete(entity);
            db.Repository.Save();
        }

        public IEnumerable<DomainClass.Model.Student> Get()
        {
          var res=  db.Repository.Get();
            return res;
        }

        public void Insert(DomainClass.Model.Student entity)
        {
            db.Repository.Insert(entity);
            db.Repository.Save();
        }
        public void Update(DomainClass.Model.Student entity)
        {
            db.Repository.Update(entity);
            db.Repository.Save();
        }
    }
}
