﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChiroDash.Application.Assistants.Models
{
    public class AssistantToUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
