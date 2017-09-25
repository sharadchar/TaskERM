using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class CollectionSummary<T> where T : class
    {
        public decimal Median { get; set; }
        public decimal Percentage { get; set; }
        public IEnumerable<T> LowerValues { get; set; }
        public IEnumerable<T> HigherValues { get; set; }
        public IEnumerable<T> AbnormalValues { get; set; }
    }
}
