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
    public class SaveModel
    {
        private List<string> argsForSimulator = new List<string>(){"lon", "lat", "throttle", "rudder"};
        private readonly string last = "rudder";
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

        public void Connect(string ip, string port)
        {
            Client.Connect(ip, port);
            Debug.WriteLine("connected to client");
        }

        public void SaveToFile(string fileName, int timesPerSec, int numOfSec)
        {
            FileStream fs = createFile(fileName);
            writeToFile(fs, timesPerSec, numOfSec);
            Debug.WriteLine("saved to file");
        }

        private FileStream createFile(string fileName)
        {
            Debug.WriteLine("creating file");
            string fileWEtx = fileName + ".txt";
            return File.Create(fileWEtx);
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
            fs.Close();
        }

        private void writeOnceToFile(FileStream fs)
        {   
            foreach (string str in argsForSimulator)
            {
                byte[] writeFileNameb = Encoding.ASCII.GetBytes(str);
                double result = Client.ReadFromServer(str);
                byte[] writeFileResb;
                if (!str.Equals(this.last))
                {
                    writeFileResb = Encoding.ASCII.GetBytes(result.ToString() + "\t");
                }
                else
                {
                    writeFileResb = Encoding.ASCII.GetBytes(result.ToString() + "\r\n");
                }
                fs.Write(writeFileResb, 0, writeFileResb.Length);
            }
        }
    }
}