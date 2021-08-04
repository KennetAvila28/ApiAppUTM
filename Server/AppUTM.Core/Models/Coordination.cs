﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppUTM.Core.Models
{
    public class Coordination : BaseEntity
    {
        public string Nombre { get; set; }

        [JsonIgnore]
        public List<Event> Events { get; set; }
    }
}