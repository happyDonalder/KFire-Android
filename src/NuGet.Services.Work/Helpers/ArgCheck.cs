﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.Services.Work
{
    internal static class ArgCheck
    {
        public static void Require(string value, string name)
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void Require(object value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
