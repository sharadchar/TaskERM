using BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Summary
    {
        public CollectionSummary<T> CalculateFileSummary<T>(decimal percentage, IReadOnlyCollection<T> collection,
            Func<T, decimal> propertyFunc) where T : class
        {
            if (percentage < 0)
            {
                throw new ArgumentOutOfRangeException("percentage", "Percentage cannot be negative");
            }

            if (collection == null || collection.Any() == false)
            {
                throw new Exception("[collection] is either NULL or empty");
            }

            if (propertyFunc == null)
            {
                throw new NullReferenceException("Provide the property expression to extract the data from");
            }
            //
            // If there's only one item, no need to calculate
            //
            if (collection.Count == 1)
            {
                return new CollectionSummary<T>
                {
                    Percentage = percentage,
                    Median = collection.Select(propertyFunc).First(),
                    HigherValues = null,
                    LowerValues = null
                };
            }
            //
            // Calculate Median
            //
            var itemCount = collection.Count();
            var halfPoint = itemCount / 2;
            var dataPoints = collection.AsParallel()
                .Select(propertyFunc)
                .OrderBy(x => x)
                .ToList();

            decimal median;
            if ((itemCount % 2) == 0)
            {
                median = (dataPoints[halfPoint] + dataPoints[halfPoint - 1]) / 2;
            }
            else
            {
                median = dataPoints[halfPoint];
            }

            median = decimal.Round(median, 3);

            //var 
            var medianPercentageValue = (median * percentage) / 100;
            var higherPercentValue = decimal.Round(median + medianPercentageValue, 3);
            var lowerPercentValue = decimal.Round(median - medianPercentageValue, 3);

            var higherAbnormalities = collection.AsParallel()
                .Where(x => propertyFunc(x) > higherPercentValue)
                .ToList();

            var lowerAbnormalities = collection.AsParallel()
                .Where(x => propertyFunc(x) < lowerPercentValue)
                .ToList();

            return new CollectionSummary<T>
            {
                Percentage = percentage,
                Median = median,
                HigherValues = higherAbnormalities,
                LowerValues = lowerAbnormalities
            };
        }


    }
}
