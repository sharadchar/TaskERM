﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class File_Info <T> where T : class
    {
        public string Type { get; set; }
        public string FileName { get; set; }
        public List<LP_data> FileDataLP { get; set; }
        public List<TOU_data> FileDataTOU { get; set; }
        public CollectionSummary<T>  summary { get; set; } 
    }
}
