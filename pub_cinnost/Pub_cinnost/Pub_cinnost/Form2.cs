using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        int i = 0;

        public Form2()
        {
            InitializeComponent();
            XmlReader xmlReader2 = XmlReader.Create("body.xml");
            while (xmlReader2.Read())
            {
                if ((xmlReader2.NodeType == XmlNodeType.Element))
                {
                    if (xmlReader2.HasAttributes)
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1[0, i].Value = xmlReader2.GetAttribute("kat");
                        dataGridView1[1, i].Value = xmlReader2.GetAttribute("sk");
                        dataGridView1[2, i].Value = xmlReader2.GetAttribute("en");
                        dataGridView1[3, i].Value = xmlReader2.GetAttribute("vyz");
                        i++;
                    }
                    //MessageBox.Show(xmlReader.GetAttribute("name") + ": " + xmlReader.GetAttribute("surname") + +xmlReader.GetAttribute("pass"));
                }
            }
            xmlReader2.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.Links.Add(24, 9, "http://www.youtube.com/watch?v=YSLnEaehczI");
            System.Diagnostics.Process.Start("http://www.youtube.com/watch?v=YSLnEaehczI");   
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.Links.Add(24, 9, "http://193.87.95.36/fvt/publish.htm");
            System.Diagnostics.Process.Start("http://193.87.95.36/fvt/publish.htm");   
        }
    }
}
