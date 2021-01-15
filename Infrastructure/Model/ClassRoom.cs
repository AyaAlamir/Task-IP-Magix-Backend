using Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Model
{
    public class ClassRoom : BaseEntity
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
    }
}
