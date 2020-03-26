using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZKTecoFingerPrintScanner_Implementation.Models
{
    /// <summary>
    ///  بيانات الموظف
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// رقم الهوية
        /// </summary>
        public int Emp_ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Emp_Password { get; set; }
        public string Emp_First_Name { get; set; }
        public string Emp_Middle_Name { get; set; }
        public string Emp_Last_Name { get; set; }
        public string Emp_Email { get; set; }
        public string Emp_Gender { get; set; }
        public float Emp_Salary { get; set; }
        public int Pos_ID { get; set; }
    }

}
