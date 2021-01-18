using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.DTOs
{
    public class AddEditStudentInputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClassRoomID { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
