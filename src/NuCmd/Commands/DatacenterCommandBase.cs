﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NuGet.Services.Operations.Model;
using PowerArgs;

namespace NuCmd.Commands
{
    public abstract class DatacenterCommandBase : EnvironmentCommandBase
    {
        [ArgShortcut("dc")]
        [ArgDescription("The datacenter to work in")]
        public int? Datacenter { get; set; }

        protected virtual Datacenter GetDatacenter()
        {
            return GetDatacenter(required: true);
        }

        protected virtual Datacenter GetDatacenter(bool required)
        {
            var env = GetEnvironment();
            if (!Datacenter.HasValue)
            {
                if (required)
                {
                    throw new InvalidOperationException(Strings.DatacenterCommandBase_NoDC);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return GetDatacenter(env, Datacenter.Value);
            }
        }
    }
}
