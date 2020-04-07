


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
        public EnrollStudResponse Enrollstudent(EnrollStudRequest request)
        {

            throw new NotImplementedException();
      
        }

        public EnrollStudResponsePr PromoteStudents(EnrollStudRequestPr requestPr)
        {

            throw new NotImplementedException();
        }
    }
}
