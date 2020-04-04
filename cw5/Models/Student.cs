using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cw5.Models
{
    public class Student
    {
       
        public string IndexNumber { get; set; }//nazwa studiow
        
       
        public string FirstName { get; set; }
        
        
        public string Lastname { get; set; }
        
      
        public DateTime BirthDate { get; set; }
        

        public string Studies { get; set; }
        
        public string Semester { get; set; }
    }
}
