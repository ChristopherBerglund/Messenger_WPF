using MailKit.Net.Pop3;
using MyMailer_WPF.Data;
using MyMailer_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMailer_WPF
{
    public class Reader
    {
        public static MContext context = new MContext();
        public static void ReadUnOpenedEmails(RsaEncryption Rsa)
        {
            using (var client = new Pop3Client())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
                client.Connect("pop.gmail.com", 995, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(Global.myEmail, Global.myEmailPassword);

                for (int i = 0; i < client.Count; i++)
                {
                    var message = client.GetMessage(i);
                    var body = message.GetTextBody(MimeKit.Text.TextFormat.Text);
                    string[] Data = body.Split(";;;");

                    string enCrypyted = (Data[0]);
                    string deCrypted = Rsa.Decrypter(enCrypyted);
                    int fromID = Convert.ToInt32(Data[1]);
                    int toID = Convert.ToInt32(Data[2]);

                    Mail m = new Mail(deCrypted, fromID, toID);
                    context.mails.Add(m);
                }
                context.SaveChanges();
            }
        }
    }
}
