using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;

namespace Robot
{
    public class battery
    {
        public int mierka;
        public float cas = 0;
        public float aktualne_nap;
        public string value;

        Pen ciara = new Pen(Color.Red, 1);
        Pen osi = new Pen(Color.Black, 3);
        Pen osi2 = new Pen(Color.Black, 1);
        PointF ABFloat = new PointF(0, 0);
        PointF ABFloat2 = new PointF(0, 0);
        PointF ABFloat3 = new PointF(0, 0);
        PointF[] ABFloat4 = new PointF[18000];
        float[] ulozene_napatia = new float[18000];
        public Thread Time_Thread2;

        public void run_tr(Graphics graf)
        {
            Time_Thread2 = new Thread(new ParameterizedThreadStart(vlakno_graf3));
            Time_Thread2.Start(graf);
        }
      
        public void vlakno_graf3(object grafobj)
        {
            Graphics graf = (Graphics)grafobj;
            while(true)
            {
                //create new file after xx values
                if (cas == 25000)
                {
                    string tx = DateTime.Now.ToShortDateString();
                    string hour = DateTime.Now.Hour.ToString();
                    string min = DateTime.Now.Minute.ToString();
                    string sec = DateTime.Now.Second.ToString();
                    File.Move("input.txt", "log" + hour + min + sec + tx + ".txt");
                    cas = 0;
                    ABFloat.X = 0;
                    ABFloat.Y = 0;
                    graf.Clear(Color.White);
                    Pen osi = new Pen(Color.Black, 3);
                    osi.EndCap = LineCap.ArrowAnchor;
                    graf.DrawLine(osi, 0, 0, 250, 0); //horizontal
                    graf.DrawLine(osi, 0, 0, 0, 170); //vetical
                    Font f = new Font(new FontFamily("Arial"), 10, FontStyle.Regular);
                    StringFormat sf = new StringFormat();
                    GraphicsPath gp = new GraphicsPath();
                    gp.AddString("Napatie", new FontFamily("Times New Roman"), (int)FontStyle.Bold, 10,
                    new Point(10, -170), null); //napisy v pictureboxe
                    Matrix m = new Matrix(1, 0, 0, -1, 0, 0);
                    gp.Transform(m);
                    graf.DrawPath(Pens.Black, gp);
                }
                //create new file after xx values
                Thread.Sleep(1000); //sleep thread to 1s
                cas = cas + 1;
                ABFloat2.X = (cas / mierka);
                ABFloat2.Y = aktualne_nap * 11;
                FileStream fs;
                try
                {
                    fs = new FileStream("input.txt", FileMode.Append);  //zapis do suboru
                }
                catch
                {
                    fs = new FileStream("input.txt", FileMode.CreateNew);
                }
                TextWriter write = new StreamWriter(fs);
                if (cas != 0 && aktualne_nap != 0)
                {
                    write.WriteLine((Math.Round(Convert.ToDouble((cas)), 0).ToString()) + "_" + (aktualne_nap).ToString());
                    write.Close();
                }
                Matrix mat1 = new Matrix(1, 0, 0, -1, 0, 0); //Flip Y axis
                mat1.Translate(20, 180, MatrixOrder.Append); //Add translation
                graf.Transform = mat1;
                if (ABFloat.X < 240)
                {
                  graf.DrawLine(ciara, ABFloat, ABFloat2);
                }
                ABFloat = ABFloat2; //Presun bod
                Updater();
              

            }
        }

        public void Updater()
        {
            try
            {
                ((Robot1)Robot1.ActiveForm).change_components_text(aktualne_nap.ToString(), cas.ToString()); //textBox1, textBox2          
            }
            catch
            {
            }
        }

        public string rfiles(Graphics graf, int mierka2)
        {
            mierka = mierka2;
            int t = 0;
            string sLine;
            string[] combo = new string[2];
            string fileName = "input.txt";  //vypis hodnot zo suboru
            FileStream fs2;
            try
            {
                fs2 = new FileStream("input.txt", FileMode.CreateNew);
                fs2.Close();
            }
            catch { }
            StreamReader fileReader;
            fileReader = new StreamReader(fileName);
            sLine = fileReader.ReadToEnd();
            string[] comb2 = sLine.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            fileReader.Close(); 
            int lineCount = File.ReadAllLines("input.txt").Length;
            graf.Clear(Color.White);
            ABFloat3 = ABFloat4[t];
            Matrix mat1 = new Matrix(1, 0, 0, -1, 0, 0); //Flip Y axis
            mat1.Translate(20, 180, MatrixOrder.Append); //Add translation
            graf.Transform = mat1;
            Pen osi = new Pen(Color.Black, 3);
            osi.EndCap = LineCap.ArrowAnchor;
            graf.DrawLine(osi, 0, 0, 250, 0); //horizontal
            graf.DrawLine(osi, 0, 0, 0, 170); //vetical
            Font f = new Font(new FontFamily("Arial Narrow"), 10, FontStyle.Regular);
            StringFormat sf = new StringFormat();
            GraphicsPath gp = new GraphicsPath();
            gp.AddString("Napatie [V]", new FontFamily("Arial Narrow"), (int)FontStyle.Regular, 10, new Point(10, -170), null); //napisy v pictureboxe
            int cy = -16;
            Point bod0 = new Point(-18, cy);
            int z = 1;
            while (z < 15) //Os Y Labels
            {
                gp.AddString(z.ToString(), new FontFamily("Arial Narrow"), (int)FontStyle.Regular, 10, bod0, null);
                cy = cy - 11;
                bod0.Y = cy;
                z = z + 1;
            }

            value = "s";
            int r2 = 19;
            int incr_seg = 12;
            int popisx = 2;
            switch (mierka)
            {
                case 1:
                    value = "s";
                    r2 = 11;
                    incr_seg = 20;
                    popisx = 20;
                    break;
                case 2:
                    value = "s";
                    r2 = 11;
                    incr_seg = 20;
                    popisx = 40;
                    break;
                case 5:
                    value = "m";
                    r2 = 10;
                    incr_seg = 24;
                    popisx = 2;
                    break;
                case 10:
                    value = "m";
                    r2 = 10;
                    incr_seg = 24;
                    popisx = 4;
                    break;
                case 20:
                    value = "m";
                    r2 = 15;
                    incr_seg = 15;
                    popisx = 5;
                    break;
                case 50:
                    value = "h";
                    r2 = 3;
                    incr_seg = 72;
                    popisx = 1;
                    break;
                case 100:
                    value = "h";
                    r2 = 6;
                    incr_seg = 36;
                    popisx = 1;
                    break;
            }

            gp.AddString("Cas [" + value + "]", new FontFamily("Arial Narrow"), (int)FontStyle.Regular, 10, new Point(245, 7), null); //napisy v pictureboxe

            //Os X Labels podla mierky

            PointF ABFloat7 = new PointF(0, -4);
            PointF ABFloat8 = new PointF(0, 4);
            Point bod1 = new Point(0, 5);

            int inc = 0;
            int p2 = 0;
            int z2 = 0;
            while (inc < r2)
            {              
                p2 = p2 + incr_seg; //pripocita hodnotu rozstupu dielika na x
                z2 = z2 + popisx;//pripocita hodnotu popisky na xovej osi
                bod1.X = p2 - 8;
                ABFloat7.X = p2;
                ABFloat8.X = p2;
                gp.AddString(z2.ToString(), new FontFamily("Arial Narrow"), (int)FontStyle.Regular, 10, bod1, null);
                graf.DrawLine(osi2, ABFloat7, ABFloat8);

                inc = inc + 1;
            }

            Matrix m = new Matrix(1, 0, 0, -1, 0, 0);
            gp.Transform(m);
            graf.DrawPath(Pens.Black, gp);

            int p = 11;
            int r = 0;                                  //stupnice na osiach
            PointF ABFloat5 = new PointF(-4, p);
            PointF ABFloat6 = new PointF(4, p);
            while (r < 14)
            {
                graf.DrawLine(osi2, ABFloat5, ABFloat6);
                p = p+11;
                ABFloat5.Y = p;
                ABFloat6.Y = p;
                r = r+1;
            }

            while (t < lineCount)
            {
                combo = comb2[t].Split('_');
                ABFloat4[t] = new PointF((float.Parse(combo[0])) / mierka2, 11 * (float.Parse(combo[1])));
                if (ABFloat.X < 240)
                {
                    graf.DrawLine(ciara, ABFloat3, ABFloat4[t]);
                }
                ABFloat3 = ABFloat4[t];
                t = t + 1;
                ABFloat = ABFloat3;
            }
            cas = t;
            
        return sLine;
        }
    }
}
