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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string name = textBox5.Text;
            string surname = textBox6.Text;
            int lenght = 0;

            if (name.Length > surname.Length)
                lenght = surname.Length;
            else
                lenght = name.Length;
            int[] nameb = new int[lenght];
            int[] surnameb = new int[lenght];
            int[] passw = new int[lenght];
            int genpass = 0;
            for (int i = 0; i < (lenght - 1); i++)
            {
                nameb[i] = (int)name[i];
                surnameb[i] = (int)surname[i];
                passw[i] = nameb[i] + surnameb[i];
            }
            for (int i = 0; i < (lenght - 1); i++)
            {
                genpass += Convert.ToInt32(Math.Pow(Convert.ToDouble(passw[i]), 3)) + Convert.ToInt32(Math.Pow(Convert.ToDouble(passw[i + 1]), 3));
            }
            genpass = genpass + 321;

            if (textBox1.Text == genpass.ToString())
            {
                MessageBox.Show("Aplikacia odblokovana");
                //button6.Enabled = false;
                //button14.Enabled = false;
                //button5.Enabled = false;
                //textBox7.Enabled = false;
                //textBox11.Enabled = false;
                this.Close();
                WindowsFormsApplication1.Form1.ActiveForm.Focus();
                WindowsFormsApplication1.Form1.ActiveForm.Enabled = true;
            }
            else
            {
                MessageBox.Show("Zla licencia");
                this.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "xxxxxx")
            {
                this.Size = new System.Drawing.Size(300, 300);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = textBox2.Text;
            string surname = textBox3.Text;
            int lenght = 0;
            
            if (name.Length > surname.Length)
                lenght = surname.Length;
            else
                lenght = name.Length;
            int[] nameb = new int[lenght];
            int[] surnameb = new int[lenght];
            int[] passw = new int[lenght];
            int genpass = 0;
            for (int i = 0; i < (lenght-1); i++)
            {
                nameb[i] = (int)name[i];
                surnameb[i] = (int)surname[i];
                passw[i] = nameb[i] + surnameb[i];
            }
            for (int i = 0; i < (lenght-1); i++)
            {
                genpass += Convert.ToInt32(Math.Pow(Convert.ToDouble(passw[i]),3)) + Convert.ToInt32(Math.Pow(Convert.ToDouble(passw[i + 1]),3));
            }
            genpass = genpass + 321;

            textBox4.Text = genpass.ToString();
        }
    }
}
