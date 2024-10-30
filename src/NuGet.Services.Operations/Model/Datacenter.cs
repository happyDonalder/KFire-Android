﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuGet.Services.Operations.Model
{
    public class Datacenter
    {
        public int Id { get; set; }
        public string Region { get; set; }
        public string AffinityGroup { get; set; }

        public IList<Resource> Resources { get; private set; }
        public IList<Service> Services { get; private set; }

        public Datacenter()
        {
            Resources = new List<Resource>();
            Services = new List<Service>();
        }
    }
}
