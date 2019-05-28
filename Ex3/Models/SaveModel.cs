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
        private List<string> argsForSimulator = new List<string>(){"lon", "lat", "throttle", "rudder"};
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
            string fileWEtx = fileName + ".txt";
            FileStream fs = File.Create(fileWEtx);

            double result;
            byte[] writeStr;
            foreach(string str in argsForSimulator)
            {
                writeStr = Encoding.ASCII.GetBytes(str + ":\t");
                fs.Write(writeStr, 0, str.Length);
                result = Client.ReadFromServer(str);
                writeStr = Encoding.ASCII.GetBytes(result.ToString());
                fs.Write(writeStr, 0, writeStr.Length);
            }

            Byte[] data = Encoding.ASCII.GetBytes("hello");
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