using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class LP_data 
    {
        public int MeterPointCode { get; set; }
        public int SerialNumber { get; set; }
        public string PlantCode { get; set; }
        public DateTime dateTime { get; set; }
        public string DataType { get; set; }
        public decimal DataValue { get; set; }
        public string Units { get; set; }
        public string Status { get; set; }
    }
}
