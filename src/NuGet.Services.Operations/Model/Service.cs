﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuGet.Services.Operations.Model
{
    public class Service : EnvironmentComponentBase
    {
        public Uri Uri { get; set; }

        public Service() : base() { }
    }
}
