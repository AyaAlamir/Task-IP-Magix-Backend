using Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Infrastructure.Model
{
    public class Student : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

        [ForeignKey("ClassRoom")]
        public int ClassRoomID { get; set; }
        public virtual ClassRoom ClassRoom { get; set; }
    }
}
