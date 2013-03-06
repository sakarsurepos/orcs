using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Robot
{
    class GPS
    {
        public static FileStream fsgps;
        public static TextWriter writegps;        
        
        public static void GPSFileLogCreate()
        {
            try
            {
                fsgps = new FileStream("gpslog.txt", FileMode.Append);  //zapis do suboru
            }
            catch
            {
                fsgps = new FileStream("gpslog.txt", FileMode.CreateNew);
            }
            writegps = new StreamWriter(fsgps);
        }

        public static string GPS_UTC_Time(string GPS_RMC_Data)
        {
            char[] GPS_Data         = GPS_RMC_Data.ToCharArray();

            string UTC_Time_Hours   = new string(GPS_Data, 7, 2);
            string UTC_Time_Minutes = new string(GPS_Data, 9, 2);
            string UTC_Time_Seconds = new string(GPS_Data, 11, 2);

            return UTC_Time_Hours + ":" + UTC_Time_Minutes + ":" + UTC_Time_Seconds;
        }

        public static string GPS_Status(string GPS_RMC_Data)
        {
            string GPS_PositionStatus = null;
            char[] GPS_Data           = GPS_RMC_Data.ToCharArray();

            string Status = new string(GPS_Data, 14, 1);
            if (Status == "A")
            {
                GPS_PositionStatus = "Fix";
            }
            else if (Status == "V")
            {
                GPS_PositionStatus = "Not";
            }
            return GPS_PositionStatus;
        }

        public static string GPS_Latitude(string GPS_RMC_Data, string type)
        {
            char[] GPS_Data = GPS_RMC_Data.ToCharArray();

            string Latitude_Degree  = new string(GPS_Data, 16, 2);
            string Latitude_Minute  = new string(GPS_Data, 18, 2);
            string Latitude_Second  = new string(GPS_Data, 21, 4);
            string N_S_Indicator    = new string(GPS_Data, 26, 1);

            if (N_S_Indicator == "N")
            {
                N_S_Indicator = "North";
            }
            else if (N_S_Indicator == "S")
            {
                N_S_Indicator = "South";
            }

            if (type == "info")
            {
                return "  " + Latitude_Degree + "° " + Latitude_Minute + "´ "
                    + (double.Parse(Latitude_Second) / 100 * 60 / 100).ToString("f2") + "´´   " + N_S_Indicator;
            }
            if (type == "number")
            {
                double LatDeg = int.Parse(Latitude_Degree) + (float.Parse(Latitude_Minute) / 60) 
                                + float.Parse(Latitude_Second) / 10000 / 60;
                return LatDeg.ToString("f6");
            }
            else
            {
                return " ";
            }
        }

        public static string GPS_Longitude(string GPS_RMC_Data, string type)
        {
            char[] GPS_Data = GPS_RMC_Data.ToCharArray();

            string Longitude_Degree     = new string(GPS_Data, 28, 3);
            string Longitude_Minute     = new string(GPS_Data, 31, 2);
            string Longitude_Second     = new string(GPS_Data, 34, 4);
            string E_W_Indicator        = new string(GPS_Data, 39, 1);

            if (E_W_Indicator == "E")
            {
                E_W_Indicator = "East";
            }
            else if (E_W_Indicator == "W")
            {
                E_W_Indicator = "West";
            }

            if (type == "info")
            {
                return " " + Longitude_Degree + "° " + Longitude_Minute + "´ "
                    + (double.Parse(Longitude_Second) / 100 * 60 / 100).ToString("f2") + "´´   " + E_W_Indicator;
            }
            else if (type == "number")
            {
                double LatDeg = int.Parse(Longitude_Degree) + (float.Parse(Longitude_Minute) / 60)
                                + float.Parse(Longitude_Second) / 10000 / 60;
                return LatDeg.ToString("f6");
            }
            else
            {
                return " ";
            }
        }

        public static string GPS_Speed(string GPS_RMC_Data)
        {
            char[] GPS_Data     = GPS_RMC_Data.ToCharArray();
            string Speed1       = new string(GPS_Data, 41, 3);
            string Speed2       = new string(GPS_Data, 45, 1);
            double _speed_      = (double.Parse(Speed1) + double.Parse(Speed2) * 0.1) * 1.852;

            return _speed_.ToString("f3") + "  km/h";
        }

        public static string GPS_Date(string GPS_RMC_Data)
        {
            char[] GPS_Data = GPS_RMC_Data.ToCharArray();

                string Date_Day = new string(GPS_Data, 53, 2);
                string Date_Month = new string(GPS_Data, 55, 2);
                string Date_Year = new string(GPS_Data, 57, 2);
                return Date_Day + "." + Date_Month + "." + "20" + Date_Year;
        }
                
        public static string GPS_Course(string GPS_RMC_Data)
        {
            char[] GPS_Data = GPS_RMC_Data.ToCharArray();
            string Course_Over_Ground = new string(GPS_Data, 47, 5) + "°";
            return Course_Over_Ground;
        }

        public static float GPS_Course2(string GPS_RMC_Data)
        {
            char[] GPS_Data = GPS_RMC_Data.ToCharArray();
            string Course_Over_Ground = new string(GPS_Data, 47, 5);
            string Course_Over_Ground2 = "0";

            if (Course_Over_Ground == "000.0")
            {//0
                Course_Over_Ground2 = "0";
            }
            if (Course_Over_Ground[0] == '0' && Course_Over_Ground[1] == '0' && Course_Over_Ground[2] == '0' && Course_Over_Ground[4] != '0') //
            {//1
                Course_Over_Ground2 = new string(GPS_Data, 51, 1);
            }
            if (Course_Over_Ground[0] == '0' && Course_Over_Ground[1] == '0' && Course_Over_Ground[2] != '0') //
            {//2
                Course_Over_Ground2 = new string(GPS_Data, 49, 1);
                Course_Over_Ground2 = Course_Over_Ground2 + new string(GPS_Data, 51, 1);
            }
            if (Course_Over_Ground[0] == '0' && Course_Over_Ground[1] != '0')
            {//4
                Course_Over_Ground2 = new string(GPS_Data, 48, 2);
                Course_Over_Ground2 = Course_Over_Ground2 + new string(GPS_Data, 51, 1);
            }
            if (Course_Over_Ground[0] != '0')
            {//5
                Course_Over_Ground2 = new string(GPS_Data, 47, 3);
                Course_Over_Ground2 = Course_Over_Ground2 + new string(GPS_Data, 51, 1);
            }
            return float.Parse(Course_Over_Ground2);
        }

    }
}
