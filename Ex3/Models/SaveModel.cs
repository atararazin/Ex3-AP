using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Ex3.Models
{
    public class SaveModel
    {
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

        public void SaveToFile(string fileName)
        {
            Debug.WriteLine("writing to file");
            Byte[] getLonCommand = Encoding.ASCII.GetBytes(SimulatorRequests.map["lon"]);
            Byte[] getLatCommand = Encoding.ASCII.GetBytes(SimulatorRequests.map["lat"]);
            Byte[] getThrottleCommand = Encoding.ASCII.GetBytes(SimulatorRequests.map["throttle"]);
            Byte[] getRudderCommand = Encoding.ASCII.GetBytes(SimulatorRequests.map["rudder"]);

            Byte[] data = Encoding.ASCII.GetBytes("hello");
            string fileWEtx = fileName + ".txt";
            FileStream fs = File.Create(fileWEtx);
            fs.Write(data, 0, data.Length);
            fs.Close();

            // Open the stream and read it back.    
            StreamReader sr = File.OpenText(fileWEtx);
            string s = "";
            while ((s = sr.ReadLine()) != null)
            {
                Debug.WriteLine(s);
            }
            Debug.WriteLine("done writing to file");

        }
    }
}