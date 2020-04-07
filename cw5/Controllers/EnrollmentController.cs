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
        public IActionResult EnrollStudent(EnrollStudRequest request)
        {

             var resp = _service.Enrollstudent(request);

            IActionResult result = Created("enrolled",resp);
            return result;
                }
         
    [HttpPost("promotions")]
    public IActionResult EnrollStudents(EnrollStudRequestPr requestPr)
        {
            var resp = _service.PromoteStudents(requestPr);
            IActionResult response = Created("promoted", resp);
          
            return response;
        }


 
    }
       




   
}