


using cw5.Controllers;
using cw5.DTOs;
using cw5.DTOs.Response;
using cw5.Models;
using System;
using System.Data.SqlClient;

namespace cw5.Services
{
    public interface IStudentDbService
    {
        public EnrollStudResponse EnrollStudent(EnrollStudRequest request);
        public EnrollStudResponsePr PromoteStudent(EnrollStudRequestPr request);
    }
}
