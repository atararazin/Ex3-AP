using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex3.Models
{
    interface IModel
    {
        Location GetLocation();
        void ReadData();
    }
}
