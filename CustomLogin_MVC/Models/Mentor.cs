using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLogin_MVC.Models
{
    public partial class Mentor
    {
        
        public Mentor()
        {
            this.Scores = new HashSet<Score>();
        }

        public int MentorId { get; set; }
        public string mDepartment { get; set; }
        public string mExpertise { get; set; }
        public string UserId { get; set; }

       
        public virtual ICollection<Score> Scores { get; set; }
    }
}
