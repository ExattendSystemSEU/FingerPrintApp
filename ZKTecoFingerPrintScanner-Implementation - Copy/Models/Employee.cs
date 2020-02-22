using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZKTecoFingerPrintScanner_Implementation.Models
{
    public class Employee
    {
        public int Emp_ID { get; set; }
        public string Emp_Password { get; set; }
        public int Emp_National_Num { get; set; }
        public string Emp_First_Name { get; set; }
        public string Emp_Middle_Name { get; set; }
        public string Emp_Last_Name { get; set; }
        public string Emp_Email { get; set; }
        public string Emp_Gender { get; set; }
        public float Emp_Salary { get; set; }
        public int Pos_ID { get; set; }
    }

}
