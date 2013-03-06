using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Net;
using System.Text.RegularExpressions;
using System.Net.Cache;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Bitmap bm;
        publikacie[] arr;
        citacie[] arr2;
        int lengh = 0;
        int lengh2 = 0;
        string[] txt = new string[3000];
        string[] txt2 = new string[3000];
        string[] txtp = new string[3000];
        string[] txt2p = new string[3000];
        string[] txt3 = new string[20000];
        string[,] duplcheck = new string[2, 50];
        string[,] duplcheck2 = new string[2, 50];
        int inxdup = 0;
        int inxdup2 = 0;
        int poc;
        int poc2;
        int str;
        double fbody;
        bool c = false;
        double per;
        bool complflag =false;
        int incr = 2;
        string jaz2;
        string zdroj2;
        double body2;
        double per2;
        double fbody2;
        int pcit2;
        int rid0;
        ThreadStart vlaknodel;
        Thread vlakno1;
        bool timerflag = false;
        int ip = 0;
        int ip2 = 0;
        bool nav2 = false;
        bool lvl1 = false;
        bool lvl2 = false;
        bool lvl3 = false;
        DataGridViewCheckBoxColumn chk;
        bool repflag = false;
        bool por1 = false;
        bool por2 = false;
        string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public static DateTime GetNistTime()
        {
            DateTime dateTime = DateTime.MinValue;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://nist.time.gov/timezone.cgi?UTC/s/0");
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
            request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore); //No caching
            try
            {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader stream = new StreamReader(response.GetResponseStream());
                    string html = stream.ReadToEnd().ToUpper();
                    string time = Regex.Match(html, @">\d+:\d+:\d+<").Value; //HH:mm:ss format
                    string date = Regex.Match(html, @">\w+,\s\w+\s\d+,\s\d+<").Value; //dddd, MMMM dd, yyyy
                    dateTime = DateTime.Parse((date + " " + time).Replace(">", "").Replace("<", ""));
                }
            }
            catch
            {
                MessageBox.Show("Problem s pripojením na internet");
            }

            return dateTime;
        }

        double getkat(string Kat, string Lan)
        {
            string[] kat2 = new string[49];
            string[] sk2 = new string[49];
            string[] en2 = new string[49];
            string[] vyz2 = new string[49];
            int i = 0;
            XmlReader xmlReader3 = XmlReader.Create("body.xml");
            while (xmlReader3.Read())
            {
                if ((xmlReader3.NodeType == XmlNodeType.Element))
                {
                    if (xmlReader3.HasAttributes)
                    {
                        kat2[i] = xmlReader3.GetAttribute("kat");
                        sk2[i] = xmlReader3.GetAttribute("sk");
                        en2[i] = xmlReader3.GetAttribute("en");
                        vyz2[i] = xmlReader3.GetAttribute("vyz");
                        i++;
                    }
                }
            }
            xmlReader3.Close();

            double points = 0;
            for (int ic = 0; ic < kat2.Length; ic++)
            {
                if (Kat == kat2[ic])
                {
                    if (Lan == "sk")
                    {
                        points = double.Parse(sk2[ic], System.Globalization.CultureInfo.InvariantCulture); //SK
                    }
                    if (Lan == "en") //EN
                    {
                        points = double.Parse(en2[ic], System.Globalization.CultureInfo.InvariantCulture);
                    }
                    if (c == true) //vyznamna
                    {
                        points = double.Parse(vyz2[ic], System.Globalization.CultureInfo.InvariantCulture);
                    }
                    break;
                }
            }
            return points;
        }

        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false; 
            //this.label35.Text =
            //Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            //MessageBox.Show(String.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Revision, version.Build));
            
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed) {
            System.Deployment.Application.ApplicationDeployment ad = System.Deployment.Application.ApplicationDeployment.CurrentDeployment;
            this.label35.Text = "v. " + ad.CurrentVersion.ToString(); }

            ToolTip buttonToolTip = new ToolTip();
            buttonToolTip.ToolTipTitle = "Nápoveda";
            buttonToolTip.UseFading = false;
            buttonToolTip.UseAnimation = false;
            buttonToolTip.IsBalloon = true;
            buttonToolTip.ShowAlways = true;
            buttonToolTip.AutoPopDelay = 5000;
            buttonToolTip.InitialDelay = 1000;
            buttonToolTip.ReshowDelay = 500;
            buttonToolTip.SetToolTip(button11, "Uloží údaje.");
            int ixx = 0;
            string nam = "xxxxxxxx ";
            string surn = "xxxxxxxx ";
            string id = "xxxxxxxx ";
            string pa = "xxxxxxxx ";
            string ya = "xxxxxxxx ";
            string li = "xxxxxxxx ";
            XmlReader xmlReader;
            if (File.Exists(path +"\\data2.xml")) //C:\\
            {
                xmlReader = XmlReader.Create(path + "\\data2.xml"); //C:\\
            }
            else
            {
                xmlReader = XmlReader.Create("data.xml");
            }
            while (xmlReader.Read())
            {
                if ((xmlReader.NodeType == XmlNodeType.Element))
                {
                    
                    /*if (xmlReader.HasAttributes)
                    /{
                        li = xmlReader.GetAttribute("lic");
                        ya = xmlReader.GetAttribute("year");
                        nam = xmlReader.GetAttribute("name");
                        surn = xmlReader.GetAttribute("surname");
                        id = xmlReader.GetAttribute("id");
                        pa = xmlReader.GetAttribute("pass");                       
                    }*/
                    
                    for (int i = 0; i < xmlReader.AttributeCount; i++)
                    {
                        //MessageBox.Show(xmlReader[i]);
                        //nam = xmlReader[i];
                        nam = xmlReader[i];
                        i = i + 1;
                        surn = xmlReader[i];
                        i = i + 1;
                        id = xmlReader[i];
                        i = i + 1;
                        pa = xmlReader[i];
                        i = i + 1;
                        ya = xmlReader[i];
                        i = i + 1;
                        li = xmlReader[i];
                    }
                }
            }
            xmlReader.Close();
            textBox13.Text = li;
            textBox5.Text = id;
            textBox6.Text = pa;
            textBox7.Text = surn;
            textBox11.Text = nam;
            textBox8.Text = ya;

            if (File.Exists(path + "\\citacie.txt")) //C:\\
            {
                DateTime dt = File.GetLastWriteTime(path + "\\clanky.txt"); //C:\\
            label39.Text = dt.ToString();
            }
            else
            {
                label39.Text = "bez zalohy dát";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fileName = "test.txt";
            FileStream fs = new FileStream(fileName,FileMode.Open);
            // Nadstavba citania na FileStream 
            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding(1250));
            string temp=null;
            // Citanie po riadkoch
            while( (temp=sr.ReadLine()) !=null)
            {
            richTextBox1.AppendText(temp + Environment.NewLine);
            txt[lengh] = temp;
            lengh++;
            }
            sr.Close();
            progressBar1.Value = 60;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label12.BackColor = Color.White;
            label13.BackColor = Color.Red;
            string RichTextBoxContents = richTextBox1.Text;
            string[] RichTextBoxLines = richTextBox1.Lines;
            int y = 0;
            foreach (string line in RichTextBoxLines)
            {
                txt[y] =  line;
                y++;
            }

            int cl = 0;
            int por = 1;
            
            for (int i = 0; i < txt.Length; i++)
            {
                int konnaz3 = (txt[i]).IndexOf("znamov"); //Najdi koniec nazvu
                if (konnaz3 >= 0) //Najdi zaciatok clanku
                {
                    rid0 = i; //riadok cisla clanku
                    break;
                }
            }
                 
            int rid =4+rid0+1; //4

            poc = int.Parse(txt[rid0].Remove(0,27)); //[0]
            label24.Text = poc.ToString();
            arr = new publikacie[poc];
         
            for (int i = 0; i < poc; i++)
            {

                string kat = (txt[rid + 1]).Substring(10, 3); //kategoria 9
                int konnaz = (txt[rid + 1]).IndexOf("/") - 1; //Najdi koniec nazvu
                string naz = (txt[rid + 1]).Substring(21, konnaz-21); //Nazov
                textBox2.Text = textBox7.Text.ToUpper() + ", " + textBox11.Text;
                int konper = (txt[rid + 2]).IndexOf(textBox2.Text) + textBox2.Text.Length + 2; //Najdi zaciatok percent
                
                if (txt[rid + 2].Substring(konper + 1, 1) == "%") //osetrenie < 10 percent
                {
                    per = double.Parse((txt[rid + 2]).Substring(konper, 1)); //Percenta
                }
                if (txt[rid + 2].Substring(konper + 2, 1) == "0") //osetrenie 100 percent
                {
                    per = double.Parse((txt[rid + 2]).Substring(konper, 3)); //Percenta
                }
                if (txt[rid + 2].Substring(konper + 2, 1) == "%") //osetrenie 100 percent
                {
                    per = double.Parse((txt[rid + 2]).Substring(konper, 2)); //Percenta
                }
                string lan = "en";
                string []spec = {"á", "é", "í", "ý", "ô", "ľ", "š", "č", "ž", "ť", "ó", "ň", "ô", "ä"};
                for (int ix = 0; ix < spec.Length; ix++)
                {
                    int a = naz.IndexOf(spec[ix]);
                    if (a > 0)
                    {
                        lan = "sk";
                        break;
                    }
                }
                fbody = getkat(kat, lan);
                arr[cl] = new publikacie(por++, kat, naz, lan, per, fbody, ((per / 100) * fbody));
                cl++;
                rid = rid + 5;
            }
            
            dataGridView1.DataSource = arr;  //Naplnenie pola hodnotami
            chk = new DataGridViewCheckBoxColumn();
            chk.HeaderText = "Vyz. pub.";
            chk.Name = "chk";
            //chk.TrueValue = "1";

            dataGridView1.Columns.Add(chk);
            button2.Enabled = false;
            button3.Enabled = true;
            button6.Enabled = true;
            //richTextBox1.Clear();
            progressBar1.Value = 72;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label13.BackColor = Color.White;
            label14.BackColor = Color.Red;
            double sucet = 0;
            for(int i =0; i < poc ; i++)
            {
                c = Convert.ToBoolean(dataGridView1[ 7, i].Value);
                if (c == true) //prepocitaj pri zaciarknuti
                {
                    string kat1 = Convert.ToString(dataGridView1[1, i].Value);
                    string lan1 = Convert.ToString(dataGridView1[3, i].Value);
                    fbody = getkat(kat1, lan1);
                    double pern = Convert.ToDouble(dataGridView1[4, i].Value);
                    dataGridView1[5, i].Value = fbody;
                    dataGridView1[6, i].Value = Math.Round((pern / 100) * fbody, 2);
                }
                else //prepocitaj po odciarknuti
                {
                    string kat1 = Convert.ToString(dataGridView1[1, i].Value);
                    string lan1 = Convert.ToString(dataGridView1[3, i].Value);
                    fbody = getkat(kat1, lan1);
                    double pern = Convert.ToDouble(dataGridView1[4, i].Value);
                    dataGridView1[5, i].Value = fbody;
                    dataGridView1[6, i].Value = Math.Round((pern / 100) * fbody, 2);
                }
                sucet = sucet + arr[i].Body;
                textBox1.Text = sucet.ToString();
            }
            button10.Enabled = true;
            button15.Enabled = true;
            progressBar1.Value = 84;
        }

        private void button4_Click(object sender, EventArgs e)
        { //nacitaj data pre citacie
            string fileName = "test2.txt";
            FileStream fs = new FileStream(fileName, FileMode.Open);
            // Nadstavba citania na FileStream 
            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding(1250));
            string temp = null;
            // Citanie po riadkoch
            while ((temp = sr.ReadLine()) != null)
            {
                richTextBox2.AppendText(temp + Environment.NewLine);
                txt2[lengh] = temp;
                lengh++;
            }
            sr.Close();
        }

        private void koniecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.Close();
        }

        private void navodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 Navod = new Form2();
            Navod.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string[,] duplcheck = new string[2, 50];
            string[,] duplcheck2 = new string[2, 50];
            inxdup = 0;
            inxdup2 = 0;
            //dataGridView2.Columns.Clear();
            label19.BackColor = Color.White;
            label21.BackColor = Color.Red;
            
            //zisti pocet stran
            string RichTextBoxContents3 = richTextBox2.Text;
            string[] RichTextBoxLines3 = richTextBox2.Lines;
            int y3 = 0;
            foreach (string line in RichTextBoxLines3)
            {
                txt3[y3] = line;
                y3++;
            }
            

            int cl2 = 0;
            int por = 1;
            int rid = 0;
            int riad = 0;
            int pcit = 0;
            string kat = "0";
            pcit2 = 0;
            
            for (int iy = 0; iy < txt3.Length; iy++) //ziska posledny riadok 
            {
                if (txt3[iy] == null)
                {
                    riad = iy;
                    break;
                }
            }

            for (int iz = 0; iz < riad; iz++) //zisti pocet citacii pre dany rok
            {
                int konnaz = (txt3[iz]).IndexOf(textBox3.Text + " [1");
                int konnaz2 = (txt3[iz]).IndexOf(textBox3.Text + " [2");
                if (konnaz == 0 || konnaz2 == 0) //ak najde citaciu
                {
                    pcit2++;
                }
            }

            arr2 = new citacie[pcit2]; //vytvori urcity pocet poloziek pre citacie
            
            for(int iz= 0; iz < riad; iz++) //vyhlada citacie
            {
                int konnaz = (txt3[iz]).IndexOf(textBox3.Text + " [1");
                int konnaz9 = (txt3[iz]).IndexOf(textBox3.Text + " [2");
                if (konnaz == 0 || konnaz9 == 0) //ak najde citaciu
                {
                    string kat2 = (txt3[iz]).Substring(6, 1);
                    if (int.Parse(kat2) == 3 || int.Parse(kat2) == 4) //jazyk citacie
                    {
                        jaz2 = "SK";
                    }
                    else
                    {
                        jaz2 = "EN";
                    }
                    if (int.Parse(kat2) == 1)
                    {
                        duplcheck[0, inxdup] = txt3[iz].Substring(9, 80);
                        duplcheck[1, inxdup] = cl2.ToString();
                        inxdup++;
                        body2 = 2; //zmena 2013
                    }
                    if (int.Parse(kat2) == 2)
                    {
                        duplcheck2[0, inxdup2] = txt3[iz].Substring(9, 80);
                        duplcheck2[1, inxdup2] = cl2.ToString();
                        inxdup2++;
                        body2 = 2; //zmena 2013
                    }
                    if (int.Parse(kat2) == 3)
                    {
                        body2 = 0f;
                    }
                    if (int.Parse(kat2) == 4)
                    {
                        body2 = 0f;
                    }
                    int konnaz2 = (txt3[iz]).IndexOf(" In: "); //kde publikovana
                    int konnaz3 = (txt3[iz]).IndexOf(" : ");
                    string cit2 = "x";
                    if (konnaz3 < 0)
                    {
                        konnaz3 = (txt3[iz]).IndexOf("N: ");
                        cit2 = txt3[iz].Substring(konnaz2, konnaz3-konnaz2-25);
                    }
                    else
                    {
                        cit2 = txt3[iz].Substring(konnaz2, konnaz3 - konnaz2);
                    }
                    string naz2 = txt3[iz].Substring(9,80);
                    int iztst = iz;
                    for (int i = 0; i < 50; i++) //Najde zaciatok kde je nazov citovaneho clanku
                    {
                        iztst = iztst-2;
                        int konnaz4 = (txt3[iztst]).IndexOf("[");
                        if (konnaz4 == 0)
                        {
                            int konnaz5 = (txt3[iztst]).IndexOf((textBox7.Text).ToUpper() + ", " + textBox11.Text);
                            per2 = int.Parse((txt3[iztst]).Substring(konnaz5 + (textBox7.Text).Length + (textBox11.Text).Length + 4, 2));
                            if((txt3[iztst]).Substring(konnaz5 + (textBox7.Text).Length + (textBox11.Text).Length + 4, 3)=="100")//100
                            {
                                per2 = int.Parse((txt3[iztst]).Substring(konnaz5 + (textBox7.Text).Length + (textBox11.Text).Length + 4, 3));
                            }
                            if ((txt3[iztst]).Substring(konnaz5 + (textBox7.Text).Length + (textBox11.Text).Length + 5, 1) == "%")//5
                            {
                                per2 = int.Parse((txt3[iztst]).Substring(konnaz5 + (textBox7.Text).Length + (textBox11.Text).Length + 4, 1));
                            }
                            int konnaz6 = (txt3[iztst-1]).IndexOf("/");
                            if ((txt3[iztst - 1]).Substring(20, 1) == " ")
                            {
                                zdroj2 = (txt3[iztst - 1]).Substring(21, konnaz6 - 21);
                            }
                            else
                            {
                                zdroj2 = (txt3[iztst - 1]).Substring(20, konnaz6 - 20);
                            }
                            break;
                        }
                    }
                    arr2[cl2] = new citacie(cl2+1, kat2, naz2, cit2, jaz2, zdroj2 , body2, per2, fbody2);
                    cl2++;
                    pcit++;
                }
            }
            int ip = 0;
            for (int i = 0; !string.IsNullOrEmpty(duplcheck[1, i]); i++) //check same citation
            {
                for (int iy = 0; !string.IsNullOrEmpty(duplcheck[1, iy]); iy++)
                {
                    if (duplcheck[0, i] == duplcheck[0, ip+1])
                    {
                        arr2[int.Parse(duplcheck[1,ip+1])].Body = 2; //zmena 2013 0.5
                        duplcheck[0, ip + 1] = i.ToString();
                    }
                }
                ip++;
            }
            ip = 0;
            for (int i = 0; !string.IsNullOrEmpty(duplcheck2[1, i]); i++) //check same citation
            {
                for (int iy = 0; !string.IsNullOrEmpty(duplcheck2[1, iy]); iy++)
                {
                    if (duplcheck2[0, i] == duplcheck2[0, ip + 1])
                    {
                        arr2[int.Parse(duplcheck2[1, ip+1])].Body = 2; //zmena 0.25
                        duplcheck2[0, ip + 1] = i.ToString();
                    }
                }
                ip++;
            }
            ip = 0;
            for (int i=0; i < arr2.Length; i++) //zaokruhli body na 5 desatinnych miest
            {
                arr2[i].FBody = Math.Round((arr2[i].Per / 100 * arr2[i].Body), 5);
            }
            dataGridView2.DataSource = arr2;
            label30.Text = pcit.ToString();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            webBrowser1.AllowNavigation = true;
            webBrowser1.Navigate("https://epc.lib.tuke.sk/Login.aspx?from=/PrehladPubl.aspx");
            
            //HtmlElement buttonw1 = webBrowser1.Document.All["refreshPage"]; //refreshPage
            //buttonw1.InvokeMember("click");
            //webBrowser1.Navigate("javascript:clickRefresh()");
            button6.Enabled = false;
            button7.Enabled = true;
            progressBar1.Value = 12;
            label6.BackColor = Color.DarkGray;
            label7.BackColor = Color.Red;
            
            //MessageBox.Show("finish");
            //while (webBrowser1.ReadyState != webBrowser1.DocumentCompleted) ;
            //while (webBrowser1.IsBusy != false);
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            label7.BackColor = Color.DarkGray;
            label8.BackColor = Color.Red;
            HtmlElement textArea = webBrowser1.Document.All["tbUserName"];
            if (textArea != null)
            {
                textArea.InnerText = textBox5.Text;
            }
            HtmlElement textArea2 = webBrowser1.Document.All["tbPassword"];
            if (textArea != null)
            {
                textArea2.InnerText = textBox6.Text;
            }
            HtmlElement buttonw = webBrowser1.Document.All["bLogin"]; //refreshPage
            buttonw.InvokeMember("click");
            button7.Enabled = false;
            button8.Enabled = true;
            progressBar1.Value = 24;
            button14.Enabled = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            label8.BackColor = Color.DarkGray;
            label9.BackColor = Color.Red;
            HtmlElement textArea3 = webBrowser1.Document.All["ctl00_ContentPlaceHolderMain_tbHladanaFraza"];
            if (textArea3 != null)
            {
                Clipboard.SetText(textBox7.Text + ", " + textBox11.Text);
                textBox2.Text = textBox7.Text.ToUpper() + ", " + textBox11.Text;
                textArea3.Focus();
                SendKeys.SendWait("^v");
            }

            HtmlElement buttonw3 = webBrowser1.Document.All["ctl00_ContentPlaceHolderMain_Image1"];
            buttonw3.InvokeMember("click");

            HtmlElement textArea4 = webBrowser1.Document.All["ctl00_ContentPlaceHolderMain_tbRokVydaniaOd"];
            if (textArea4 != null)
            {
                textArea4.InnerText = textBox8.Text;
            }

            HtmlElement textArea5 = webBrowser1.Document.All["ctl00_ContentPlaceHolderMain_tbRokVydaniaDo"];
            if (textArea5 != null)
            {
                textArea5.InnerText = textBox8.Text;
            }

            HtmlElement check1 = webBrowser1.Document.All["ctl00_ContentPlaceHolderMain_chbAjPercentualnePodiely"];
            check1.InvokeMember("click");

            HtmlElement buttonw2 = webBrowser1.Document.All["ctl00_ContentPlaceHolderMain_btnHladaj"];
            buttonw2.InvokeMember("click");

            button8.Enabled = false;
            button9.Enabled = true;
            progressBar1.Value = 36;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            por1 = true;
            richTextBox1.Clear();
            textBox2.Text = textBox7.Text.ToUpper() + ", " + textBox11.Text;
            string name = textBox11.Text;
            string surname = textBox7.Text;
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

            if (textBox13.Text == genpass.ToString() || textBox5.Text == "kz751zf" )
            {
                label9.BackColor = Color.DarkGray;
                label12.BackColor = Color.Red;
                webBrowser1.Focus();
                SendKeys.SendWait("^a");
                SendKeys.SendWait("^c");
                richTextBox1.Paste(DataFormats.GetFormat("Text"));
                tabControl1.SelectTab(1);
                button9.Enabled = false;
                button2.Enabled = true;
                button2_Click(sender,e); //test
                progressBar1.Value = 48;
            }
            else
            {
                DateTime test = GetNistTime();
                if ((test.Year > 2012) && textBox13.Text == genpass.ToString()) 
                {
                    label9.BackColor = Color.DarkGray;
                    label12.BackColor = Color.Red;
                    webBrowser1.Focus();
                    SendKeys.SendWait("^a");
                    SendKeys.SendWait("^c");
                    richTextBox1.Paste(DataFormats.GetFormat("Text"));
                    tabControl1.SelectTab(1);
                    button9.Enabled = false;
                    button2.Enabled = true;
                    button2_Click(sender, e); //test
                    progressBar1.Value = 48;
                }
                else if ((test.Year == 2012)) //test.Day <= 21 && test.Month <= 12 &&
                {
                    label9.BackColor = Color.DarkGray;
                    label12.BackColor = Color.Red;
                    webBrowser1.Focus();
                    SendKeys.SendWait("^a");
                    SendKeys.SendWait("^c");
                    richTextBox1.Paste(DataFormats.GetFormat("Text"));
                    tabControl1.SelectTab(1);
                    button9.Enabled = false;
                    button2.Enabled = true;
                    button2_Click(sender, e); //test
                    progressBar1.Value = 48;
                }
                else
                {
                    MessageBox.Show("Neplatne licencne cislo");
                    MessageBox.Show("Beta fungovala do 31.12.2012, po tomto datume bude mozne vygenerovat len body konkretneho uzivatela a iba s licencnym cislom", "Kontrola Licencie", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            try
            {
                if (repflag == true)
                {
                    dataGridView1.Columns.Remove(chk.Name);
                }
            }
            catch { }
            repflag = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (por1 == true && por2 == true && File.Exists(path + "\\clanky.txt") && File.Exists(path + "\\citacie.txt")) //C:\\
            {
                button26.Enabled = true;
            }
            textBox9.Text = (double.Parse(textBox4.Text) * double.Parse(textBox10.Text)).ToString();
            progressBar1.Value = 100;
            button10.Enabled = true;
            tabControl1.SelectTab(0);
            button25.Enabled = true;
            label40.BackColor = Color.White;
            label40.BackColor = Color.Red;
            label22.BackColor = Color.Gray;
            button18.Enabled = true;
            button19.Enabled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            button7.Enabled = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            File.Delete("data.xml");
            XmlWriter xmlWriter = XmlWriter.Create("data.xml");
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("users");
            xmlWriter.WriteStartElement("user");
            xmlWriter.WriteAttributeString("name", textBox11.Text);
            xmlWriter.WriteAttributeString("surname", textBox7.Text);
            xmlWriter.WriteAttributeString("id", textBox5.Text);
            xmlWriter.WriteAttributeString("pass", textBox6.Text);
            xmlWriter.WriteAttributeString("year", textBox8.Text);
            xmlWriter.WriteAttributeString("lic", textBox13.Text);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            File.Copy("data.xml", path +"\\data2.xml", true); //C:\\
        }

        private void button12_Click(object sender, EventArgs e)
        {
            button12.Enabled = false;
            button13.Enabled = true;
            label17.BackColor = Color.DarkGray;
            label18.BackColor = Color.Red;
            HtmlElement textArea3 = webBrowser1.Document.All["ctl00_ContentPlaceHolderMain_tbHladanaFraza"];
            if (textArea3 != null)
            {
                Clipboard.SetText(textBox7.Text + ", " + textBox11.Text);
                textBox2.Text = textBox7.Text.ToUpper() + ", " + textBox11.Text;
                textArea3.Focus();
                SendKeys.SendWait("^v");
            }

            HtmlElement buttonw3 = webBrowser1.Document.All["ctl00_ContentPlaceHolderMain_Image1"];
            buttonw3.InvokeMember("click");
            /*
            HtmlElement textArea4 = webBrowser1.Document.All["ctl00_ContentPlaceHolderMain_tbRokVydaniaOd"];
            if (textArea4 != null)
            {
                textArea4.InnerText = textBox8.Text;
            }

            HtmlElement textArea5 = webBrowser1.Document.All["ctl00_ContentPlaceHolderMain_tbRokVydaniaDo"];
            if (textArea5 != null)
            {
                textArea5.InnerText = textBox8.Text;
            }
            */ 
            //ctl00_ContentPlaceHolderMain_chbOhlasy
            //ctl00_ContentPlaceHolderMain_chbAjPercentualnePodiely
            HtmlElement check1 = webBrowser1.Document.All["ctl00_ContentPlaceHolderMain_chbAjPercentualnePodiely"];
            check1.InvokeMember("click");

            HtmlElement check2 = webBrowser1.Document.All["ctl00_ContentPlaceHolderMain_chbOhlasy"];
            check2.InvokeMember("click");

            HtmlElement buttonw2 = webBrowser1.Document.All["ctl00_ContentPlaceHolderMain_btnHladaj"]; //ctl00_ContentPlaceHolderMain_ddlKrit1    ctl00_ContentPlaceHolderMain_tbHladanaFraza_TextBoxWatermarkExtender_ClientState ctl00_ContentPlaceHolderMain_tbHladanaFraza
            buttonw2.InvokeMember("click");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            por2 = true; //flag pre tlac obnovu
            richTextBox2.Clear();
            //button13.Enabled = false;
            button5.Enabled = true;
            label18.BackColor = Color.DarkGray;
            label19.BackColor = Color.Red;
            //extrahuj prvu stranku
            webBrowser1.Focus();
            webBrowser1.Document.Body.Focus();
            HtmlElement textArea3 = webBrowser1.Document.All["ctl00_UpdatePanelMenu"];
            textArea3.InvokeMember("click");
            SendKeys.SendWait("^a");
            SendKeys.SendWait("^c");
            //richTextBox2.Paste();
            richTextBox2.Paste(DataFormats.GetFormat("Text"));
            button5_Click(sender,e);
            tabControl1.SelectTab(2);
            label17.BackColor = Color.Gray; 
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            complflag = true;
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            complflag = false;
        }

        void fciavlakno()
        {
            Found:
            while (true)
            {
                Thread.Sleep(2500);
                if (complflag == true)
                {
                    if (incr <= str)
                    {
                        webBrowser1.Navigate("javascript:__doPostBack('ctl00$ContentPlaceHolderMain$gvVystupyByFilter','Page$" + (incr).ToString() + "')");
                        complflag = false;
                        //timer1.Enabled = false;
                        while (true)
                        {
                            Thread.Sleep(2500);
                            if (complflag == false)
                            {
                                //Thread.Sleep(2000);
                                webBrowser1.Focus();
                                SendKeys.SendWait("^a");
                                SendKeys.SendWait("^c");
                                richTextBox2.Paste();
                                incr++;
                                //vlakno1.Start();
                                //timer1.Enabled = true;
                                //timer2.Enabled = false;
                                complflag = true;
                                goto Found;
                            }
                        }
                    }
                    else
                    {
                        timerflag = true;
                        timer3.Enabled = true;
                        break;
                    }
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            label20.BackColor = Color.DarkGray;
            label17.BackColor = Color.Red;
            //webBrowser1.Navigate("https://epc.lib.tuke.sk/PrehladPubl.aspx");
            webBrowser1.Navigate("https://epc.lib.tuke.sk/EvidPubl.aspx");
            button14.Enabled = false;
            button12.Enabled = true;
            button13.Enabled = true;
         }

        private void button15_Click(object sender, EventArgs e)
        {
            label14.BackColor = Color.White;
            label20.BackColor = Color.Red;
            tabControl1.SelectTab(0);
            button14_Click(sender, e);
            button15.Enabled = false;
            button14.Enabled = true;
        }

        private void button17_Click(object sender, EventArgs e) //citacie sucet
        {
            label21.BackColor = Color.White;
            label31.BackColor = Color.Red;
            double sucet=0;
            double sucet2 = 0;
            for (int i = 0; i < pcit2; i++)
            {
                sucet = sucet + arr2[i].Body;
                sucet2 = sucet2 + arr2[i].FBody;
                
            }
            textBox12.Text = sucet2.ToString();
            //textBox14.Text = sucet2.ToString();
            button16.Enabled = true;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            label31.BackColor = Color.White;
            label22.BackColor = Color.Red;
            textBox4.Text = (double.Parse(textBox1.Text)+double.Parse(textBox12.Text)).ToString();
            progressBar1.Value = 90;
        }

        private void fullVersionRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 request = new Form3();
            request.ShowDialog();
        }

        private void unlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 request = new Form4();
            request.ShowDialog();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (timerflag == true)
            {
                tabControl1.SelectTab(2);
                timer3.Enabled = false;
            }            
        }

        private void button18_Click(object sender, EventArgs e) //Tlac citacie
        {
            tabControl1.SelectTab(2);
            ((Form)printPreviewDialog2).WindowState = FormWindowState.Maximized;
            printPreviewDialog2.PrintPreviewControl.Zoom = 1.5;
            printPreviewDialog2.ShowDialog();
        }

        private void button19_Click(object sender, EventArgs e) //Tlac clanky
        {
            tabControl1.SelectTab(1);
            ((Form)printPreviewDialog1).WindowState = FormWindowState.Maximized;
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.5;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int width = 0;
            int height = 0;
            int x = 0;
            int y = 0;
            int rowheight = 0;
            int columnwidth = 0;

            StringFormat str = new StringFormat();
            str.Alignment = StringAlignment.Near;
            str.LineAlignment = StringAlignment.Center;
            str.Trimming = StringTrimming.EllipsisCharacter;
            Pen p = new Pen(Color.Black, 2.5f);
            //e.Graphics.DrawRectangle(p,dataGridView1.Bounds);
            e.Graphics.Clear(Color.White);
            //printPreviewDialog1.Document=printDocument1;
            //e.Graphics.DrawString("Hi this a test print", new Font(Font.SystemFontName,10.5f), Brushes.Black, new PointF(250.0f, 250.0f));
            //e.Graphics.DrawImage((Image)bm,new Point(10,10));
            Font fontrep = new Font(Font.SystemFontName, 9f);
            Font fontrep2 = new Font(Font.SystemFontName, 6.5f);
            
            //Draw Column 1
            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(50, 100, dataGridView1.Columns[0].Width-20, dataGridView1.Rows[0].Height));
            e.Graphics.DrawRectangle(Pens.Black, 50, 100, dataGridView1.Columns[0].Width-20, dataGridView1.Rows[0].Height);
            e.Graphics.DrawString("P.č.", fontrep, Brushes.Black, new RectangleF(51, 99, dataGridView1.Columns[0].Width-20, dataGridView1.Rows[0].Height), str);
            //Draw column 2
            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(30 + dataGridView1.Columns[0].Width, 100, dataGridView1.Columns[0].Width-15, dataGridView1.Rows[0].Height));
            e.Graphics.DrawRectangle(Pens.Black, 30 + dataGridView1.Columns[0].Width, 100, dataGridView1.Columns[0].Width-15, dataGridView1.Rows[0].Height);
            e.Graphics.DrawString("Kat.", fontrep, Brushes.Black, new RectangleF(31 + dataGridView1.Columns[0].Width, 99, dataGridView1.Columns[0].Width-15, dataGridView1.Rows[0].Height), str);
            //Draw column 3
            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(13 + 50 + dataGridView1.Columns[0].Width, 100, dataGridView1.Columns[0].Width +450, dataGridView1.Rows[0].Height));
            e.Graphics.DrawRectangle(Pens.Black, 13 + 50 + dataGridView1.Columns[0].Width, 100, dataGridView1.Columns[0].Width + 450, dataGridView1.Rows[0].Height);
            e.Graphics.DrawString("Názov článku", fontrep, Brushes.Black, new RectangleF(13 + 50 + dataGridView1.Columns[0].Width, 100, dataGridView1.Columns[0].Width + 450, dataGridView1.Rows[0].Height), str);
            //Draw column 4
            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(510 + 50 + dataGridView1.Columns[0].Width, 100, dataGridView1.Columns[0].Width-20, dataGridView1.Rows[0].Height));
            e.Graphics.DrawRectangle(Pens.Black, 510 + 50 + dataGridView1.Columns[0].Width, 100, dataGridView1.Columns[0].Width-20, dataGridView1.Rows[0].Height);
            e.Graphics.DrawString("Jaz.", fontrep, Brushes.Black, new RectangleF(510 + 50 + dataGridView1.Columns[0].Width, 100, dataGridView1.Columns[0].Width-20, dataGridView1.Rows[0].Height), str);
            //Draw column 5
            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(538 + 50 + dataGridView1.Columns[0].Width, 100, dataGridView1.Columns[0].Width-10, dataGridView1.Rows[0].Height));
            e.Graphics.DrawRectangle(Pens.Black, 538 + 50 + dataGridView1.Columns[0].Width, 100, dataGridView1.Columns[0].Width-10, dataGridView1.Rows[0].Height);
            e.Graphics.DrawString("%", fontrep, Brushes.Black, new RectangleF(538 + 50 + dataGridView1.Columns[0].Width, 100, dataGridView1.Columns[0].Width-10, dataGridView1.Rows[0].Height), str);
            //Draw column 6
            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(576 + 50 + dataGridView1.Columns[0].Width, 100, dataGridView1.Columns[0].Width-10, dataGridView1.Rows[0].Height));
            e.Graphics.DrawRectangle(Pens.Black, 576 + 50 + dataGridView1.Columns[0].Width, 100, dataGridView1.Columns[0].Width-10, dataGridView1.Rows[0].Height);
            e.Graphics.DrawString("Body", fontrep, Brushes.Black, new RectangleF(576 + 50 + dataGridView1.Columns[0].Width, 100, dataGridView1.Columns[0].Width-10, dataGridView1.Rows[0].Height), str);
            //Draw column 7
            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(614 + 50 + dataGridView1.Columns[0].Width, 100, dataGridView1.Columns[0].Width-10, dataGridView1.Rows[0].Height));
            e.Graphics.DrawRectangle(Pens.Black, 614 + 50 + dataGridView1.Columns[0].Width, 100, dataGridView1.Columns[0].Width-10, dataGridView1.Rows[0].Height);
            e.Graphics.DrawString("Σ", fontrep, Brushes.Black, new RectangleF(614 + 50 + dataGridView1.Columns[0].Width, 100, dataGridView1.Columns[0].Width-10, dataGridView1.Rows[0].Height), str);
            //Draw column 8
            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(652 + 50 + dataGridView1.Columns[0].Width, 100, dataGridView1.Columns[0].Width-20, dataGridView1.Rows[0].Height));
            e.Graphics.DrawRectangle(Pens.Black, 652 + 50 + dataGridView1.Columns[0].Width, 100, dataGridView1.Columns[0].Width-20, dataGridView1.Rows[0].Height);
            e.Graphics.DrawString("Vyz", fontrep, Brushes.Black, new RectangleF(652 + 50 + dataGridView1.Columns[0].Width, 100, dataGridView1.Columns[0].Width-20, dataGridView1.Rows[0].Height), str);

            width = 100 + dataGridView1.Columns[0].Width;
            height = 100;
            //variable i is declared at class level to preserve the value of i if e.hasmorepages is true
            while (ip < dataGridView1.Rows.Count)
            {
                if (height > e.MarginBounds.Height)
                {
                    height = 100;
                    width = 100;
                    e.HasMorePages = true;
                    return;
                }

                height += dataGridView1.Rows[ip].Height;
                //col1
                e.Graphics.DrawRectangle(Pens.Black, 50, height, dataGridView1.Columns[0].Width-20, dataGridView1.Rows[0].Height);
                e.Graphics.DrawString(dataGridView1.Rows[ip].Cells[0].FormattedValue.ToString(), dataGridView1.Font, Brushes.Black, new RectangleF(50, height, dataGridView1.Columns[0].Width-20, dataGridView1.Rows[0].Height), str);
                //col2
                e.Graphics.DrawRectangle(Pens.Black, 30 + dataGridView1.Columns[0].Width, height, dataGridView1.Columns[0].Width-15, dataGridView1.Rows[0].Height);
                e.Graphics.DrawString(dataGridView1.Rows[ip].Cells[1].Value.ToString(), dataGridView1.Font, Brushes.Black, new RectangleF(31 + dataGridView1.Columns[0].Width, height, dataGridView1.Columns[0].Width-15, dataGridView1.Rows[0].Height), str);
                //col3
                e.Graphics.DrawRectangle(Pens.Black, 13 + 50 + dataGridView1.Columns[0].Width, height, dataGridView1.Columns[0].Width +449, dataGridView1.Rows[0].Height);
                e.Graphics.DrawString(dataGridView1.Rows[ip].Cells[2].Value.ToString(), fontrep2, Brushes.Black, new RectangleF(13 + 50 + dataGridView1.Columns[0].Width, height, dataGridView1.Columns[0].Width + 449, dataGridView1.Rows[0].Height), str);
                //col4
                e.Graphics.DrawRectangle(Pens.Black, 510 + 50 + dataGridView1.Columns[0].Width, height, dataGridView1.Columns[0].Width-20, dataGridView1.Rows[0].Height);
                e.Graphics.DrawString(dataGridView1.Rows[ip].Cells[3].Value.ToString(), dataGridView1.Font, Brushes.Black, new RectangleF(510 + 50 + dataGridView1.Columns[0].Width, height, dataGridView1.Columns[0].Width-20, dataGridView1.Rows[0].Height), str);
                //col5
                e.Graphics.DrawRectangle(Pens.Black, 538 + 50 + dataGridView1.Columns[0].Width, height, dataGridView1.Columns[0].Width-10, dataGridView1.Rows[0].Height);
                e.Graphics.DrawString(dataGridView1.Rows[ip].Cells[4].Value.ToString(), dataGridView1.Font, Brushes.Black, new RectangleF(538 + 50 + dataGridView1.Columns[0].Width, height, dataGridView1.Columns[0].Width-10, dataGridView1.Rows[0].Height), str);
                //col6
                e.Graphics.DrawRectangle(Pens.Black, 576 + 50 + dataGridView1.Columns[0].Width, height, dataGridView1.Columns[0].Width-10, dataGridView1.Rows[0].Height);
                e.Graphics.DrawString(dataGridView1.Rows[ip].Cells[5].Value.ToString(), dataGridView1.Font, Brushes.Black, new RectangleF(576 + 50 + dataGridView1.Columns[0].Width, height, dataGridView1.Columns[0].Width-10, dataGridView1.Rows[0].Height), str);
                //col7
                e.Graphics.DrawRectangle(Pens.Black, 614 + 50 + dataGridView1.Columns[0].Width, height, dataGridView1.Columns[0].Width-10, dataGridView1.Rows[0].Height);
                e.Graphics.DrawString(dataGridView1.Rows[ip].Cells[6].Value.ToString(), dataGridView1.Font, Brushes.Black, new RectangleF(614 + 50 + dataGridView1.Columns[0].Width, height, dataGridView1.Columns[0].Width-10, dataGridView1.Rows[0].Height), str);
                //col8
                e.Graphics.DrawRectangle(Pens.Black, 652 + 50 + dataGridView1.Columns[0].Width, height, dataGridView1.Columns[0].Width-20, dataGridView1.Rows[0].Height);
                string vyz5 = "";
                if ((Convert.ToBoolean(dataGridView1.Rows[ip].Cells[7].Value)).ToString() == "True")
                {
                    vyz5 = "X";
                }
                e.Graphics.DrawString(vyz5, dataGridView1.Font, Brushes.Black, new RectangleF(657 + 50 + dataGridView1.Columns[0].Width, height, dataGridView1.Columns[0].Width-20, dataGridView1.Rows[0].Height), str);

                width += dataGridView1.Columns[0].Width;
                ip++;
            }
            e.Graphics.DrawString("Meno: " + textBox11.Text + "    Priezvisko: " + textBox7.Text + "     Dátum .........       Podpis ........." , new Font(FontFamily.GenericSerif, 14, FontStyle.Bold), Brushes.Black, new RectangleF(50, 45, 900, 200));
            e.Graphics.DrawString("Súčet bodov za články: " + textBox1.Text + " bodov" + "                 Rok vykazovania: " + textBox8.Text, new Font(FontFamily.GenericSerif, 14, FontStyle.Bold), Brushes.Black, new RectangleF(50, 65, 900, 200));
            ip = 0;

            //e.Graphics.Save();
            //bm = new Bitmap(dataGridView1.ClientRectangle.Width, dataGridView1.ClientRectangle.Height);
            //dataGridView1.DrawToBitmap(bm, dataGridView1.ClientRectangle);
            //bm.Save(@"C:\\clanky.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            //e.Graphics.Clear(Color.White);
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) //citacie
        {
            int width = 0;
            int height = 0;
            int x = 0;
            int y = 0;
            int rowheight = 0;
            int columnwidth = 0;

            StringFormat str2 = new StringFormat();
            str2.Alignment = StringAlignment.Near;
            str2.LineAlignment = StringAlignment.Center;
            str2.Trimming = StringTrimming.EllipsisCharacter;
            Pen p = new Pen(Color.Black, 2.5f);
            Font fontrep = new Font(Font.SystemFontName, 7f);
            Font fontrep2 = new Font(Font.SystemFontName, 6.5f);
            //e.Graphics.DrawRectangle(p,dataGridView1.Bounds);
            //e.Graphics.Clear(Color.White);
            //printPreviewDialog1.Document=printDocument1;
            //e.Graphics.DrawString("Hi this a test print", new Font(Font.SystemFontName,10.5f), Brushes.Black, new PointF(250.0f, 250.0f));
            //e.Graphics.DrawImage((Image)bm,new Point(10,10));

            //Draw Column 1
            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(50, 100, dataGridView2.Columns[0].Width-20, dataGridView2.Rows[0].Height));
            e.Graphics.DrawRectangle(Pens.Black, 50, 100, dataGridView2.Columns[0].Width-20, dataGridView2.Rows[0].Height);
            e.Graphics.DrawString("P.č.", fontrep, Brushes.Black, new RectangleF(50, 100, dataGridView2.Columns[0].Width - 20, dataGridView2.Rows[0].Height), str2);
            
            //Draw column 2
            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(30 + dataGridView2.Columns[0].Width, 100, dataGridView2.Columns[0].Width-20, dataGridView2.Rows[0].Height));
            e.Graphics.DrawRectangle(Pens.Black, 30 + dataGridView2.Columns[0].Width, 100, dataGridView2.Columns[0].Width-20, dataGridView2.Rows[0].Height);
            e.Graphics.DrawString("Kat.", fontrep, Brushes.Black, new RectangleF(30 + dataGridView2.Columns[0].Width, 100, dataGridView2.Columns[0].Width - 20, dataGridView2.Rows[0].Height), str2);
            //Draw column 3
            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle( 58 + dataGridView2.Columns[0].Width, 100, dataGridView2.Columns[0].Width + 180, dataGridView2.Rows[0].Height));
            e.Graphics.DrawRectangle(Pens.Black,  58 + dataGridView2.Columns[0].Width, 100, dataGridView2.Columns[0].Width + 180, dataGridView2.Rows[0].Height);
            e.Graphics.DrawString("Názov citácie", fontrep, Brushes.Black, new RectangleF(58 + dataGridView2.Columns[0].Width, 100, dataGridView2.Columns[0].Width + 180, dataGridView2.Rows[0].Height), str2);
            //Draw column 4
            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(286 + dataGridView2.Columns[0].Width, 100, dataGridView2.Columns[0].Width+90, dataGridView2.Rows[0].Height));
            e.Graphics.DrawRectangle(Pens.Black, 286 + dataGridView2.Columns[0].Width, 100, dataGridView2.Columns[0].Width+90, dataGridView2.Rows[0].Height);
            e.Graphics.DrawString("Zdroj citácie", fontrep, Brushes.Black, new RectangleF(286 + dataGridView2.Columns[0].Width, 100, dataGridView2.Columns[0].Width + 90, dataGridView2.Rows[0].Height), str2);
            //Draw column 5
            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(424 + dataGridView2.Columns[0].Width, 100, dataGridView2.Columns[0].Width - 20, dataGridView2.Rows[0].Height));
            e.Graphics.DrawRectangle(Pens.Black, 424 + dataGridView2.Columns[0].Width, 100, dataGridView2.Columns[0].Width-20, dataGridView2.Rows[0].Height);
            e.Graphics.DrawString("Jaz.", fontrep, Brushes.Black, new RectangleF(424 + dataGridView2.Columns[0].Width, 100, dataGridView2.Columns[0].Width - 20, dataGridView2.Rows[0].Height), str2);
            //Draw column 6
            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(452 + dataGridView2.Columns[0].Width, 100, dataGridView2.Columns[0].Width + 90, dataGridView2.Rows[0].Height));
            e.Graphics.DrawRectangle(Pens.Black, 452 + dataGridView2.Columns[0].Width, 100, dataGridView2.Columns[0].Width+90, dataGridView2.Rows[0].Height);
            e.Graphics.DrawString("Citovaný článok", fontrep, Brushes.Black, new RectangleF(452 + dataGridView2.Columns[0].Width, 100, dataGridView2.Columns[0].Width + 90, dataGridView2.Rows[0].Height), str2);
            //Draw column 7
            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(590 + dataGridView2.Columns[0].Width, 100, dataGridView2.Columns[0].Width - 20, dataGridView2.Rows[0].Height));
            e.Graphics.DrawRectangle(Pens.Black, 590 + dataGridView2.Columns[0].Width, 100, dataGridView2.Columns[0].Width -20, dataGridView2.Rows[0].Height);
            e.Graphics.DrawString("Body", fontrep, Brushes.Black, new RectangleF(590 + dataGridView2.Columns[0].Width, 100, dataGridView2.Columns[0].Width - 20, dataGridView2.Rows[0].Height), str2);
            //Draw column 8
            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(618 + dataGridView2.Columns[0].Width, 100, dataGridView2.Columns[0].Width - 20, dataGridView2.Rows[0].Height));
            e.Graphics.DrawRectangle(Pens.Black, 618 + dataGridView2.Columns[0].Width, 100, dataGridView2.Columns[0].Width -20, dataGridView2.Rows[0].Height);
            e.Graphics.DrawString("%", fontrep, Brushes.Black, new RectangleF(618 + dataGridView2.Columns[0].Width, 100, dataGridView2.Columns[0].Width - 20, dataGridView2.Rows[0].Height), str2);
            //Draw column 9
            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(646 + dataGridView2.Columns[0].Width, 100, dataGridView2.Columns[0].Width - 10, dataGridView2.Rows[0].Height));
            e.Graphics.DrawRectangle(Pens.Black, 646 + dataGridView2.Columns[0].Width, 100, dataGridView2.Columns[0].Width - 10, dataGridView2.Rows[0].Height);
            e.Graphics.DrawString("Σ", fontrep, Brushes.Black, new RectangleF(646 + dataGridView2.Columns[0].Width, 100, dataGridView2.Columns[0].Width - 10, dataGridView2.Rows[0].Height), str2);
            //e.Graphics.Clear(Color.White);
            width = 100 + dataGridView2.Columns[0].Width;
            height = 100;       
            //variable i is declared at class level to preserve the value of i if e.hasmorepages is true
           
            while (ip2 < dataGridView2.Rows.Count)
            {
                 if (height > e.MarginBounds.Height)
                {
                    height = 100;
                    width = 100;
                    e.HasMorePages = true;
                    return;
                }

                height += dataGridView2.Rows[ip2].Height;
                //col1
                e.Graphics.DrawRectangle(Pens.Black, 50, height, dataGridView2.Columns[0].Width-20, dataGridView2.Rows[0].Height);
                e.Graphics.DrawString(dataGridView2.Rows[ip2].Cells[0].FormattedValue.ToString(), dataGridView2.Font, Brushes.Black, new RectangleF(50, height, dataGridView2.Columns[0].Width-20, dataGridView2.Rows[0].Height), str2);
                //col2
                e.Graphics.DrawRectangle(Pens.Black, 30 + dataGridView2.Columns[0].Width, height, dataGridView2.Columns[0].Width-20, dataGridView2.Rows[0].Height);
                e.Graphics.DrawString(dataGridView2.Rows[ip2].Cells[1].Value.ToString(), dataGridView2.Font, Brushes.Black, new RectangleF(30 + dataGridView2.Columns[0].Width, height, dataGridView2.Columns[0].Width-20, dataGridView2.Rows[0].Height), str2);
                //col3
                e.Graphics.DrawRectangle(Pens.Black, 58 + dataGridView2.Columns[0].Width, height, dataGridView2.Columns[0].Width + 180, dataGridView2.Rows[0].Height);
                e.Graphics.DrawString(dataGridView2.Rows[ip2].Cells[2].Value.ToString(), fontrep2, Brushes.Black, new RectangleF(58 + dataGridView2.Columns[0].Width, height, dataGridView2.Columns[0].Width + 180, dataGridView2.Rows[0].Height), str2); //dataGridView2.Rows[ip2].Cells[2].Value.ToString()
                //col4
                e.Graphics.DrawRectangle(Pens.Black, 286  + dataGridView2.Columns[0].Width, height, dataGridView2.Columns[0].Width + 90, dataGridView1.Rows[0].Height);
                e.Graphics.DrawString(dataGridView2.Rows[ip2].Cells[3].Value.ToString(), fontrep2, Brushes.Black, new RectangleF(286 + dataGridView2.Columns[0].Width, height, dataGridView2.Columns[0].Width + 90, dataGridView2.Rows[0].Height), str2);
                //col5
                e.Graphics.DrawRectangle(Pens.Black, 424 + dataGridView2.Columns[0].Width, height, dataGridView2.Columns[0].Width-20, dataGridView2.Rows[0].Height);
                e.Graphics.DrawString(dataGridView2.Rows[ip2].Cells[4].Value.ToString(), dataGridView2.Font, Brushes.Black, new RectangleF(424 + dataGridView2.Columns[0].Width, height, dataGridView2.Columns[0].Width-20, dataGridView2.Rows[0].Height), str2);
                //col6
                e.Graphics.DrawRectangle(Pens.Black, 452 + dataGridView2.Columns[0].Width, height, dataGridView2.Columns[0].Width+90, dataGridView2.Rows[0].Height);
                e.Graphics.DrawString(dataGridView2.Rows[ip2].Cells[5].Value.ToString(), fontrep2, Brushes.Black, new RectangleF(452 + dataGridView2.Columns[0].Width, height, dataGridView2.Columns[0].Width+90, dataGridView2.Rows[0].Height), str2);
                //col7
                e.Graphics.DrawRectangle(Pens.Black, 590 + dataGridView2.Columns[0].Width, height, dataGridView2.Columns[0].Width -20, dataGridView2.Rows[0].Height);
                e.Graphics.DrawString(dataGridView2.Rows[ip2].Cells[6].Value.ToString(), fontrep2, Brushes.Black, new RectangleF(590 + dataGridView2.Columns[0].Width, height, dataGridView2.Columns[0].Width -20, dataGridView2.Rows[0].Height), str2);
                //col8
                e.Graphics.DrawRectangle(Pens.Black, 618 + dataGridView2.Columns[0].Width, height, dataGridView2.Columns[0].Width -20, dataGridView2.Rows[0].Height);
                e.Graphics.DrawString(dataGridView2.Rows[ip2].Cells[7].Value.ToString(), fontrep2, Brushes.Black, new RectangleF(618 + dataGridView2.Columns[0].Width, height, dataGridView2.Columns[0].Width -20, dataGridView2.Rows[0].Height), str2);
                //col9
                e.Graphics.DrawRectangle(Pens.Black, 646 + dataGridView2.Columns[0].Width, height, dataGridView2.Columns[0].Width - 10, dataGridView2.Rows[0].Height);
                e.Graphics.DrawString(dataGridView2.Rows[ip2].Cells[8].Value.ToString(), fontrep2, Brushes.Black, new RectangleF(646 + dataGridView2.Columns[0].Width, height, dataGridView2.Columns[0].Width - 10, dataGridView2.Rows[0].Height), str2);

                width += dataGridView2.Columns[0].Width;
                ip2++;
            }
            
            e.Graphics.DrawString("Meno: " + textBox11.Text + "    Priezvisko: " + textBox7.Text + "     Dátum .........       Podpis .........", new Font(FontFamily.GenericSerif, 14, FontStyle.Bold), Brushes.Black, new RectangleF(50, 45, 900, 200));
            e.Graphics.DrawString("Súčet bodov za citácie : " + textBox12.Text + " bodov" +                   "      Rok vykazovania: " + textBox8.Text, new Font(FontFamily.GenericSerif, 14, FontStyle.Bold), Brushes.Black, new RectangleF(50, 65, 900, 200));
            ip2 = 0;
            //bm = new Bitmap(dataGridView2.ClientRectangle.Width, dataGridView2.ClientRectangle.Height);
            //dataGridView2.DrawToBitmap(bm, dataGridView2.ClientRectangle);
            //bm.Save(@"C:\\citacie.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            //bm = null;
            //bm = new Bitmap(500, 500);
            //button1.DrawToBitmap(bm, button1.ClientRectangle);
            //e.Graphics.Clear(Color.White);
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void button20_Click(object sender, EventArgs e)
        {
            webBrowser2.Navigate("http://maisportal.tuke.sk/portal/rozvrhy.mais");
            lvl1 = true;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            HtmlElement rcheck = webBrowser2.Document.All["search_pedagog"];
            rcheck.SetAttribute("value", "P");
            rcheck.InvokeMember("click");

            HtmlElement rcombo = webBrowser2.Document.All["zvolenaFakultaId"];
            rcombo.SetAttribute("value", "6873");
            rcombo.SetAttribute("selectedIndex", "5");
            rcombo.InvokeMember("click");

            HtmlElement rodosli = webBrowser2.Document.All["odosli"];
            rodosli.InvokeMember("click");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            
            HtmlElement rodosli3 = webBrowser2.Document.All["priezviskoZamestnanca"];
            //            if (textArea5 != null)
            //{
            rodosli3.InnerText = textBox7.Text;
            //}
            HtmlElement rodosli4 = webBrowser2.Document.All["btn_obnov_zamestnancov"];
            rodosli4.InvokeMember("click");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            HtmlElement rodosli2 = webBrowser2.Document.All["odosli"];
            HtmlElement rcombozam = webBrowser2.Document.All["zvolenyZamestnanec"];
            rcombozam.SetAttribute("selectedIndex", "1");
            //string xx = rcombozam.GetAttribute("value").Equals("Židek, Kamil");
            rcombozam.Focus();
            //rcombozam.
            //rcombozam.SetAttribute("selectedIndex", "2"); 

            //rcombozam.SetAttribute("value", "184107");
            //rcombozam.SetAttribute("zvolenyZamestnanecId", "Židek, Kamil");
            rodosli2.InvokeMember("click");
        }

        private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            nav2 = true;
            if (lvl1 == true)
            {
                //MessageBox.Show("ddf");
                HtmlElement rcheck = webBrowser2.Document.All["search_pedagog"];
                rcheck.SetAttribute("value", "P");
                rcheck.InvokeMember("click");

                HtmlElement rcombo = webBrowser2.Document.All["zvolenaFakultaId"];
                rcombo.SetAttribute("value", "6873");
                rcombo.SetAttribute("selectedIndex", "5");
                rcombo.InvokeMember("click");

                HtmlElement rodosli = webBrowser2.Document.All["odosli"];
                rodosli.InvokeMember("click");
                lvl1 = false;
                lvl2 = true;
            }
            if (lvl2 == true)
            {
                HtmlElement rodosli3 = webBrowser2.Document.All["priezviskoZamestnanca"];
                //            if (textArea5 != null)
                //{
                rodosli3.InnerText = textBox7.Text;
                //}
                HtmlElement rodosli4 = webBrowser2.Document.All["btn_obnov_zamestnancov"];
                rodosli4.InvokeMember("click");
                lvl2 = false;
                lvl3 = true;
            }
            if (lvl3 == true)
            {
                HtmlElement rodosli2 = webBrowser2.Document.All["odosli"];
                HtmlElement rcombozam = webBrowser2.Document.All["zvolenyZamestnanec"];
                rcombozam.SetAttribute("selectedIndex", "1");
                //string xx = rcombozam.GetAttribute("value").Equals("Židek, Kamil");
                rcombozam.Focus();
                //rcombozam.
                //rcombozam.SetAttribute("selectedIndex", "2"); 

                //rcombozam.SetAttribute("value", "184107");
                //rcombozam.SetAttribute("zvolenyZamestnanecId", "Židek, Kamil");
                rodosli2.InvokeMember("click");
                lvl3 = false;
            }
        }

        private void webBrowser2_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            //nav2 = false;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (repflag == true)
            {
                dataGridView1.Columns.Remove(chk.Name);
                richTextBox2.Clear();
                richTextBox1.Clear();
                lengh = 0;
                lengh2 = 0;
            }

            repflag = true;
            tabControl1.SelectTab(1);
            dataGridView1_Click(sender, e);
            //Nahraj clanky
            if (File.Exists(path + "\\clanky.txt") && File.Exists(path + "\\citacie.txt")) //c:\\
            {
                string fileName = path + "\\clanky.txt"; //c:\\
                FileStream fs = new FileStream(fileName, FileMode.Open);
                // Nadstavba citania na FileStream 
                StreamReader sr = new StreamReader(fs, Encoding.GetEncoding(1200));
                string temp = null;
                // Citanie po riadkoch
                while ((temp = sr.ReadLine()) != null)
                {
                    richTextBox1.AppendText(temp + Environment.NewLine);
                    txt[lengh] = temp;
                    lengh++;
                }
                sr.Close();
                button2_Click(sender, e);
                //obnov vyznamne citacie
                int totalLines = richTextBox1.Lines.Length;
                string lastLine = richTextBox1.Lines[totalLines - 2];
                string[] words = lastLine.Split(';');
                foreach (string word in words)
                {
                    if (word == " " || word == "")
                    {

                    }
                    else
                    {
                        dataGridView1[7, (int.Parse(word))].Value = true;
                        //dataGridView1.Refresh();
                    }
                }
                //nacitaj data pre citacie
                string fileName2 = path + "\\citacie.txt"; //c:\\
                FileStream fs2 = new FileStream(fileName2, FileMode.Open);
                // Nadstavba citania na FileStream 
                StreamReader sr2 = new StreamReader(fs2, Encoding.GetEncoding(1200));
                string temp2 = null;
                // Citanie po riadkoch
                while ((temp2 = sr2.ReadLine()) != null)
                {//buffer overflow
                    richTextBox2.AppendText(temp2 + Environment.NewLine);
                    txt2[lengh] = temp2;
                    lengh++;
                }
                sr2.Close();
                button5_Click(sender, e);
                button3_Click(sender, e);
                button17_Click(sender, e);
                button16_Click(sender, e);
                button10_Click(sender, e);
                tabControl1.SelectTab(1);
            }
            else
            {
                MessageBox.Show("Najprv ulozte data");
            }
            button18.Enabled = true;
            button19.Enabled = true;
        }

        private void button25_Click(object sender, EventArgs e)
        {//System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)
            //vyhladaj checkbox zaciarknuty zapis do suboru
            for (int i = 0; i < poc; i++)
            {
                bool c = Convert.ToBoolean(dataGridView1[7, i].Value);
                if (c == true)
                {
                    //MessageBox.Show(i.ToString());
                    richTextBox1.AppendText(i.ToString());
                    richTextBox1.AppendText(";");
                }
            }
            richTextBox1.SaveFile(path + "\\clanky.txt", RichTextBoxStreamType.UnicodePlainText); //C:\\
            richTextBox2.SaveFile(path + "\\citacie.txt", RichTextBoxStreamType.UnicodePlainText); //c:\\
            MessageBox.Show("Dáta boli uložené");
            DateTime dt = File.GetLastWriteTime(path + "\\clanky.txt"); //C:\\
            label39.Text = dt.ToString();
        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
            if (e.Value != null)
            {
                if (e.Value.ToString() == "10" && e.ColumnIndex.ToString() == "6")
                    e.CellStyle.BackColor = Color.YellowGreen;
                if (e.Value.ToString() == "5" && e.ColumnIndex.ToString() == "6")
                    e.CellStyle.BackColor = Color.Yellow;
            }  
        }

        private void button26_Click(object sender, EventArgs e)
        {
            if (File.Exists(path + "\\clanky.txt") && File.Exists(path + "\\citacie.txt")) //C:\\
            {
                string fileName = path + "\\clanky.txt"; //C:\\
                FileStream fs = new FileStream(fileName, FileMode.Open);
                // Nadstavba citania na FileStream 
                StreamReader sr = new StreamReader(fs, Encoding.GetEncoding(1200));
                string temp = null;
                // Citanie po riadkoch
                while ((temp = sr.ReadLine()) != null)
                {
                    richTextBox1.AppendText(temp + Environment.NewLine);
                    txtp[lengh] = temp;
                    lengh++;
                }
                sr.Close();
                button2_Click(sender, e);
                //obnov vyznamne citacie
                int totalLines = richTextBox1.Lines.Length;
                string lastLine = richTextBox1.Lines[totalLines - 2];
                string[] words = lastLine.Split(';');
                /*
                foreach (string word in words) //
                {
                    if (word == " " || word == "")
                    {

                    }
                    else
                    {
                        dataGridView1[7, (int.Parse(word))].Value = true;
                        //dataGridView1.Refresh();
                    }
                }
                 */
                for (int i = 0; i < lengh; i++)
                {
                    if (txt[i] != txtp[i])
                    {
                        MessageBox.Show("change clanky");
                    }
                }

                //nacitaj data pre citacie
                string fileName2 = path + "\\citacie.txt"; //C:\\
                FileStream fs2 = new FileStream(fileName2, FileMode.Open);
                // Nadstavba citania na FileStream 
                StreamReader sr2 = new StreamReader(fs2, Encoding.GetEncoding(1200));
                string temp2 = null;
                // Citanie po riadkoch
                while ((temp2 = sr2.ReadLine()) != null)
                {//buffer overflow
                    richTextBox2.AppendText(temp2 + Environment.NewLine);
                    txt2p[lengh] = temp2;
                    lengh++;
                }
                sr2.Close();
                //button5_Click(sender, e);
                //button3_Click(sender, e);
                //button17_Click(sender, e);
                //button16_Click(sender, e);
                //button10_Click(sender, e);
                tabControl1.SelectTab(1);
                //compare 
                for (int i = 0; i < lengh; i++)
                {
                    if (txt2[i] != txt2p[i])
                    {
                        MessageBox.Show("change citacie");
                    }
                }
                        
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            button6_Click(sender, e); //open webpage
            while (webBrowser1.ReadyState != WebBrowserReadyState.Complete) Application.DoEvents();
            button7_Click(sender, e); //login
            while (webBrowser1.ReadyState != WebBrowserReadyState.Complete && webBrowser1.IsBusy == false) Application.DoEvents();
            //Thread.Sleep(1000);
            button8_Click(sender, e);
            while (webBrowser1.ReadyState != WebBrowserReadyState.Complete && webBrowser1.IsBusy == false) Application.DoEvents();
            //Thread.Sleep(1000);
            button9_Click(sender, e);
            
        }
    }
}