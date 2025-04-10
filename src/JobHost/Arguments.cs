﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerArgs;

namespace NuGet.Services.Work.JobHost
{
    public class Arguments
    {
        [ArgRequired()]
        [ArgPosition(0)]
        [ArgShortcut("j")]
        [ArgDescription("The job to invoke")]
        public string Job { get; set; }

        [ArgShortcut("p")]
        [ArgDescription("The JSON dictionary payload to provide to the job")]
        public string Payload { get; set; }

        [ArgShortcut("ep")]
        [ArgDescription("A base64-encoded UTF8 payload string to use. Designed for command-line piping")]
        public string EncodedPayload { get; set; }

        [ArgShortcut("c")]
        [ArgDescription("The JSON dictionary configuration to provide to the job")]
        public string Configuration { get; set; }

        [ArgShortcut("ec")]
        [ArgDescription("A base64-encoded UTF8 configuration string to use. Designed for command-line piping")]
        public string EncodedConfiguration { get; set; }
    }
}
