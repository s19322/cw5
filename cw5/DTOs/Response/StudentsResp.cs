using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw5.DTOs.Response
{
    public class StudentsResp
    {
        public string IndexNumber { get; set; }//nazwa studiow

        public string FirstName { get; set; }

        public string Lastname { get; set; }

        public DateTime BirthDate { get; set; }

        public string Studies { get; set; }

        public int Semester { get; set; }
    }
}
