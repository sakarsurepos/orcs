// MJPEG Screensaver
//
// Copyright (C) 2007 Andrew Hamilton
// hamiltona@mchsi.com

//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
//
// The method to retrieve and parse the MJPEG stream
// is used from the excellent software:
// Camara Vision
//
// Copyright © Andrew Kirillov, 2005-2006
// andrew.kirillov@gmail.com
//

using System;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Robot
{
	/// <summary>
	/// Summary description for Options.
	/// </summary>
	public class Options
	{
		private int streams;
		private string text;
		private string bg_color;
        private string[] url;
        private int scale;
		public static string options_path = Environment.GetFolderPath(Environment.SpecialFolder.Personal)+"\\screen.xml";

		public Options()
		{
			this.streams = 0;
            this.scale = 0;
            this.text = "<URL #1>;<URL #2>;<URL #3>;...";
            this.bg_color = ColorTranslator.ToHtml(Color.Black);
            this.url = text.Split(';');
		}

		public void CreateOptionsFile()
		{
			XmlSerializer ser = new XmlSerializer( typeof(Options));
			
			Options o = new Options();
			StreamWriter wr = new StreamWriter( options_path );

			ser.Serialize(wr,(object)o);

			wr.Flush();
			wr.Close();
		}

		public Options ReadOptionsFromFile()
		{
			if ( !File.Exists( options_path ))
			{
				//File.Create( options_path );
                CreateOptionsFile();
			}

            XmlSerializer ser = new XmlSerializer(typeof(Options));
            XmlTextReader reader = new XmlTextReader(options_path);
            Options myo = (Options)ser.Deserialize(reader);
            reader.Close();

			return myo;
		}

		public int Streams
		{
			get { return this.streams; }
			set
            {
                if (value < 0 || value > 3)
                    this.streams = 0;
                else
                    this.streams = value;
            }
		}

		public string Text
		{
			get { return this.text; }
			set { this.text = value; }
		}
	
		public string BgColor
		{
			get { return this.bg_color; }
			set { this.bg_color = value; }
		}

        public string[] Url
        {
            get
            {
                String[] tmpstr1 = new string[4];
                int i = 0;
                int j = 1;
                foreach (string urlval in this.url)
                {
                    if (urlval != null && urlval != "")
                    {
                        tmpstr1[i] = urlval;
                    }
                    else
                    {
                        Array.Resize(ref tmpstr1, 4 - j++);
                    }
                    i++;
                }
                return tmpstr1;
                //return this.url;
            }
            set { this.url = value; }
        }

        public int Scale
        {
            get { return this.scale; }
            set { this.scale = value; }
        }
	}
}
