using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public class publikacie
    {
        private int por;
        private string kat;
        private string nazov;
        private string jaz;
        private double per;
        private double fbody;
        private double body;

        public publikacie()
        {
        }

        public publikacie(int Por, string Kat, string Nazov, string Jaz, double Per, double Fbody, double Body)
        {
            por = Por;
            kat = Kat;
            nazov = Nazov;
            jaz = Jaz;
            per = Per;
            fbody = Fbody;
            body = Body;
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

        public string Jaz
        {
            get { return jaz; }
            set { jaz = value; }
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

        public double Body
        {
            get { return body; }
            set { body = value; }
        }
    }
}
