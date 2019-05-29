using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Ex3.Models
{
    public class DisplayFromFileModel
    {
        private double lon;
        private double lat;
        private double rudder;
        private double throttle;
        private static DisplayFromFileModel s_instace = null;
        public static DisplayFromFileModel Instance
        {
            get
            {
                if (s_instace == null)
                {
                    s_instace = new DisplayFromFileModel();
                }
                return s_instace;
            }
        }

        public void Display(string fileName, string timesPerSec)
        {
            uploadFromFile(fileName);
            int timesPerSecInt = Int32.Parse(timesPerSec);
            displayData(timesPerSecInt);
        }

        private void uploadFromFile(string fileName)
        {
            Debug.WriteLine("file", fileName);
            var fileStream = new FileStream(fileName + ".txt", FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[2048];
            try
            {
                fileStream.Read(buffer, 0, buffer.Count());
            }
            catch
            {
                Console.WriteLine("unable to read from file");
            }
            string str = Encoding.ASCII.GetString(buffer);
            parseData(str);
        }

        private void displayData(int timesPerSec)
        {

        }

        private void parseData(string data)
        {
            Debug.WriteLine("parsing data");
            string[] arrOfStrs = data.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in arrOfStrs)
            {
                string name = line.Substring(0, line.IndexOf(":"));
                string val = line.Substring(line.IndexOf(" "), line.Count() - 1);
                int value = Int32.Parse(val);
                switch (name)
                {
                    case "lon":
                        this.lon = value;
                        break;
                    case "lat":
                        this.lat = value;
                        break;
                    case "rudder":
                        this.rudder = value;
                        break;
                    case "throttle":
                        this.throttle = value;
                        break;
                    default:
                        Console.WriteLine("not a variable");
                        break;
                }

            }
            Debug.WriteLine("values");
            Debug.WriteLine(this.lon);
            Debug.WriteLine(this.lat);
            Debug.WriteLine(this.rudder);
            Debug.WriteLine(this.throttle);
        }
    }
}