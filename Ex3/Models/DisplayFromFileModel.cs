using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Ex3.Models
{
    public class DisplayFromFileModel:IDisplayModel
    {
        private double lon;
        private double lat;
        private double rudder;
        private double throttle;
        private bool atEnd = false;
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

        public Location GetLocation()
        {
            return new Location(this.lon, this.lat);
        }

        public void Display(string fileName, string timesPerSec)
        {
            uploadFromFile(fileName);
            Debug.WriteLine("done reading from file");
            int timesPerSecInt = Int32.Parse(timesPerSec);
            displayData(timesPerSecInt);
        }

        private void uploadFromFile(string fileName)
        {
            const Int32 bufferSize = 1024;
            var fileStream = new FileStream(fileName + ".txt", FileMode.Open, FileAccess.Read);
            var streamReader = new StreamReader(fileStream, Encoding.ASCII, true, bufferSize);
            var lines = File.ReadLines(fileName+".txt");
            foreach (string line in lines)
            {
                readOneLine(streamReader);
            }
        }

        private void readOneLine(StreamReader streamReader)
        {
            string str = null;
            try
            {
                str = streamReader.ReadLine();
            }
            catch
            {
                Console.WriteLine("unable to read from file");
            }
            //string str = Encoding.ASCII.GetString(buffer);
            if (!str.Equals("end"))
            {
                parseData(str);
            }
        }

        

        private void parseData(string data)
        {
            Debug.WriteLine("parsing data");
            string[] split = data.Split('\t');
            this.lon = Double.Parse(split[0]);
            this.lat = Double.Parse(split[1]);
            this.throttle = Double.Parse(split[2]);
            this.rudder = Double.Parse(split[3]);  
        }


        private void displayData(int timesPerSec)
        {

        }

    }
}