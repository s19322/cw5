
using cw5.Controllers;
using cw5.DTOs;
using cw5.DTOs.Response;
using cw5.Models;
using System;
using System.Data.SqlClient;

namespace cw5.Services
{
    public class SqlServerStudentDbService : IStudentDbService
    {

        public EnrollStudResponse Enrollstudent(EnrollStudRequest request)
        {
            /*var student = new Student();
            student.IndexNumber = request.IndexNumber;
            student.FirstName = request.FirstName;
            student.Lastname = request.Lastname;
            student.Studies = request.Studies;
            student.BirthDate = request.BirthDate;*/



            // response.Semester = student.Semester;

            var response = new EnrollStudResponse();
            using (SqlConnection con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19322;Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                var tran = con.BeginTransaction();
                com.Transaction = tran;//polaczenie i transakcja
                com.CommandText = "select IdStudy from Studies where Name=@Name";
                com.Parameters.AddWithValue("Name", request.Studies);
                 try
                {
                var dr = com.ExecuteReader();

                if (!dr.Read())
                {
                    tran.Rollback();
                    //  return BadRequest("This course doesn't exists");
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
                    //  return BadRequest("student with IndexNumber: {request.IndexNmber} already exists");
                }
                dr.Close();
                com.CommandText = "insert into Student (IndexNumber,FirstName,LastName,BirthDate,IdEnrollment)" +
                    "values(@Index,@FirstName,@LastName,@BirthDate,@IdEnrollment) ";
                com.Parameters.AddWithValue("Index", request.IndexNumber);
                com.Parameters.AddWithValue("FirstName", request.FirstName);
                com.Parameters.AddWithValue("LastName", request.Lastname);
                com.Parameters.AddWithValue("BirthDate", request.BirthDate);
                com.Parameters.AddWithValue("IdEnrollment", IdEnroll);

                // response = new EnrollStudResponse()//??????format problem

                response.IndexNumber = request.IndexNumber;
                response.Semester = 1;
                response.Studies = request.Studies;
               

                
                dr.Close();
                    tran.Commit();
                      }
                      catch (SqlException SqlEx)
                      {
                          tran.Rollback();

                      }
                }

                return response;

            }
        
        public int getIdEnroll(SqlCommand com, int IdStudies)
        {

            com.CommandText = "select IdEnrollment from Enrollment "
                                    + "where semester = 1 and IdStudy = @IdStudies and StartDate = "
                                    + "(select max(StartDate) from Enrollment "
                                    + " where semester = 1 and IdStudy = @IdStudies)";
            com.Parameters.AddWithValue("IdStudies", IdStudies);
            var dr = com.ExecuteReader();
            int IdEntroll = dr.Read() ? (int)dr["IdEnrollment"] : 0;
            dr.Close();
            return IdEntroll;
        }
        public void InsertEnroll(SqlCommand com, int IdStudies)
        {
            com.CommandText = "Insert into Entrollment (IdEnrollment,Semester,IdStudy,StartDate)" +
                           "values(MAX(I1,1,@IdStudies,GetDate())";
            com.Parameters.AddWithValue("IdStudies", IdStudies);
        }




        public EnrollStudResponsePr PromoteStudents(EnrollStudRequestPr requestPr)
        {
            EnrollStudResponsePr resp = new EnrollStudResponsePr();

            //  throw new NotImplementedException();
            using (SqlConnection con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19322;Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                var tran = con.BeginTransaction();
                com.Transaction = tran;
                try
                {
                    com.CommandText = "select * from Enrollment inner join Studies on Enrollment.IdStudy = Studies.IdStudy"
                                 + "where Studies.Name = @Name and Enrollment.Semester = @Semester";
                    com.Parameters.AddWithValue("Name", requestPr.Studies);
                    com.Parameters.AddWithValue("Semester", requestPr.Semester);

                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        tran.Rollback();
                        //  return BadRequest("Database doesn't have this data:{reqestPr.Semester} or {requestPr.Studies}");
                    }
                    dr.Close();
                    com.CommandText = "exec PromotionProcedure @Studies, @Semester";
                    com.Parameters.AddWithValue("Studies", requestPr.Studies);
                    com.Parameters.AddWithValue("Semester", requestPr.Semester);
                    com.ExecuteNonQuery();

                    com.CommandText = "select e.IdEnrollment, e.IdStudy,e.Semester,LastName,FirstName from Enrollment e inner" +
                                       "join Student on Student.IdEnrollment=e.IdEnrollment inner join Studies s on e.IdStudy = s.IdStudy" +
                                 " where s.Name = @SName and Semester = @prev_semEnroll + 1";

                    com.Parameters.AddWithValue("SName", requestPr.Studies);
                    com.Parameters.AddWithValue("pev_semEnroll", requestPr.Semester);
                    dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        tran.Rollback();
                        Console.WriteLine("Brak danych w bazie");
                    }
                    resp = new EnrollStudResponsePr()
                    {
                     Semester = int.Parse(dr["Semester"].ToString()),
                    IdEnrollment = int.Parse(dr["IdEnrollment"].ToString()),
                    LastName = dr["LastName"].ToString(),
                    FirstName = dr["FirstName"].ToString()
                };
                    dr.Close();
                    tran.Commit();
                }catch(SqlException SqlEx)
                {
                    tran.Rollback();
                }
                return resp;
            }
        }
    }
}
