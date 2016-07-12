using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
//using System.Web.Configuration;

namespace CustomLogin_MVC.Models
{
    public class CustomLogin_MVCContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public CustomLogin_MVCContext() : base("name=CustomLogin_MVCContext")
        {
        }
        

        public System.Data.Entity.DbSet<CustomLogin_MVC.Models.UserModel> UserModels { get; set; }
        public virtual DbSet<Thread> Threads { get; set; }
        public virtual DbSet<Reply> Replies { get; set; }
        public virtual DbSet<Seen> Seens { get; set; }
    }
}
