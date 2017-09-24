using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class TOU_data
    {
        public int MeterPointCode { get; set; }
        public int SerialNumber { get; set; }
        public string PlantCode { get; set; }
        public DateTime dateTime { get; set; }
        public string DataType { get; set; }
        public decimal Energy { get; set; }
        public decimal MaximumDemand { get; set; }
        public DateTime TimeofMaxDemand { get; set; }
        public string Units { get; set; }
        public string Status { get; set; }
        public string Period { get; set; }
        public Boolean DLSActive { get; set; }
        public int BillingResetCount { get; set; }
        public DateTime BillingResetDateTime { get; set; }
        public string Rate { get; set; }
    }
}
