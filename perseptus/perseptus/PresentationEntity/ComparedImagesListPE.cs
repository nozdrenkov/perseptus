using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace perseptus.PresentationEntity
{
    public class ComparedImagesListPE
    {
        public ComparedImagesListPE()
        {
            Images = new List<KeyValuePair<string, double>>();
        }
        public List<KeyValuePair<string, double>> Images { get; set; }
        public String OriginImageFilePath { get; set; }
    }
}