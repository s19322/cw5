


using cw5.DTOs;
using cw5.DTOs.Response;
using cw5.Models;
using System;
using System.Data.SqlClient;

namespace cw5.Services
{
    public interface IStudentDbService
    {
        public int Enrollstudent(EnrollStudRequest request)
        {

            throw new NotImplementedException();
      
        }

        public int PromoteStudents(int semester, string studies)
        {

            throw new NotImplementedException();
        }
    }
}
