using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMailer_WPF.Model
{
    public class User
    {
        [Key]
        public int userID { get; set; }
        public string userName { get; set; }
        public string userEmail { get; set; }
    }
}
