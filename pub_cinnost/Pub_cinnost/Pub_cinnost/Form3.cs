using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            System.Net.NetworkCredential cred = new System.Net.NetworkCredential("kamil.zidek@gmail.com", "joneson55");
            mail.To.Add("zidek.kamil@gmail.com");
            mail.Subject = "Ziadost o licenciu: " + textBox1.Text;
            mail.From = new System.Net.Mail.MailAddress("kamil.zidek@gmail.com");
            mail.IsBodyHtml = true;
            mail.Body = "Ziadost o licenciu: " + textBox1.Text;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = cred;
            smtp.Port = 587;
            smtp.Send(mail);
            MessageBox.Show("Požiadavka na vygenerovanie hesla bola poslana");
        }
    }
}
