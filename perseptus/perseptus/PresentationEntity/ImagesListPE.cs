﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace perseptus.PresentationEntity
{
    public class ImagesListPE
    {
        public ImagesListPE()
        {
            Images = new List<String>();
        }
        public List<String> Images { get; set; }
    }
}