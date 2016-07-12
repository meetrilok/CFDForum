using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CustomLogin_MVC.Models
{
    public partial class Reply
    {
        public int ReplyId { get; set; }
        [Required(ErrorMessage ="Name Cannot be empty")]
        public string ReplyUser { get; set; }
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        [UIHint("tinymce_full")]
        [Required(ErrorMessage ="Comment field should not be empty")]
        public string ReplyText { get; set; }
        public System.DateTime ReplyTime { get; set; }
        public bool Answer { get; set; }
        public bool AuthReply { get; set; }

        public int ThreadId { get; set; }

        public virtual Thread Thread { get; set; }
    }
}
