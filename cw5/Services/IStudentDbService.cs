


using cw5.Controllers;
using cw5.DTOs;
using cw5.DTOs.Request;
using cw5.DTOs.Response;
using cw5.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace cw5.Services
{
    public interface IStudentDbService
    {
        public EnrollStudResponse EnrollStudent(EnrollStudRequest request);
        public EnrollStudResponsePr PromoteStudent(EnrollStudRequestPr request);

        public void DeleteStudent(DeleteStudReq request);
        public void ModifyStudent(Student request);
        public List<Student> getAllStudentsData();

    }
}
