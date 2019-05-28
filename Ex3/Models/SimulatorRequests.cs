using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ex3.Models
{
    public class SimulatorRequests
    {
        public static readonly Dictionary<string, string> map;

        static SimulatorRequests()
        {
            map = new Dictionary<string, string>
            {
                {"lon", "get /position/longitude-deg\r\n" },
                {"lat", "get /position/latitude-deg\r\n" },
                {"throttle", "get /controls/engines/current-engine/throttle\r\n" },
                {"rudder", "get /controls/flight/rudder\r\n" }
            };
        }    
    }
}