using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw5.DTOs.Response
{
    public class EnrollStudResponsePr
    {
        public int Semester { get; set; }

        public string FirstName { get; set; }
    
        public string LastName { get; set; }
        public int IdEnrollment { get; set; }
    }
}
