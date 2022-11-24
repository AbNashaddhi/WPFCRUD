using System;
using System.Collections.Generic;
using System.Text;

namespace WPFCRUDOP
{
   public  class Employee
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Age { get; set; }
        public virtual string Salary { get; set; }
        public virtual string Designation { get; set; }
    }
}
