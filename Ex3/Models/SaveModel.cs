using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ex3.Models
{
    public class SaveModel:IModel
    {
        private readonly List<string> argsForSimulator = new List<string>(){"lon", "lat", "throttle", "rudder"};
        private readonly string last = "rudder";
        private double lon;
        private double lat;
        private Location location;
        private FileStream fs;
        private static SaveModel s_instace = null;
        public static SaveModel Instance
        {
            get
            {
                if (s_instace == null)
                {
                    s_instace = new SaveModel();
                }
                return s_instace;

            }
        }

        public Location GetLocation()
        {
            return this.location;
        }

        public void ReadData()
        {
            writeOnceToFile(fs);
            //double lat = Client.ReadFromServer("lat");
            //double lon = Client.ReadFromServer("lon");
            //this.location = new Location(lon, lat);
        }

        public void Connect(string ip, string port)
        {
            Client.Connect(ip, port);
            Debug.WriteLine("connected to client");
        }

        public void SaveToFile(string fileName, int timesPerSec, int numOfSec)
        {
            fs = createFile(fileName);
            writeToFile(fs, timesPerSec, numOfSec);
            Debug.WriteLine("saved to file");
        }

        public void openFile(string fileName)
        {
            fs = createFile(fileName);
            Debug.WriteLine("Opened file");
        }


        private FileStream createFile(string fileName)
        {
            string fileWEtx = fileName + ".txt";
            return File.Create(Path.GetTempPath() + fileWEtx);
        }

        private void writeToFile(FileStream fs, int timesPerSec, int numOfSecs)
        {
            for(int i = 0; i < numOfSecs; i++)
            {
                for (int j = 0; j < timesPerSec; ++j)
                {
                    var stopWatch = Stopwatch.StartNew();
                    writeOnceToFile(fs);

                    var ms = (int)stopWatch.ElapsedMilliseconds;
                    int times = 1000 / timesPerSec;
                    if (ms < times)
                        Task.Delay(times - ms);
                }
            }
            byte[] end = Encoding.ASCII.GetBytes("end");
            fs.Write(end, 0, end.Length);
            fs.Close();
            fs = null;
        }

        public void CloseFile()
        {
            fs.Close();
            fs = null;
        }

        private void writeOnceToFile(FileStream fs)
        {
            string oneRow = "";
            foreach (string str in argsForSimulator)
            {
                byte[] writeFileNameb = Encoding.ASCII.GetBytes(str);
                double result = Client.ReadFromServer(str);
                updateLocation(str, result);
                if (!str.Equals(this.last))
                {
                    oneRow += result.ToString() + "\t";
                }
                else
                {
                    oneRow += result.ToString() + "\r\n";
                }
            }
            if (fs != null)
            {
                byte[] oneRowBytes = Encoding.ASCII.GetBytes(oneRow);
                fs.Write(oneRowBytes, 0, oneRowBytes.Length);
            }
                


        }

        private void updateLocation(string type, double data)
        {
            switch (type)
            {
                case "lon":
                    this.lon = data;
                    break;
                case "lat":
                    this.lat = data;
                    break;
                default:
                    Console.WriteLine("invalid");
                    break;
            }

            this.location = new Location(lon, lat);
        }
    }
}