using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.DTOs
{
    public class ClassRoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
