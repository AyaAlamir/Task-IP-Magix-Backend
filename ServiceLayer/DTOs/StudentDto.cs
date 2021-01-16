using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
