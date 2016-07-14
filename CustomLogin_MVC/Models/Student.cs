using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLogin_MVC.Models
{
    public partial class Student
    {
        public int StudentId { get; set; }
        public string sPhone { get; set; }
        public string sCollege { get; set; }
        public string sDepartment { get; set; }
        public string UserId { get; set; }
        public int TeamId { get; set; }

        public virtual Team Team { get; set; }
    }
}
