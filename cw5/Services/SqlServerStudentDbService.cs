

using cw5.Controllers;
using cw5.DTOs;
using cw5.DTOs.Response;
using cw5.ModelsFrameWorkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;

namespace cw5.Services
{
    public class SqlServerStudentDbService : IStudentDbService
    {
        public EnrollStudResponse EnrollStudent(EnrollStudRequest request)
        {
            var db = new s19322Context();
            var response = new EnrollStudResponse();
            var idStudy = db.Studies.Where(d => d.Name == request.Studies).ToList();
            foreach (var s in idStudy)
            {
                if (s == null)
                    return null;
            }
            var maxStartDate = db.Enrollment.Select(d => d.StartDate).Max();
        //metoda getIdEnrollment()
            var idEnroll = db.Enrollment.Where(d => d.Semester == 1)
                         .Where(d => d.StartDate == maxStartDate)
                         .Select(d => d.IdStudy.Equals(idStudy.Select(e => e.IdStudy))).ToList();
            foreach (var s in idEnroll)
            {
                if (s == null)

                    /* "Insert into Entrollment (IdEnrollment,Semester,IdStudy,StartDate)" +
       "values((select count(*)+1 from Enrollment),1,@IdStudies,GetDate())";
                                   */
                    maxStartDate = db.Enrollment.Select(d => d.StartDate).Max();
                idEnroll = db.Enrollment.Where(d => d.Semester == 1)
                             .Where(d => d.StartDate == maxStartDate)
                             .Select(d => d.IdStudy.Equals(idStudy.Select(e => e.IdStudy))).ToList();

            }












            return response;
            //throw new System.NotImplementedException();

            /*var student = new Student();
            student.IndexNumber = request.IndexNumber;
            student.FirstName = request.FirstName;
            student.Lastname = request.Lastname;
            student.Studies = request.Studies;
            student.BirthDate = request.BirthDate;*/
            // response.Semester = student.Semester;
            /*
                        using (SqlConnection con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19322;Integrated Security=True"))
                        using (SqlCommand com = new SqlCommand())
                        {
                            con.Open();
                            com.Connection = con;
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
                                com.Parameters.Clear();
                                if (dr.Read())
                                {
                                    dr.Close();
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

                                response = new EnrollStudResponse()
                                {

                                    IndexNumber = request.IndexNumber,
                                    Semester = 1,
                                    Studies = request.Studies

                                };

                                dr.Close();
                                tran.Commit();
                            }
                            catch (SqlException SqlEx)
                            {
                                tran.Rollback();

                            }
                        }
        }
          public int getIdEnroll(SqlCommand com, int IdStudies){
        com.CommandText = "select IdEnrollment from Enrollment "
                                    + "where semester = 1 and IdStudy = @IdStudies and StartDate = "
                                    + "(select max(StartDate) from Enrollment "
                                    + " where semester = 1 and IdStudy = @IdStudies)";
            com.Parameters.AddWithValue("IdStudies", IdStudies);
            var dr = com.ExecuteReader();
        com.Parameters.Clear();
            int IdEntroll = dr.Read() ? int.Parse(dr["IdEnrollment"].ToString()) : 0;
        dr.Close();
            return IdEntroll;
        }*/
            /*         
                    public void InsertEnroll(SqlCommand com, int IdStudies)
                    {
                        com.CommandText = "Insert into Entrollment (IdEnrollment,Semester,IdStudy,StartDate)" +
                                       "values((select count(*)+1 from Enrollment),1,@IdStudies,GetDate())";
                        com.Parameters.AddWithValue("IdStudies", IdStudies);
                        var dr = com.ExecuteReader();
                        com.Parameters.Clear();
                        dr.Close();
                    }
            */
        }

    public EnrollStudResponsePr PromoteStudent(EnrollStudRequestPr requestPr)
        {
            /* //  resp.enroll = new List<object>();
             throw new NotImplementedException();
            using (SqlConnection con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19322;Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {
                con.Open();
                com.Connection = con;
                
                var tran = con.BeginTransaction();
                com.Transaction = tran;
                try
                {
                    com.CommandText = "select * from Enrollment inner join Studies on Enrollment.IdStudy = Studies.IdStudy"
                                 + "where Studies.Name = @Name and Enrollment.Semester = @Semester";
                    com.Parameters.AddWithValue("Name", requestPr.Studies);
                    com.Parameters.AddWithValue("Semester", requestPr.Semester);

                    var dr = com.ExecuteReader();
                    com.Parameters.Clear();
                    if (!dr.Read())
                    {
                        tran.Rollback();
                        //  return BadRequest("Database doesn't have this data:{reqestPr.Semester} or {requestPr.Studies}");
                    }
                    dr.Close();
                    com.CommandText = "exec PromotionProcedure @Studies,@Semester";
                    com.Parameters.AddWithValue("Studies", requestPr.Studies);
                    com.Parameters.AddWithValue("Semester", requestPr.Semester);

                    //com.ExecuteNonQuery();
                    com.Parameters.Clear();
                    com.CommandText = "select * from Enrollment e inner" +
                                       " Studies s on e.IdStudy = s.IdStudy" +
                                 " where s.Name = @SName and Semester = @Semester + 1";

                    com.Parameters.AddWithValue("SName", requestPr.Studies);
                    com.Parameters.AddWithValue("Semester", requestPr.Semester);

                    dr = com.ExecuteReader();
                    com.Parameters.Clear();
                    if (!dr.Read())
                    {
                        tran.Rollback();
                        //Console.WriteLine("Brak danych w bazie");
                    }


                    resp.Semester = 4;// int.Parse(dr["Semester"].ToString()),
                    resp.IdEnrollment = 1;//int.Parse(dr["IdEnrollment"].ToString()),


               

                }
                catch (SqlException SqlEx)
                {
                    tran.Rollback();
                }}*/
            EnrollStudResponsePr resp = new EnrollStudResponsePr();
            return resp;
        }
    }
}
