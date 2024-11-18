﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuCmd.Models
{
    public class SqlDac
    {
        public Guid instance_id { get; set; }
        public string instance_name { get; set; }
        public string type_name { get; set; }
        public string type_version { get; set; }
        public DateTime date_created { get; set; }
        public string created_by { get; set; }
    }
}
