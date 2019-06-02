using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex3.Models
{
    public class DisplayModelBase
    {
        protected double lon;
        protected double lat;
        protected Location location;
        private static DisplayModelBase s_instace = null;
        public static DisplayModelBase Instance
        {
            get
            {
                if (s_instace == null)
                {
                    s_instace = new DisplayModelBase();
                }
                return s_instace;
            }
        }

        public Location GetLocation()
        {
            return this.location;
        }

        public void Connect(string ip, string port)
        {
            Console.WriteLine("doesnt have a connect");
        }
    }
}
