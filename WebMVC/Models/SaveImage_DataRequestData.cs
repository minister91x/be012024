using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMVC.Models
{
    public class SaveImage_DataRequestData
    {
        public string Base64Image { get; set; }

        public string Sign { get; set; }
    }
}