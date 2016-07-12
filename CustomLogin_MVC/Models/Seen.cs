using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLogin_MVC.Models
{
    public partial class Seen
    {
        public int SeenId { get; set; }
        public int ThreadId { get; set; }
        public Guid UserId { get; set; }
        public virtual Thread Thread { get; set; }
        public virtual UserModel UserModel { get; set; }
    }
}
