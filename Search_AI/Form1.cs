using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Dijkstra
{
    public partial class Form1 : Form
    {
        Graphics graph; //
        int x = 10; //how many squares
        int y = 10; //how many squares
        int xp = 0; //mouse select square x
        int yp = 0; //mouse select square y
        int i = 0;


        int h; int w;       //graphical array weight, height
        int xpar, ypar;     //Parent node
        int nodeval;        //nodeval
        int xa, ya;         //actual array value
        int xs, ys;         //start path point
        int xf, yf = -1;    //finish path point

        Point[] buffer;     //Buffer of Evaluted Point with values
        //public static int[,] buffer2;
        Point[] bufferpath; //Buffer of shortest Path
        int number;         //number inside loop
        int depth = 500;    //depth of search three
        int numberpath;      //numberpath increment point
        int value;          //Point search value
        int xas, yas = 0;       //actual search value
        
        int click = 0; //additional value for start, stop, obstacles

        public Form1()
        {
            InitializeComponent();
            h = pictureBox1.Size.Height;
            w = pictureBox1.Size.Width;
            graph = pictureBox1.CreateGraphics();
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            //Get size of array
            x = int.Parse(textBox1.Text);
            y = int.Parse(textBox2.Text);
            squares.nodes = new int[x, y]; //Create Array of Nodes

            //Draw Squares Arrays
            graph = pictureBox1.CreateGraphics();
            graph.Clear(Color.White);
            while (i < h / y) //Draw horizonal lines
            {
                graph.DrawLine(new Pen(Color.Blue), 0, yp, w, yp);
                yp = yp + h/y;
                i++;
            }
            i = 0;
            while (i < w / x) //Draw vertical lines
            {
                graph.DrawLine(new Pen(Color.Blue), xp, 0, xp, h);
                xp = xp + h / x;
                i++;
            }
            i = 0;
            xp = 0;
            yp = 0;

            //Fill array with -1 values
            for (int ja = 0; ja < y; ja++)     //fill y value
            {
                for (int ia = 0; ia < x; ia++) //fill x values
                {
                    squares.nodes[ia, ja] = -1;
                }
            }
        }

        public static class squares //class with nodes
        {
            public static int[,] nodes; //unvisited 0, obstacle 200, start -1
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //Get Mouse click
            int mx = e.X;
            int my = e.Y;
            int xd, yd = 0;
            xd = mx / (w / x);
            yd = my / (h / y);
            squares.nodes[xd, yd] = 0;
            //Set Start Position
            if (click == 0) 
            { 
                graph.FillRectangle(new SolidBrush(Color.Green), xd * (w / x), yd * (h / y), (w / x), (h / y)); 
                xs = xd; ys = yd; squares.nodes[xs, ys] = 0;
                graph.DrawString(nodeval.ToString(), new Font(new FontFamily("Arial"), w / x / 4), new SolidBrush(Color.White), xd * w / x + w / x / 3, yd * h / y + h / y / 3);
            }
            //Set End Position
            if (click == 1) 
            { 
                graph.FillRectangle(new SolidBrush(Color.Red), xd * (w / x), yd * (h / y), (w / x), (h / y));
                xf = xd; yf = yd; squares.nodes[xf, yf] = -1;
            }
            //Set Obstacles
            if (click == 2) 
            { 
                graph.FillRectangle(new SolidBrush(Color.Black), xd * (w / x), yd * (h / y), (w / x), (h / y));
                graph.DrawString("200",new Font(new FontFamily("Arial"),w/x/4),new SolidBrush(Color.White), xd * (w / x), yd * (h / y));
                click--;
                squares.nodes[xd, yd] = 200;
            }
            click++; //obstacles loop
        }

        void drawpotentialfield()
        {
            //right
            if (xpar + 1 >= 0 && ypar >= 0 && xpar + 1 < x && ypar < y) //Check if is not out of array
            {
                if (squares.nodes[xpar + 1, ypar] == -1)
                {
                    squares.nodes[xpar + 1, ypar] = squares.nodes[xpar, ypar] + 1; //right
                    nodeval = squares.nodes[xpar + 1, ypar]; //store value
                    buffer[number] = new Point(xpar + 1, ypar); //add to buffer
                    graph.DrawString(nodeval.ToString(), new Font(new FontFamily("Arial"), w / x / 4), new SolidBrush(Color.Black), (xpar + 1) * w / x + w / x / 3, ypar * h / y + h / y / 3);
                    number++;
                }
            }

            //down
            if (xpar >= 0 && ypar + 1 >= 0 && xpar < x && ypar + 1 < y)
            {
                if (squares.nodes[xpar, ypar + 1] == -1)
                {
                    squares.nodes[xpar, ypar + 1] = squares.nodes[xpar, ypar] + 1; //down
                    nodeval = squares.nodes[xpar, ypar + 1]; //store value
                    buffer[number] = new Point(xpar, ypar + 1); //add to buffer
                    graph.DrawString(nodeval.ToString(), new Font(new FontFamily("Arial"), w / x / 4), new SolidBrush(Color.Black), xpar * w / x + w / x / 3, (ypar + 1) * h / y + h / y / 3);
                    number++;
                }
            }
            
            //left
            if (xpar - 1 >= 0 && ypar >= 0 && xpar - 1 < x && ypar < y)
            {
                if (squares.nodes[xpar - 1, ypar] == -1)
                {
                    squares.nodes[xpar - 1, ypar] = squares.nodes[xpar, ypar] + 1; //left 
                    nodeval = squares.nodes[xpar - 1, ypar]; //store value
                    buffer[number] = new Point(xpar - 1, ypar); //add to buffer
                    graph.DrawString(nodeval.ToString(), new Font(new FontFamily("Arial"), w / x / 4), new SolidBrush(Color.Black), (xpar - 1) * w / x + w / x / 3, ypar * h / y + h / y / 3);
                    number++;
                }
            }

            //up
            if (xpar >= 0 && ypar - 1 >= 0 && xpar < x && ypar - 1 < y)
            {
                if (squares.nodes[xpar, ypar - 1] == -1)
                {
                    squares.nodes[xpar, ypar - 1] = squares.nodes[xpar, ypar] + 1; //up
                    nodeval = squares.nodes[xpar, ypar - 1]; //store value
                    buffer[number] = new Point(xpar, ypar - 1); //add to buffer
                    graph.DrawString(nodeval.ToString(), new Font(new FontFamily("Arial"), w / x / 4), new SolidBrush(Color.Black), xpar * w / x + w / x / 3, (ypar - 1) * h / y + h / y / 3);
                    number++;
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //Create Potential field, inital start Point to value 0
            squares.nodes[xs, ys] = 0;
            xpar = xs;
            ypar = ys;
            xa = xpar;
            ya = ypar;
            buffer = new Point[5000];
            number = 1;

            for (i = 0; i < number; i++)  //number????
            {
                drawpotentialfield();
                xpar = buffer[i+1].X; //insert x parent from array
                ypar = buffer[i+1].Y; //insert y parent from array 
                
                if (squares.nodes[xf, yf] != -1)
                {
                    break;
                }
            }
            
            //Search shortest path
            //create 
            bufferpath = new Point[5000];
            int numberpath = 0;
            Pen p = new Pen(Color.Orange, (h / y) / 8);
            p.EndCap = LineCap.ArrowAnchor;
            value = squares.nodes[xf, yf] - 1;
            int xpr = xf;
            int ypr = yf;
            xas = xpr;
            yas = ypr;

            while (true)
            {
                if (value == 0)
                {
                    break;
                }
                while (true)
                {
                    //Search UP

                    if (xpr >= 0 && ypr - 1 >= 0 && xpr < x && ypr - 1 < y)
                    {
                        if (squares.nodes[xpr, ypr - 1] == value) //check value in array and if exist in buffer
                        {
                            xas = xpr;//buffer[number - 2].X;
                            yas = ypr - 1; //buffer[number - 2].Y - 1;
                            //bufferpath[numberpath] = buffer[number - 1];//new Point(xas, yas);
                            graph.DrawRectangle(new Pen(Color.Orange, (h / y) / 10), (xas * (w / x)) + (h / y) / 10 / 2, (yas * (h / y)) + (h / y) / 10 / 2, (w / x) - (h / y) / 10, (h / y) - (h / y) / 10);
                            //graph.DrawLine(p, xas * (w / x) + (w / x) / 2, yas * (h / y) + (h / y), xas * (w / x) + (w / x) / 2, yas * (h / y) + (h / y) - (h / y) / 3);
                            value = value-1;
                            number = number-1;
                            numberpath = numberpath +1;
                            ypr = ypr - 1; //change for second loop
                            break;
                        }
                    }
                    //Search LEFT

                    if (xpr - 1 >= 0 && ypr >= 0 && xpr - 1 < x && ypr < y)
                    {
                        if (squares.nodes[xpr - 1, ypr] == value)
                        {
                            xas = xpr - 1;//buffer[number - 2].X - 1;
                            yas = ypr;//buffer[number - 2].Y;
                            //bufferpath[numberpath] = new Point(xas, yas);
                            graph.DrawRectangle(new Pen(Color.Orange, (h / y) / 10), (xas * (w / x)) + (h / y) / 10 / 2, (yas * (h / y)) + (h / y) / 10 / 2, (w / x) - (h / y) / 10, (h / y) - (h / y) / 10);
                            //graph.DrawLine(p, xas * (w / x) + (w / x) / 2, yas * (h / y) + (h / y), xas * (w / x) + (w / x) / 2, yas * (h / y) + (h / y) - (h / y) / 3);
                            value = value - 1;
                            number = number - 1;
                            numberpath = numberpath + 1;
                            xpr = xpr - 1;
                            break;
                        }
                    }
                    //Search DOWN
                    xas = xpr;//buffer[number - 2].X;
                    yas = ypr + 1; //buffer[number - 2].Y + 1;
                    if (xas >= 0 && yas >= 0 && xas < x && yas < y)
                    {
                        if (squares.nodes[xas, yas] == value)
                        {
                            //bufferpath[numberpath] = new Point(xas, yas);
                            graph.DrawRectangle(new Pen(Color.Orange, (h / y) / 10), (xas * (w / x)) + (h / y) / 10 / 2, (yas * (h / y)) + (h / y) / 10 / 2, (w / x) - (h / y) / 10, (h / y) - (h / y) / 10);
                            //graph.DrawLine(p, xas * (w / x) + (w / x) / 2, yas * (h / y) + (h / y), xas * (w / x) + (w / x) / 2, yas * (h / y) + (h / y) - (h / y) / 3);
                            value = value - 1;
                            number = number - 1;
                            numberpath = numberpath + 1;
                            ypr = ypr + 1;
                            break;
                        }
                    }
                    //Search RIGHT
                    xas = xpr + 1; //buffer[number - 2].X + 1;
                    yas = ypr;//buffer[number - 2].Y;
                    if (xas >= 0 && yas >= 0 && xas < x && yas < y)
                    {
                        if (squares.nodes[xas, yas] == value)
                        {
                            //bufferpath[numberpath] = new Point(xas, yas);
                            graph.DrawRectangle(new Pen(Color.Orange, (h / y) / 10), (xas * (w / x)) + (h / y) / 10 / 2, (yas * (h / y)) + (h / y) / 10 / 2, (w / x) - (h / y) / 10, (h / y) - (h / y) / 10);
                            //graph.DrawLine(p, xas * (w / x) + (w / x) / 2, yas * (h / y) + (h / y), xas * (w / x) + (w / x) / 2, yas * (h / y) + (h / y) - (h / y) / 3);
                            value = value - 1;
                            number = number - 1;
                            numberpath = numberpath + 1;
                            xpr = xpr + 1;
                            break;
                        }
                    }
                }
            }
        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            //graph.Clear(Color.White);
            pictureBox1.Visible = true;
        }

    }
}
