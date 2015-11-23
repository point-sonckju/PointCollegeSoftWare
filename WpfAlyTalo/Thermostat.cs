using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAlyTalo
{
    public class Thermostat
    {
        public int Temperature { get; set; }
        public void LampotilanAsetus(int NewTemperature)
        {
            Temperature = NewTemperature;
        }
    }
}
