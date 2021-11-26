using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMailer_WPF
{
    public class Sender
    {
      
            private static string emailAddress = Global.myEmail;
            private static string password = Global.myEmailPassword;
            public static void SendEmail(string cypher, string ToEmail)
            {
                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress("Tester", "bobertestar@gmail.com"));
                message.To.Add(MailboxAddress.Parse(ToEmail));
                message.Subject = "GnuMailer";
                message.Body = new TextPart("plain")
                {
                    Text = $"{cypher}"
                };
                SmtpClient client = new SmtpClient();
                try
                {
                    client.Connect("smtp.gmail.com", 465, true);
                    client.Authenticate(emailAddress, password);
                    client.Send(message);
                    Console.WriteLine("Email Sent!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    
}
