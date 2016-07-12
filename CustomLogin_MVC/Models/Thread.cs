using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CustomLogin_MVC.Models
{
    public partial class Thread
    {
        
        public Thread()
        {
            this.Replies = new HashSet<Reply>();
            this.Seens = new HashSet<Seen>();
        }

        public int ThreadId { get; set; }
        
        public string User { get; set; }
        [Required(ErrorMessage = "Oneliner Cannot be empty")]
        public string Threadtitle { get; set; }
        [Required(ErrorMessage = "Please select Category")]
        public string ThreadType { get; set; }
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        [UIHint("forumeditor")]
        [Required(ErrorMessage = "Please enter Detailed Description")]
        public string ThreadText { get; set; }
        public System.DateTime ThreadTime { get; set; }
        public DateTime updatedTime { get; set; }
        public bool solved { get; set; }
        
        public virtual ICollection<Reply> Replies { get; set; }
        public virtual ICollection<Seen> Seens { get; set; }
    }
    public enum ProjectType
    {
        Android = 1,
        Windows = 2,
        Web = 3,
        Bot = 4,
        Extension = 5,
        Other = 6
    }
}
