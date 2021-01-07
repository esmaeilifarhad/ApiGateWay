using DomainClass.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace APIJWTLayerProj.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class StudentController : ControllerBase
    {
        Services.Student.StudentCRUD _student=new Services.Student.StudentCRUD();
        //UnitOfWork<DomainClass.Model.Student> db = new UnitOfWork<Student>();

        [Authorize(Roles = "Admin")]
        [HttpGet("Get")]
        public IEnumerable<Student> Get()
        {
            return _student.Get();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Update")]
        public void Update(DomainClass.Model.Student Student)
        {
            _student.Update(Student);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Insert")]
        public void Insert(DomainClass.Model.Student Student)
        {
           _student.Insert(Student);
            //db.Repository.Save();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Delete")]
        public void Delete(int id)
        {
            _student.Delete(id);
        }

    }
}
