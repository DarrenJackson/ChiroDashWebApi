﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChiroDash.Application.Doctors.Models
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
