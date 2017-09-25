using BusinessLogic;
using BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERM_Power_Task
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var directoryLocation = ConfigurationManager.AppSettings.Get("DirectoryPath");
                List<File_Info> filesData = new FileLoader().GetFilesData(directoryLocation).Result.ToList();

                Summary summObj = new Summary();
                foreach (var filedata in filesData)
                {
                    if (filedata.Type == "LP")
                        summObj.CalculateFileSummary<LP_data>(20, filedata.FileDataLP, (x) => x.DataValue);
                    else
                        summObj.CalculateFileSummary<TOU_data>(20, filedata.FileDataTOU, (x) => x.Energy); ;
                }

                System.Console.ReadLine();
            }
            catch(Exception ex)
            {
                System.Console.WriteLine("Application Error Happened : " + ex.Message);
                if (ex.InnerException != null && string.IsNullOrWhiteSpace(ex.InnerException.Message))
                {
                    System.Console.WriteLine("Inner Exception : " + ex.InnerException.Message);
                }
                System.Console.Read();
            }
        }
    }
}
