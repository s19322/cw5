using cw5.DTOs;
using cw5.DTOs.Request;
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

        [HttpGet("students")]
        public IActionResult getStudents()
        {
            IActionResult response = Ok(_service.getAllStudentsData());
            return response;
        }
       [HttpPost("modifystudent")]
        public IActionResult ModifyStudent(Student request)
        {
            IActionResult response = Ok(request.IndexNumber);

                _service.ModifyStudent(request);

            return response;
        }
        [HttpPost("deletestudent")]
        public IActionResult DeleteStudent(DeleteStudReq request)
        {
            IActionResult response = Ok($"Successfully deleted {request.IndexNumber}");
           
                _service.DeleteStudent(request);
            
          
            return response;
        }



    }






}