using System.ComponentModel.DataAnnotations;

namespace cw5.Controllers
{
    public class EnrollStudRequestPr
    {
        [Required]
        public string Studies { get; set; }


        [Required]
        public int Semester { get; set; }

    }
}