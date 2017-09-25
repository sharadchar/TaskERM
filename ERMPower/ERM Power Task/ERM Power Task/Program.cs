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
                List<FileData> filesData = new FileLoader().GetFilesData(directoryLocation).Result.ToList();
                                
                ObjectCreater objCre = new ObjectCreater();
                Summary summObj = new Summary();
                List<File_Info<LP_data>> LPFileInfo = new List<File_Info<LP_data>>();
                List<File_Info<TOU_data>> TOUFileInfo = new List<File_Info<TOU_data>>();


                foreach (var filedata in filesData)
                {
                    if (filedata.Type == "LP")
                    {
                        File_Info<LP_data> tempFileInfo = new File_Info<LP_data>();
                        tempFileInfo.Type = filedata.Type;
                        tempFileInfo.FileName = filedata.FileName;
                        objCre.GetdataObjects<LP_data>(filedata.Data.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList(), tempFileInfo).Wait();
                        tempFileInfo.summary = summObj.CalculateFileSummary<LP_data>(20, tempFileInfo.FileDataLP, (x) => x.DataValue);
                        LPFileInfo.Add(tempFileInfo);
                    }
                    else
                    {
                        File_Info<TOU_data> tempFileInfo = new File_Info<TOU_data>();
                        tempFileInfo.Type = filedata.Type;
                        tempFileInfo.FileName = filedata.FileName;
                        objCre.GetdataObjects<TOU_data>(filedata.Data.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList(), tempFileInfo).Wait();
                        tempFileInfo.summary = summObj.CalculateFileSummary<TOU_data>(20, tempFileInfo.FileDataTOU, (x) => x.Energy); ;
                        TOUFileInfo.Add(tempFileInfo);
                    }
                }

                DisplayInformation(LPFileInfo, TOUFileInfo);

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

        /// <summary>
        /// This method displays the information to the user
        /// </summary>
        /// <param name="LPFileInfo">List of LP file information</param>
        /// <param name="TOUFileInfo">List of TOU file information</param>
        private static void DisplayInformation(List<File_Info<LP_data>> LPFileInfo, List<File_Info<TOU_data>> TOUFileInfo)
        {
            if (LPFileInfo.Count > 0)
            {
                System.Console.WriteLine("There are " + LPFileInfo.Count.ToString() + " LP type files");
                foreach (var item in LPFileInfo)
                {
                    System.Console.WriteLine("File " + item.FileName + ": has median = " + item.summary.Median);
                }
                System.Console.WriteLine("-------------------------------------------------------------------------------");
            }
            else
            {
                System.Console.WriteLine("The is no LP type files");
                System.Console.WriteLine("-------------------------------------------------------------------------------");
            }

            if (LPFileInfo.Count > 0)
            {
                System.Console.WriteLine("There are " + TOUFileInfo.Count.ToString() + " TOU type files");
                foreach (var item in TOUFileInfo)
                {
                    System.Console.WriteLine("File " + item.FileName + ": has median = " + item.summary.Median);
                }
                System.Console.WriteLine("-------------------------------------------------------------------------------");
            }
            else
            {
                System.Console.WriteLine("The is no TOU type files");
                System.Console.WriteLine("-------------------------------------------------------------------------------");

            }
        }
    }
}
