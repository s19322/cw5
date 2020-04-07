using cw5.DTOs;
using cw5.DTOs.Response;
using cw5.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;

namespace cw5.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudRequest request)
        {



            if (!ModelState.IsValid)//zwraca stan modelu czyli enrollstudrequest czyli jesli którys z
                                    //warunkow zostaje naruszony 
            {
                var d = ModelState;
                return BadRequest("cos poszlo nie tak");

            }
            var student = new Student();
            student.IndexNumber = request.IndexNumber;
            student.FirstName = request.FirstName;
            student.Lastname = request.Lastname;
            student.Studies = request.Studies;
            student.BirthDate = request.BirthDate;


            var response = new EnrollStudResponse();
            response.Semester = student.Semester;


            using (SqlConnection con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19322;Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                var tran = con.BeginTransaction();
                com.Transaction = tran;//polaczenie i transakcja
                com.CommandText = "select IdStudy from Studies where Name=@Name";
                com.Parameters.AddWithValue("Name", request.Studies);

                var dr = com.ExecuteReader();

                if (!dr.Read())
                {
                    tran.Rollback();
                    return BadRequest("This course doesn't exists");
                }

                int IdStudies = (int)dr["IdStudy"];
                dr.Close();

                int IdEnroll = getIdEnroll(com, IdStudies);

                if (IdEnroll == 0)
                {
                    InsertEnroll(com, IdStudies);
                    IdEnroll = getIdEnroll(com, IdStudies);
                   
                }

                com.CommandText = "select IndexNumber from Student where IndexNumber=@IndexNumber";
                com.Parameters.AddWithValue("IndexNumber", request.IndexNumber);
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    tran.Rollback();
                    return BadRequest("student with IndexNumber: {request.IndexNmber} already exists");
                }
                com.CommandText = "insert into Student (IndexNumber,FirstName,LastName,BirthDate,IdEnrollment)" +
                    "values(@Index,@FirstName,@LastName,@BirthDate,@IdEnrollment) ";
                com.Parameters.AddWithValue("Index", request.IndexNumber);
                com.Parameters.AddWithValue("FirstName", request.FirstName);
                com.Parameters.AddWithValue("LastName", request.Lastname);
                com.Parameters.AddWithValue("BirthDate", request.BirthDate);
                com.Parameters.AddWithValue("IdEnrollment", IdEnroll);
                
            }


            return Ok(response.Semester);


        }
        public int getIdEnroll(SqlCommand com, int IdStudies)
        {
            
            com.CommandText = "select IdEnrollment from Enrollment "
                                    + "where semester = 1 and IdStudy = @IdStudies and StartDate = "
                                    + "(select max(StartDate) from Enrollment "
                                    + " where semester = 1 and IdStudy = @IdStudies)";
            com.Parameters.AddWithValue("IdStudies", IdStudies);
            var dr = com.ExecuteReader();
            int  IdEntroll = dr.Read() ? (int)dr["IdEnrollment"] : 0;
            dr.Close();
            return IdEntroll;
        }
        public void InsertEnroll(SqlCommand com, int IdStudies)
        {
            com.CommandText = "Insert into Entrollment (IdEnrollment,Semester,IdStudy,StartDate)" +
                           "values(@@IDENTITY+1,1,@IdStudies,GetDate())";
            com.Parameters.AddWithValue("IdStudies", IdStudies);
        }
       

    }
}