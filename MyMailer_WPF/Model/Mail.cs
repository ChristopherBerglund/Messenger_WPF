using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMailer_WPF.Model
{
    public class Mail
    {
        [Key]
        public int mailID { get; set; }
        public string message { get; set; }
        public int  fromID { get; set; }
        public int toID { get; set; }
        public DateTime time { get; set; } = DateTime.Now;  
        public Mail(string message, int fromID, int toID)
        {
            this.message = message;
            this.fromID = fromID;
            this.toID = toID;
        }
    }
}
