using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Robot
{
    class Kinematics
    {
        //initial variables for kinematics
        public int length = 20;
        public double x1;  //start position
        public double y1;  //start position
        public double x2;  //start angle
        public double y2;  //start angle
        public double x3;  //final forward, backward position /rotate begin angle
        public double y3;  //final forward, backward position /rotate begin angle
        public double x4;  //middle of turning radius
        public double y4;  //middle of turning radius
        public double gama; //angle of ?
        public double x5;  //final position after turning
        public double y5;  //final position after turning
        public double x6;  //end angle of turning
        public double y6;  //end angle of rurning
        public double x7;  //turning final direction
        public double y7;  //turning final direction

        public Pen p3; //gray - history of position
        public Pen p2; //green - goal
        public Pen p1; //red - actual position
        public Pen p;  //aqua - axis

        string Rx;
        string Angle;
        int fg = 0;

        public Kinematics()
        {
        }

        public void IncializePic(Graphics g)
        {
            p3 = new Pen(Brushes.Gray, 1);
            p2 = new Pen(Brushes.Green, 1);
            p1 = new Pen(Brushes.Red, 1);
            p = new Pen(Brushes.Aqua, 1);
            //Inicialize Axis Transform
            Matrix mat1 = new Matrix();
            mat1 = new Matrix(1, 0, 0, -1, 0, 0); //Flip Y axis
            mat1.Translate(30, 420, MatrixOrder.Append); //Add translation
            g.Transform = mat1;
            //prepend the 90 deg clockwise rotation
            //m.Rotate(90,MatrixOrder.Prepend);
            g.Clear(Color.Silver);

            //Draw the axes
            g.DrawLine(p, -20, 0, 400, 0); //horizontal axes
            g.DrawLine(p, 0, -20, 0, 400); //vetical axes
        }

        public void forward(Graphics g, string textBoxrstartx1, string textBoxrstarty1, string textBoxrtravel, string textBoxrangle, string textBoxrendx, string textBoxrendy)
        {
            IncializePic(g);
            
            //Draw Robot Angle
            x1 = double.Parse(textBoxrstartx1);
            y1 = double.Parse(textBoxrstarty1);
            x2 = (double)((Convert.ToDouble(textBoxrtravel)) * Math.Cos((Math.PI / 180) * (Convert.ToDouble(textBoxrangle))));
            y2 = (double)((Convert.ToDouble(textBoxrtravel)) * Math.Sin((Math.PI / 180) * (Convert.ToDouble(textBoxrangle))));
            x3 = (double)(length * Math.Cos((Math.PI / 180) * (Convert.ToDouble(textBoxrangle))));
            y3 = (double)(length * Math.Sin((Math.PI / 180) * (Convert.ToDouble(textBoxrangle))));

            //Old Position
            g.DrawLine(p3, (float)x1, (float)y1, (float)(x1 + x3), (float)(y1 + y3));
            //Old Direction
            g.DrawEllipse(p3, (float)x1 - 5, (float)y1 - 5, 10, 10);

            //Draw Direction
            g.DrawLine(p1, (float)(x1 + x2), (float)(y1 + y2), (float)(x1 + x2 + x3), (float)(y1 + y2 + y3));
            //Draw Robot Position /Start Point Incremented
            g.DrawEllipse(p1, (float)(x1 + x2) - 5, (float)(y1 + y2) - 5, 10, 10);

            //Draw End Point
            g.DrawEllipse(p2, (float)double.Parse(textBoxrendx) - 5, (float)double.Parse(textBoxrendy) - 5, 10, 10);
        }

        public void backward(Graphics g, string textBoxrstartx1, string textBoxrstarty1, string textBoxrtravel, string textBoxrangle, string textBoxrendx, string textBoxrendy)
        {
            IncializePic(g);
            
            //Draw Robot Angle
            x1 = double.Parse(textBoxrstartx1);
            y1 = double.Parse(textBoxrstarty1);
            x2 = (double)((Convert.ToDouble(textBoxrtravel)) * Math.Cos((Math.PI / 180) * (Convert.ToDouble(textBoxrangle))));
            y2 = (double)((Convert.ToDouble(textBoxrtravel)) * Math.Sin((Math.PI / 180) * (Convert.ToDouble(textBoxrangle))));
            x3 = (double)(length * Math.Cos((Math.PI / 180) * (Convert.ToDouble(textBoxrangle))));
            y3 = (double)(length * Math.Sin((Math.PI / 180) * (Convert.ToDouble(textBoxrangle))));

            //Old Position
            g.DrawLine(p3, (float)x1, (float)y1, (float)(x1 + x3), (float)(y1 + y3));
            //Old Direction
            g.DrawEllipse(p3, (float)x1 - 5, (float)y1 - 5, 10, 10);

            //Draw Direction
            g.DrawLine(p1, (float)(x1 - x2), (float)(y1 - y2), (float)(x1 - x2 + x3), (float)(y1 - y2 + y3));
            //Draw Robot Position /Start Point Incremented
            g.DrawEllipse(p1, (float)(x1 - x2) - 5, (float)(y1 - y2) - 5, 10, 10);
            
            //Draw End Point
            g.DrawEllipse(p2, (float)double.Parse(textBoxrendx) - 5, (float)double.Parse(textBoxrendy) - 5, 10, 10);
        }

        public void left(Graphics g, string textBoxrstartx1, string textBoxrstarty1, string textBoxrtravel, string textBoxrangle, string textBoxrendx, string textBoxrendy, string textBoxrlenght, string textBoxrmaxsteer, string textBoxrangletravel, out string Rx, out string Angle)
        {
            //left
            if (double.Parse(textBoxrangle) <= -235) //180
            {
                textBoxrangle = (360 + double.Parse(textBoxrangle)).ToString();
                //buttonstart_Click("text", e);
            }

            if (double.Parse(textBoxrangle) >= 235) //180
            {
                textBoxrangle = (-360 + double.Parse(textBoxrangle)).ToString();
                //buttonstart_Click("text", e);
            }
            IncializePic(g);
            
            //Radius of turning
            double R = (double.Parse(textBoxrlenght)) / ((Math.Sin((Math.PI / 180) * (Convert.ToDouble(textBoxrmaxsteer)))));
            Rx = R.ToString();

            //Draw Robot Angle
            x1 = double.Parse(textBoxrstartx1);
            y1 = double.Parse(textBoxrstarty1);
            x2 = (double)(length * Math.Cos((Math.PI / 180) * (Convert.ToDouble(textBoxrangle))));
            y2 = (double)(length * Math.Sin((Math.PI / 180) * (Convert.ToDouble(textBoxrangle))));
            g.DrawLine(p3, (float)x1, (float)y1, (float)(x1 + x2), (float)(y1 + y2));

            //Draw Robot Position /Start Point
            g.DrawEllipse(p3, (float)x1 - 5, (float)y1 - 5, 10, 10);

            //Angle of turning
            x3 = (float)(length * Math.Cos((Math.PI / 180) * (Convert.ToDouble(textBoxrangle) + Convert.ToDouble(textBoxrmaxsteer)))); // + -
            y3 = (float)(length * Math.Sin((Math.PI / 180) * (Convert.ToDouble(textBoxrangle) + Convert.ToDouble(textBoxrmaxsteer))));
            g.DrawLine(p3, (float)x1, (float)y1, (float)(x1 + x3), (float)(y1 + y3));

            //Midle of Turning radius
            x4 = (float)(R * Math.Sin((Math.PI / 180) * (Convert.ToDouble(textBoxrangle) + Convert.ToDouble(textBoxrmaxsteer)))); // + -
            y4 = (float)(R * Math.Cos((Math.PI / 180) * (Convert.ToDouble(textBoxrangle) + Convert.ToDouble(textBoxrmaxsteer)))); // + -

            //Line to Turning Radius
            g.DrawLine(p3, (float)x1, (float)y1, (float)(x1 - x4), (float)(y1 + y4)); // +x4 -y4

            //Draw Circle of Turning
            g.DrawEllipse(p3, (float)(x1 - x4) - (float)R, (float)(y1 + y4) - (float)R, 2 * (float)R, 2 * (float)R); // +x4 -y4
            gama = (Math.Asin((float)(x4 / R))) * (180 / Math.PI); //Acos

            if ((int.Parse(textBoxrangle)) >= (90 - int.Parse(textBoxrmaxsteer)) || (double.Parse(textBoxrangle)) < -125)
            {
                //Draw End Point of Turning
                x5 = (float)(R * Math.Cos((Math.PI / 180) * (90 - ((-Convert.ToDouble(textBoxrangletravel)) + gama))));//add 90 -
                y5 = (float)(R * Math.Sin((Math.PI / 180) * (90 - ((-Convert.ToDouble(textBoxrangletravel)) + gama)))); //add 90 -
            }
            else
            {
                //Draw End Point of Turning
                x5 = (float)(R * Math.Cos((Math.PI / 180) * (90 - (180 - (Convert.ToDouble(textBoxrangletravel)) - gama))));//add 90 -
                y5 = (float)(R * Math.Sin((Math.PI / 180) * (90 - (180 - (Convert.ToDouble(textBoxrangletravel)) - gama)))); //add 90 -
            }
            g.DrawLine(p3, (float)(x1 - x4), (float)(y1 + y4), (float)(x1 - x4 + x5), (float)(y1 + y4 + y5)); //+x4 -y4
            //Final position
            g.DrawEllipse(p1, (float)(x1 - x4 + x5) - 5, (float)(y1 + y4 + y5) - 5, 10, 10); //+x4 -y4
            x6 = (float)(length * Math.Cos((Math.PI / 180) * (Convert.ToDouble(textBoxrmaxsteer) + Convert.ToDouble(textBoxrangle) + Convert.ToDouble(textBoxrangletravel))));
            y6 = (float)(length * Math.Sin((Math.PI / 180) * (Convert.ToDouble(textBoxrmaxsteer) + Convert.ToDouble(textBoxrangle) + Convert.ToDouble(textBoxrangletravel))));
            g.DrawLine(p3, (float)(x1 - x4 + x5), (float)(y1 + y4 + y5), (float)(x1 - x4 + x5 + x6), (float)(y1 + y4 + y5 + y6)); //+x4 -y4

            //Angle of turning
            x7 = (float)(length * Math.Cos((Math.PI / 180) * (Convert.ToDouble(textBoxrmaxsteer) + Convert.ToDouble(textBoxrangle) + Convert.ToDouble(textBoxrangletravel) - Convert.ToDouble(textBoxrmaxsteer)))); //- + -
            y7 = (float)(length * Math.Sin((Math.PI / 180) * (Convert.ToDouble(textBoxrmaxsteer) + Convert.ToDouble(textBoxrangle) + Convert.ToDouble(textBoxrangletravel) - Convert.ToDouble(textBoxrmaxsteer)))); //- + -
            g.DrawLine(p1, (float)(x1 - x4 + x5), (float)(y1 + y4 + y5), (float)(x1 - x4 + x5 + x7), (float)(y1 + y4 + y5 + y7)); //+x4 -y4

            //Draw End Point
            g.DrawEllipse(p2, float.Parse(textBoxrendx) - 5, float.Parse(textBoxrendy) - 5, 10, 10);

            //Transform Angle
            textBoxrangle = ((Convert.ToDouble(textBoxrangle) + Convert.ToDouble(textBoxrangletravel))).ToString();
            Angle = textBoxrangle;
        }

        public void right(Graphics g, string textBoxrstartx1, string textBoxrstarty1, string textBoxrtravel, string textBoxrangle, string textBoxrendx, string textBoxrendy, string textBoxrlenght, string textBoxrmaxsteer, string textBoxrangletravel, out string Rx, out string Angle)
        {
            //right
            if (double.Parse(textBoxrangle) >= 235) //235
            {
                textBoxrangle = (-360 + double.Parse(textBoxrangle)).ToString();
                //buttonstart_Click("text", e);
            }

            if (double.Parse(textBoxrangle) <= -235) //235
            {
                textBoxrangle = (360 + double.Parse(textBoxrangle)).ToString();
                //buttonstart_Click("text", e);
            }
            IncializePic(g);

            //Radius of turning
            double R = (double.Parse(textBoxrlenght)) / ((Math.Sin((Math.PI / 180) * (Convert.ToDouble(textBoxrmaxsteer)))));
            Rx = R.ToString();

            //Draw Robot Angle
            x1 = double.Parse(textBoxrstartx1);
            y1 = double.Parse(textBoxrstarty1);
            x2 = (double)(length * Math.Cos((Math.PI / 180) * (Convert.ToDouble(textBoxrangle))));
            y2 = (double)(length * Math.Sin((Math.PI / 180) * (Convert.ToDouble(textBoxrangle))));
            g.DrawLine(p3, (float)x1, (float)y1, (float)(x1 + x2), (float)(y1 + y2));

            //Draw Robot Position /Start Point
            g.DrawEllipse(p3, (float)x1 - 5, (float)y1 - 5, 10, 10);

            //Angle of turning
            x3 = (double)(length * Math.Cos((Math.PI / 180) * (Convert.ToDouble(textBoxrangle) - Convert.ToDouble(textBoxrmaxsteer))));
            y3 = (double)(length * Math.Sin((Math.PI / 180) * (Convert.ToDouble(textBoxrangle) - Convert.ToDouble(textBoxrmaxsteer))));
            g.DrawLine(p3, (float)x1, (float)y1, (float)(x1 + x3), (float)(y1 + y3));

            //Midle of Turning radius
            x4 = (double)(R * Math.Sin((Math.PI / 180) * (Convert.ToDouble(textBoxrangle) - Convert.ToDouble(textBoxrmaxsteer))));
            y4 = (double)(R * Math.Cos((Math.PI / 180) * (Convert.ToDouble(textBoxrangle) - Convert.ToDouble(textBoxrmaxsteer))));

            //Line to Turning Radius
            g.DrawLine(p3, (float)x1, (float)y1, (float)(x1 + x4), (float)(y1 - y4));

            //Draw Circle of Turning
            g.DrawEllipse(p3, (float)(x1 + x4) - (float)R, (float)(y1 - y4) - (float)R, 2 * (float)R, 2 * (float)R);
            /////textBoxrealangle.Text = ((Math.Acos((float)(x4 / R))) * (180 / Math.PI) - double.Parse(textBoxrangletravel.Text)).ToString(); //real rot. position
            gama = (Math.Acos((double)(x4 / R))) * (180 / Math.PI);

            if ((double.Parse(textBoxrangle)) <= (-90 + double.Parse(textBoxrmaxsteer)) || (double.Parse(textBoxrangle)) > 125)
            {
                //Draw End Point of Turning
                x5 = (double)(R * Math.Cos((Math.PI / 180) * ((180 - (Convert.ToDouble(textBoxrangletravel)) + gama))));
                y5 = (double)(R * Math.Sin((Math.PI / 180) * ((180 - (Convert.ToDouble(textBoxrangletravel)) + gama))));
            }
            else
            {
                //Draw End Point of Turning
                x5 = (double)(R * Math.Cos((Math.PI / 180) * (180 - (Convert.ToDouble(textBoxrangletravel)) - gama)));
                y5 = (double)(R * Math.Sin((Math.PI / 180) * (180 - (Convert.ToDouble(textBoxrangletravel)) - gama)));
            }
            g.DrawLine(p3, (float)(x1 + x4), (float)(y1 - y4), (float)(x1 + x4 + x5), (float)(y1 - y4 + y5));
            //Final position
            g.DrawEllipse(p1, (float)(x1 + x4 + x5) - 5, (float)(y1 - y4 + y5) - 5, 10, 10);
            x6 = (double)(length * Math.Cos((Math.PI / 180) * (Convert.ToDouble(textBoxrmaxsteer) - Convert.ToDouble(textBoxrangle) + Convert.ToDouble(textBoxrangletravel))));
            y6 = (double)(length * Math.Sin((Math.PI / 180) * (Convert.ToDouble(textBoxrmaxsteer) - Convert.ToDouble(textBoxrangle) + Convert.ToDouble(textBoxrangletravel))));
            g.DrawLine(p3, (float)(x1 + x4 + x5), (float)(y1 - y4 + y5), (float)(x1 + x4 + x5 + x6), (float)(y1 - y4 + y5 - y6));

            //Angle of turning
            x7 = (double)(length * Math.Cos((Math.PI / 180) * (Convert.ToDouble(textBoxrmaxsteer) - Convert.ToDouble(textBoxrangle) + Convert.ToDouble(textBoxrangletravel) - Convert.ToDouble(textBoxrmaxsteer))));
            y7 = (double)(length * Math.Sin((Math.PI / 180) * (Convert.ToDouble(textBoxrmaxsteer) - Convert.ToDouble(textBoxrangle) + Convert.ToDouble(textBoxrangletravel) - Convert.ToDouble(textBoxrmaxsteer))));
            g.DrawLine(p1, (float)(x1 + x4 + x5), (float)(y1 - y4 + y5), (float)(x1 + x4 + x5 + x7), (float)(y1 - y4 + y5 - y7));

            //Draw End Point
            g.DrawEllipse(p2, (float)double.Parse(textBoxrendx) - 5, (float)double.Parse(textBoxrendy) - 5, 10, 10);

            //Transform Angle
            textBoxrangle = ((Convert.ToDouble(textBoxrangle) - Convert.ToDouble(textBoxrangletravel))).ToString();
            Angle = textBoxrangle;
        }

        public void start(Graphics g, string textBoxrstartx1, string textBoxrstarty1, string textBoxrtravel, string textBoxrangle, string textBoxrendx, string textBoxrendy)
        {
            //start
            IncializePic(g);
            //Draw Robot Angle
            x1 = double.Parse(textBoxrstartx1);
            y1 = double.Parse(textBoxrstarty1);
            x2 = (double)(length * Math.Cos((Math.PI / 180) * (Convert.ToDouble(textBoxrangle))));
            y2 = (double)(length * Math.Sin((Math.PI / 180) * (Convert.ToDouble(textBoxrangle))));
            g.DrawLine(p1, (float)x1, (float)y1, (float)(x1 + x2), (float)(y1 + y2));
            //Draw Robot Position /Start Point
            g.DrawEllipse(p1, (float)x1 - 5, (float)y1 - 5, 10, 10);
            //Draw End Point
            g.DrawEllipse(p2, (float)double.Parse(textBoxrendx) - 5, (float)double.Parse(textBoxrendy) - 5, 10, 10);
        }

        public void drawstartend(Graphics g, string textBoxrstartx1, string textBoxrstarty1, string textBoxrangle, string textBoxrendx, string textBoxrendy)
        {
            //startend
            IncializePic(g);
                        
            //Draw Robot Angle
            
            x1= double.Parse(textBoxrstartx1);
            y1= double.Parse(textBoxrstarty1);
            x2 = (double)(length * Math.Cos((Math.PI/180)*(Convert.ToDouble(textBoxrangle))));
            y2 = (double)(length * Math.Sin((Math.PI/180)*(Convert.ToDouble(textBoxrangle))));
            g.DrawLine(p1, (float)x1, (float)y1, (float)x1+ (float)x2, (float)y1+ (float)y2);
            //Draw Robot Position /Start Point
            g.DrawEllipse(p1, (float)x1-5, (float)y1-5, 10, 10);
            //Draw End Point
            g.DrawEllipse(p2, (float)double.Parse(textBoxrendx)-5, (float)double.Parse(textBoxrendy)-5,10,10);
        }

    }
}