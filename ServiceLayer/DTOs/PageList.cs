using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.DTOs
{
    public class PageList<T>
    {
        public PageList()
        {
            DataList = new List<T>();
        }
        public List<T> DataList { get; set; }
        public int TotalCount { get; set; }

    }
}
