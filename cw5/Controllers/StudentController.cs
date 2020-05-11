using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw5.ModelsFrameWorkCore;
using cw5.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cw5.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentDbService _context;
        public StudentController(IStudentDbService context)
        {
            _context = context;

        }
        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok();
        }
    }
}