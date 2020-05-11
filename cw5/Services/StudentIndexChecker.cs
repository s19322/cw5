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

    }
}
