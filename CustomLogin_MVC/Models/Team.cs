using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLogin_MVC.Models
{
    public partial class Team
    {
        
        public Team()
        {
            this.Students = new HashSet<Student>();
            this.Scores = new HashSet<Score>();
        }

        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string ProjType { get; set; }
        public string ProjDescription { get; set; }
        public string ProjUsp { get; set; }

        
        public virtual ICollection<Student> Students { get; set; }
        
        public virtual ICollection<Score> Scores { get; set; }
    }
}
