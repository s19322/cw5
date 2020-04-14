using cw5.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw5.Services
{
    public interface IDbService
    {
     //   IEnumerable<Student> GetStudents();
        Student CheckIndex(string Index);
    }
}
