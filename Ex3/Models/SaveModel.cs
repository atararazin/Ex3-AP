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

        public void Connect(string ip, string port)
        {
            Client.Connect(ip, port);
            Debug.WriteLine("connected to client");
        }

        public void SaveToFile(string fileName)
        {
            FileStream fs = createFile(fileName);
            writeToFile(fs);
            Debug.WriteLine("saved to file");
        }

        private FileStream createFile(string fileName)
        {
            Debug.WriteLine("creating file");
            string fileWEtx = fileName + ".txt";
            return File.Create(fileWEtx);
        }

        private void writeToFile(FileStream fs)
        {
            string last = argsForSimulator.Last();
            foreach (string str in argsForSimulator)
            {
                byte[] writeFileNameb = Encoding.ASCII.GetBytes(str + ":" + " ");
                fs.Write(writeFileNameb, 0, writeFileNameb.Length);
                double result = Client.ReadFromServer(str);
                byte[] writeFileResb;
                if (str.Equals(last))
                {
                    writeFileResb = Encoding.ASCII.GetBytes(result.ToString());

                }
                else
                {
                    writeFileResb = Encoding.ASCII.GetBytes(result.ToString() + "\r\n");

                }
                fs.Write(writeFileResb, 0, writeFileResb.Length);
            }
            fs.Close();
        }
    }
}