﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.DTOs
{
    public class AddEditClassRoomInputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
    }
}
