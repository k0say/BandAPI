﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandAPI.Helpers
{
    public class BandsResourceParameters
    {
        public string MainGenre { get; set; }
        public string SearchQuery { get; set; }

        const int maxPageSize = 13;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 13;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = ((value > maxPageSize) || (value <= 0)) ? maxPageSize : value;
        }

        public string OrderBy { get; set; } = "Name";
        public string Fields { get; set; }
    }
}
