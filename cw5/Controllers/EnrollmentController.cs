using cw5.DTOs;
using cw5.DTOs.Response;
using cw5.Models;
using cw5.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace cw5.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private IStudentDbService _service ;
       
        public EnrollmentController(IStudentDbService service)
        {
            _service = service;
        }
        [HttpPost]
        public EnrollStudResponse EnrollStudent(EnrollStudRequest request)
        {

            EnrollStudResponse studentResponse = _service.EnrollStudent(request);

           
            return studentResponse;
                }
         
    [HttpPost("promotions")]
    public EnrollStudResponsePr PromoteStudents(EnrollStudRequestPr requestPr)
        {
            EnrollStudResponsePr studPr=  _service.PromoteStudent(requestPr);
  
            return studPr;
        }




    }
       




   
}