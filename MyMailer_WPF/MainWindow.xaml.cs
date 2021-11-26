using MyMailer_WPF.Data;
using MyMailer_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyMailer_WPF
{
    public partial class MainWindow : Window
    {
        MContext context = new MContext();
        RsaEncryption rsa = new RsaEncryption();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void infoBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReaderTxtbox.Items.Clear();
            string Name = infoBox.SelectedItem as string;
            var findID = context.users.Where(a => a.userName == Name).First();
            int ID = findID.userID;

            var messages = context.mails.Where(a => a.toID == ID).ToList();
            foreach (var message in messages)
            {
                ReaderTxtbox.Items.Add(message.message.ToString());
            }
        }

        private void getBtn_Click(object sender, RoutedEventArgs e)
        {
            infoBox.Items.Clear();
            var lista = context.users.ToList();
            foreach (var item in lista)
            {
                infoBox.Items.Add(item.userName);
            }
        }

        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            string cypher = string.Empty;
            string text = SenderTxtbox.Text;
            string Name = infoBox.SelectedItem as string;
            User user = context.users.Where(a => a.userName == Name).First();
            cypher = rsa.Encrypter(text);
            var sw = new StringBuilder();
            sw.Append(cypher + ";;;");
            sw.Append(1 + ";;;");
            sw.Append(user.userID + ";;;");
            
            Sender.SendEmail(sw.ToString(), user.userEmail);

            Mail m = new Mail(text, 1, user.userID);
            context.mails.Add(m);
            context.SaveChanges();
            SenderTxtbox.Clear();
            ReaderTxtbox.Items.Clear();
            var lista = context.mails.Where(a => a.toID == user.userID).ToList();
            foreach (var item in lista)
            {
                ReaderTxtbox.Items.Add(item.message);
            }
        }

        private void ReaderTxtbox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void GetMailBtn_Click(object sender, RoutedEventArgs e)
        {
            Reader.ReadUnOpenedEmails(rsa);
        }
    }
}
