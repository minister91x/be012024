﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ProductNetFrameWork.DTO
{
    public class ReturnData
    {
        public int returnCode { get; set; }
        public string returnMessage { get; set; }
    }

    public class SaveImageReturnData
    {
        public int ReturnCode { get; set; }
        public string ReturnMsg { get; set; }
    }
}
