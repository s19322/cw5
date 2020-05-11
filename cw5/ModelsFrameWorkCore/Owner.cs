using System;
using System.Collections.Generic;

namespace cw5.ModelsFrameWorkCore
{
    public partial class Owner
    {
        public Owner()
        {
            Animal = new HashSet<Animal>();
        }

        public int IdOwner { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Animal> Animal { get; set; }
    }
}
