using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cw5.DTOs
{
    public class EnrollStudRequest
    {
        [Required]
        [RegularExpression("^s[0-9]+$")]
        public string IndexNumber { get; set; }//nazwa studiow

        [Required]
        [MinLength(1)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(1)]
        public string Lastname { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Studies { get; set; }

        
    }
}
