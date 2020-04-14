using cw5.Exceptions;
using cw5.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace cw5.Services
{
    public class StudentIndexChecker : IDbService
    {
        //IEnumerable<Student> students = new List<Student>();
        //polaczenie z baza danych aby spr czy jest student o takim indexie
        public Student CheckIndex(string Index)
        {

            string str = "Data Source=db-mssql;Initial Catalog=s19322;Integrated Security=True";
            using (var con = new SqlConnection(str))
            using (var com = new SqlCommand())
            {
                
                com.Connection = con;
                con.Open();
                com.CommandText = "select LastName,FirstName from Student where IndexNumber=@index";
               com.Parameters.AddWithValue("index", Index);
                var dr = com.ExecuteReader();
                var student = new Student();

                if (dr.Read())
                {
                    student = new Student
                    {
                        FirstName = dr["FirstName"].ToString(),
                        Lastname = dr["LastName"].ToString(),
                        IndexNumber = Index
                    };

                    if (dr["FirstName"] == DBNull.Value)
                    {
                        student = null;
                        return student;
                    }
                }

                dr.Close();
                return student;


            }

        }

/*        public IEnumerable<Student> GetStudents()
        { 

            return students;
        }*/
     /*   public StudentIndexChecker()
        {
            students = new List<Student>
            {
                new Student
                {
                    
                    IndexNumber = "s19344",
                    FirstName = "Pawel",
                    Lastname = "Kowalski",
                    BirthDate = new DateTime(1998, 2, 1),
                    
                },
                new Student
                {
                    
                    IndexNumber = "s9870",
                    FirstName = "Krolina",
                    Lastname = "Nowak",
                    BirthDate = new DateTime(1990, 12, 10),
                   
                },
                new Student
                {
                  
                    IndexNumber = "s1876",
                    FirstName = "Andrzej",
                    Lastname = "Pawlowicz",
                    BirthDate = new DateTime(1998, 10, 12),
                    
                }
            };
        }
*/
       
    }
}
