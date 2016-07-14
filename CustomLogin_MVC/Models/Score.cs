using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLogin_MVC.Models
{
    public partial class Score
    {
        public int ScoreId { get; set; }
        public string Implementation { get; set; }
        public string Usability { get; set; }
        public string Innovation { get; set; }
        public string Comments { get; set; }
        public int TeamId { get; set; }
        public int MentorId { get; set; }

        public virtual Team Team { get; set; }
        public virtual Mentor Mentor { get; set; }
    }
}
