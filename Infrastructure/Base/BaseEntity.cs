using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Base
{
    public class BaseEntity
    {
        public DateTime? LastUpdated { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Deleted { get; set; }
    }
}
