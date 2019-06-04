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
        private FileStream fs;

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
            this.fs = new FileStream(Path.GetTempPath() + fileName + ".txt", FileMode.Open, FileAccess.Read);
            this.streamReader = new StreamReader(fs, Encoding.ASCII, true, bufferSize);
            this.upTo = streamReader.ReadLine();
        }

        public void ReadData()
        {
            readOneLine();
        }


        private void readOneLine()
        {
            parseData(upTo);
            if(upTo.Equals("end"))
            {
                this.fs.Close();
                return;
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
                upTo = str;
        }
        
        private void parseData(string data)
        {
            if (data.Equals("end"))
            {
                this.location = null;
                return;
            }
            string[] split = data.Split('\t');
            this.lon = Double.Parse(split[0]);
            this.lat = Double.Parse(split[1]);
            this.throttle = Double.Parse(split[2]);
            this.rudder = Double.Parse(split[3]);
            this.location = new Location(lon, lat);
        }
    }
}