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
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Net;
using System.IO;
using System.Threading;

namespace Robot
{
    public class MjpegStream
    {
        private Bitmap bmp;
        public bool runThread = true;
        private String url;
        private String exceptionstring = null;

        public MjpegStream(String url)
        {
            this.url = url;
        }

        public void FetchStream()
        {
            while (runThread)
            {
                WebRequest req = null;
                WebResponse resp = null;
                Stream stream = null;
                const int bufSize = 800 * 600;
                byte[] buffer = new byte[bufSize]; 	// buffer to read stream
                const short readSize = 1024;
                byte[] delimiter = null, delimiter2 = null, boundary = null;
                int boundaryLen = 0, delimiterLen = 0, delimiter2Len = 0, read = 0, todo = 0, total = 0;
                int pos = 0, start = 0, stop = 0;
                int align = 1;

                try
                {
                    String ct;
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    if (url == null || url == " ")
                        throw new ArgumentNullException();

                    req = (HttpWebRequest)WebRequest.Create(new Uri(url));
                    req.Timeout = 10 * 1000; //Timeout in 10 seconds.
                    resp = (HttpWebResponse)req.GetResponse();

                    ct = resp.ContentType;
                    if (ct.IndexOf("multipart/x-mixed-replace") == -1)
                        throw new WebException("Stream not a proper MJPEG stream, sorry.");

                    // get boundary
                    boundary = encoding.GetBytes(ct.Substring(ct.IndexOf("boundary=", 0) + 9));
                    boundaryLen = boundary.Length;

                    stream = resp.GetResponseStream();

                    while (runThread)
                    {
                        if (total > bufSize - readSize)
                        {
                            total = pos = todo = 0;
                        }

                        // read next portion from stream
                        if ((read = stream.Read(buffer, total, readSize)) == 0)
                            throw new ApplicationException();

                        total += read;
                        todo += read;

                        // does we know the delimiter ?
                        if (delimiter == null)
                        {
                            // find boundary
                            pos = Find(buffer, boundary, pos, todo);

                            if (pos == -1)
                            {
                                // was not found
                                todo = boundaryLen - 1;
                                pos = total - todo;
                                continue;
                            }

                            todo = total - pos;

                            if (todo < 2)
                                continue;

                            // check new line delimiter type
                            if (buffer[pos + boundaryLen] == 10)
                            {
                                delimiterLen = 2;
                                delimiter = new byte[2] { 10, 10 };
                                delimiter2Len = 1;
                                delimiter2 = new byte[1] { 10 };
                            }
                            else
                            {
                                delimiterLen = 4;
                                delimiter = new byte[4] { 13, 10, 13, 10 };
                                delimiter2Len = 2;
                                delimiter2 = new byte[2] { 13, 10 };
                            }

                            pos += boundaryLen + delimiter2Len;
                            todo = total - pos;
                        }

                        // search for image
                        if (align == 1)
                        {
                            start = Find(buffer, delimiter, pos, todo);
                            if (start != -1)
                            {
                                // found delimiter
                                start += delimiterLen;
                                pos = start;
                                todo = total - pos;
                                align = 2;
                            }
                            else
                            {
                                // delimiter not found
                                todo = delimiterLen - 1;
                                pos = total - todo;
                            }
                        }

                        // search for image end
                        while ((align == 2) && (todo >= boundaryLen))
                        {
                            stop = Find(buffer, boundary, pos, todo);
                            if (stop != -1)
                            {
                                pos = stop;
                                todo = total - pos;

                                bmp = (Bitmap)Bitmap.FromStream(new MemoryStream(buffer, start, stop - start));
                                OnBmpEvt();

                                // shift array
                                pos = stop + boundaryLen;
                                todo = total - pos;
                                Array.Copy(buffer, pos, buffer, 0, todo);

                                total = todo;
                                pos = 0;
                                align = 1;
                            }
                            else
                            {
                                // delimiter not found
                                todo = boundaryLen - 1;
                                pos = total - todo;
                            }

                        }
                    }
                }
                catch (ArgumentNullException)
                {
                    exceptionstring = "Null Url Supplied!";
                    OnWebEx();
                    Thread.Sleep(2000);
                }
                catch (WebException ex)
                {
                    exceptionstring = ex.Message;
                    OnWebEx();
                    Thread.Sleep(2000);
                }
                catch (UriFormatException ex)
                {
                    exceptionstring = ex.Message;
                    OnWebEx();
                    Thread.Sleep(2000);
                }
                catch (Exception ex)
                {
                    if (ex.GetType() != typeof(ThreadAbortException))
                    {
                        exceptionstring = ex.Message;
                        OnWebEx();
                        Thread.Sleep(2000);
                    }
                }
                finally
                {
                    // abort request
                    if (req != null)
                    {
                        req.Abort();
                        req = null;
                    }
                    // close response stream
                    if (stream != null)
                    {
                        stream.Close();
                        stream = null;
                    }
                    // close response
                    if (resp != null)
                    {
                        resp.Close();
                        resp = null;
                    }
                }
            }
        }

        // Find subarray in array
        public static int Find(byte[] array, byte[] needle, int startIndex, int count)
        {
            int needleLen = needle.Length;
            int index;

            while (count >= needleLen)
            {
                index = Array.IndexOf(array, needle[0], startIndex, count - needleLen + 1);

                if (index == -1)
                    return -1;

                int i, p;
                // check for needle
                for (i = 0, p = index; i < needleLen; i++, p++)
                {
                    if (array[p] != needle[i])
                    {
                        break;
                    }
                }

                if (i == needleLen)
                {
                    // found needle
                    return index;
                }

                count -= (index - startIndex + 1);
                startIndex = index + 1;
            }
            return -1;
        }

        public delegate void BmpHandler();

        // Define an Event based on the above Delegate
        public event BmpHandler BmpEvt;

        // By Default, create an OnXXXX Method, to call the Event
        protected void OnBmpEvt()
        {
            if (BmpEvt != null)
            {
                BmpEvt();
            }
        }

        public delegate void WebExceptionHandler();

        // Define an Event based on the above Delegate
        public event WebExceptionHandler WebEx;

        // By Default, create an OnXXXX Method, to call the Event
        protected void OnWebEx()
        {
            if (WebEx != null)
            {
                WebEx();
            }
        }

        public Bitmap Bmp
        {
            get { return this.bmp; }
            set { this.bmp = value; }
        }

        public String WebExceptionString
        {
            get { return this.exceptionstring; }
        }
    }
}
