using BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ObjectCreater 
    {
        /// <summary>
        /// This method reads the file on the basis of the file name/type LP ot TOU 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="tempobj"></param>
        /// <returns></returns>
        public async Task GetdataObjects<T>(List<string> content, File_Info<T> tempobj) where T : class
        {
            try
            {
                content.RemoveAll(x => string.IsNullOrWhiteSpace(x));
                if (tempobj.Type == "LP")
                {
                    tempobj.FileDataLP = await GetLPData(content);
                }
                else
                {
                    tempobj.FileDataTOU = await GetTOUData(content);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// This method creates the list of the LPData objects corresponding to each row in content array
        /// </summary>
        /// <param name="content">Input array content of the file</param>
        /// <returns>returns the list of LPData objects</returns>
        private async Task<List<LP_data>> GetLPData(List<string> content)
        {
            List<LP_data> tempList = new List<LP_data>();
            try
            {
                foreach (string conItem in content)
                {
                    var splitValues = await splitItems(conItem);
                    if (splitValues == null)
                        continue;
                    else
                    {
                        tempList.Add(new LP_data()
                        {
                            MeterPointCode = int.Parse(splitValues[0].Trim()),
                            SerialNumber = int.Parse(splitValues[1].Trim()),
                            PlantCode = splitValues[2].Trim(),
                            dateTime = DateTime.Parse(splitValues[3].Trim()),
                            DataType = splitValues[4].Trim(),
                            DataValue = decimal.Parse(splitValues[5].Trim()),
                            Units = splitValues[6].Trim(),
                            Status = splitValues[7].Trim()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tempList;
        }

        /// <summary>
        /// This method creates the list of the TOUData objetc corresponding to each row in content array
        /// </summary>
        /// <param name="content">Input array content of the file</param>
        /// <returns>returns the list of TOUData objects</returns>
        private async Task<List<TOU_data>> GetTOUData(List<string> content)
        {
            List<TOU_data> tempList = new List<TOU_data>();
            try
            {
                foreach (string conItem in content)
                {
                    var splitValues = await splitItems(conItem);
                    if (splitValues == null)
                        continue;
                    else
                    {
                        tempList.Add(new TOU_data()
                        {
                            MeterPointCode = int.Parse(splitValues[0].Trim()),
                            SerialNumber = int.Parse(splitValues[1].Trim()),
                            PlantCode = splitValues[2].Trim(),
                            dateTime = DateTime.Parse(splitValues[3].Trim()),
                            DataType = splitValues[4].Trim(),
                            Energy = decimal.Parse(splitValues[5].Trim()),
                            MaximumDemand = decimal.Parse(splitValues[6].Trim()),
                            //TimeofMaxDemand = DateTime.ParseExact(splitValues[7].Trim(), "dd-MM-yyyy HH:mm:ss tt", null);
                            Units = splitValues[8].Trim(),
                            Status = splitValues[9].Trim(),
                            Period = splitValues[10].Trim(),
                            DLSActive = bool.Parse(splitValues[11].Trim().ToLower()),
                            BillingResetCount = int.Parse(splitValues[12].Trim()),
                            //BillingResetDateTime = DateTime.ParseExact(splitValues[13].Trim(), "dd-MM-yyyy HH:mm:ss tt", null);
                            Rate = splitValues[14].Trim()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tempList;
        }

        /// <summary>
        /// This method splits the recieved string by ',' and returns the array
        /// </summary>
        /// <param name="row">Input string to split</param>
        /// <returns>retuns the array od strings</returns>
        private async Task<string[]> splitItems(string row)
        {
            try
            {
                var splitFields = row.Split(',');
                if (splitFields[0] == "MeterPoint Code") //Asuming 0,0 cell of csv is MeterPointCode
                    return null;
                else
                    return splitFields;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
