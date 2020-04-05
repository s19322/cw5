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


            var stud = new EnrollStudResponse();
            stud.Semester = student.Semester;


            using (SqlConnection con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19322;Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                var tran = con.BeginTransaction();
                com.CommandText = "select IdStudy from Studies where Name=@Name";
                com.Parameters.AddWithValue("Name", request.Studies);

                com.Transaction = tran;//polaczenie i transakcja
                var dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    tran.Rollback();
                    return BadRequest("takie studia nie istnieja");
                }
                int IdStudies = (int)dr["IdStudy"];

                com.CommandText = "select TOP 1 from Enrollment where IdStudies="+IdStudies+" and semester=1 order by desc";
                //com.Parameters.AddWithValue("IdStudies", IdStudies);
               // com.Transaction = tran;
                var dr2 = com.ExecuteReader();
                if (!dr2.Read())
                {
                    com.CommandText = "Insert into Entrollment (IdEnrollment,Semester,IdStudy,StartDate)" +
                        "values(@@IDENTITY+1,1,@IdStudies,'04.04.2020')";


                }

            }
            return Ok();


        }



    }
}