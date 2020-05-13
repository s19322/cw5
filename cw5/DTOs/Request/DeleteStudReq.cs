using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cw5.DTOs.Request
{
    public class DeleteStudReq
    {
        [Required]
        public string IndexNumber { get; set; }

    }
}
