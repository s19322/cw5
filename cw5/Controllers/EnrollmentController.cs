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
        private SqlServerStudentDbService _service ;
       
        public EnrollmentController(SqlServerStudentDbService service)
        {
            _service = service;
        }
        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudRequest request)
        {

             int resp = _service.Enrollstudent(request);

            IActionResult response = Created("student enrolled succesfully", resp);
            return Ok(response);
                }
         
    [HttpPost("promotions")]
    public ActionResult EnrollStudents(EnrollStudRequestPr requestPr)
        {
            int resp = _service.PromoteStudents(requestPr);

            return Ok(resp);
        }


 
    }
       




   
}