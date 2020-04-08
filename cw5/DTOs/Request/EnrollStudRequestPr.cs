using System.ComponentModel.DataAnnotations;

namespace cw5.Controllers
{
    public class EnrollStudRequestPr
    {
        [Required]
        [MinLength(1)]
        public string Studies { get; set; }


        [Required] 
        [RegularExpression("[0-9]*")]
        public int Semester {   get; set; }

    }
}