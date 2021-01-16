using System;
using System.Collections.Generic;
using System.Text;
using static Shared.Enum.CommonEnum;

namespace ServiceLayer.DTOs
{
    public class PagingModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public SortingModel SortingModel { get; set; }
    }
    public class SortingModel
    {
        public string SortingExpression { get; set; }
        public SortDirectionEnum SortingDirection { get; set; }
    }
}
