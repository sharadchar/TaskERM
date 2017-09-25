using BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class FileLoader 
    {
        public async Task<IEnumerable<FileData>> GetFilesData(string directoryLocation)
        {
            var filePaths = Directory.GetFiles(directoryLocation);

            List<FileData> files = new List<FileData>();

            try
            {
                var fileContent = await Task.WhenAll(ReadFiles(filePaths));
                
                files = fileContent.ToList();

                return files;
                //return fileContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        /// <summary>
        /// This method reads the files as string
        /// </summary>
        /// <param name="filePaths"></param>
        /// <returns></returns>
        private IEnumerable<Task<FileData>> ReadFiles(string[] filePaths)
        {
            try
            {
                return filePaths.Select(async filePath =>
                            {
                                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                                {
                                    using (var streamReader = new StreamReader(fileStream))
                                    {
                                        FileData tempobj = new FileData();
                                        if (filePath.Contains("\\LP"))
                                            tempobj.Type = "LP";
                                        else
                                            tempobj.Type = "TOU";

                                        string[] name = filePath.Split('\\');
                                        tempobj.FileName = name[name.Length - 1];

                                        var content = await streamReader.ReadToEndAsync();
                                        tempobj.Data = content;
                                        //

                                        return tempobj;                                        
                                    }
                                }
                            });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        

    }
}
