﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.Services.Work.Monitoring
{
    public class EventSourceReference
    {
        public EventSource Source { get; private set; }
        public EventLevel Level { get; private set; }

        public EventSourceReference(EventSource source, EventLevel level)
        {
            Source = source;
            Level = level;
        }
    }
}
