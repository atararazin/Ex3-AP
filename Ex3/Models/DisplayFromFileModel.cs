using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Ex3.Models
{
    public class DisplayFromFileModel:IModel
    {
        private double lon;
        private double lat;
        private double rudder;
        private double throttle;
        private Location location;
        private string upTo;
        private StreamReader streamReader;

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
            return this.location;
        }

        public void OpenFile(string fileName)
        {
            Debug.WriteLine("opening file");
            const Int32 bufferSize = 1024;
            var fileStream = new FileStream(fileName + ".txt", FileMode.Open, FileAccess.Read);
            this.streamReader = new StreamReader(fileStream, Encoding.ASCII, true, bufferSize);
            this.upTo = streamReader.ReadLine();
        }

        public void ReadData()
        {
            readOneLine();
        }

        public void Display(string fileName, string timesPerSec)
        {
            uploadFromFile(fileName);
            int timesPerSecInt = Int32.Parse(timesPerSec);
        }

        private void uploadFromFile(string fileName)
        {
            const Int32 bufferSize = 1024;
            var fileStream = new FileStream(fileName + ".txt", FileMode.Open, FileAccess.Read);
            this.streamReader = new StreamReader(fileStream, Encoding.ASCII, true, bufferSize);
            //var lines = File.ReadLines(fileName+".txt");
            this.upTo = streamReader.ReadLine();
            //foreach (string line in lines)
            //{
            //    readOneLine(streamReader);
            //}
        }

        private void readOneLine()
        {
            if (!upTo.Equals("end"))
            {
                parseData(upTo);
            }
            string str = null;
            try
            {
                str = this.streamReader.ReadLine();
            }
            catch
            {
                Console.WriteLine("unable to read from file");
            }
            if (!str.Equals("end"))
            {
                upTo = str;
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
            this.location = new Location(lon, lat);
        }
    }
}