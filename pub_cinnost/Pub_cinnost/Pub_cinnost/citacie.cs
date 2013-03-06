using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public class citacie
    {
        private int por;
        private string kat;
        private string nazov;
        private string citov;
        private string jaz;
        private string zdroj;
        private double body;
        private double per;
        private double fbody;

        public citacie()
        {
        }

        public citacie(int Por, string Kat, string Nazov, string Citov, string Jaz, string Zdroj, double Body, double Per, double FBody)
        {
            por = Por;
            kat = Kat;
            nazov = Nazov;
            citov = Citov;
            jaz = Jaz;
            zdroj = Zdroj;
            body = Body;
            per = Per;
            fbody = FBody;
        }

        public int Por
        {
            get { return por; }
            set { por = value; }
        }

        public string Kat
        {
            get { return kat; }
            set { kat = value; }
        }

        public string Nazov
        {
            get { return nazov; }
            set { nazov = value; }
        }

        public string Citov
        {
            get { return citov; }
            set { citov = value; }
        }

        public string Jaz
        {
            get { return jaz; }
            set { jaz = value; }
        }

        public string Zdroj
        {
            get { return zdroj; }
            set { zdroj = value; }
        }

        public double Body
        {
            get { return body; }
            set { body = value; }
        }

        public double Per
        {
            get { return per; }
            set { per = value; }
        }

        public double FBody
        {
            get { return fbody; }
            set { fbody = value; }
        }
    }
}
