using System;
using System.Collections.Generic;

namespace cw5.ModelsFrameWorkCore
{
    public partial class Doctor
    {
        public Doctor()
        {
            Prescription = new HashSet<Prescription>();
        }

        public int IdDoctor { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        //virtual ->lazy loading przeznaczone do nadpisania w jakies podklasie ,
        //bez slowa tego monz aprzeslonic przy pomocy slowa new 
        public virtual ICollection<Prescription> Prescription { get; set; }
    }
}
