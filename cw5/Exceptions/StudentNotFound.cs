

using System;

namespace cw5.Exceptions
{
    public class StudentNotFound : Exception
    {
        public StudentNotFound(string message) : base(message)
        {
            
        }
    }
}
