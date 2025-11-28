﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NuGet.Services.Work.Monitoring
{
    internal class TracingHttpHandler : DelegatingHandler
    {
        public event Action<HttpRequestMessage> OnSend;
        public event Action<HttpRequestMessage, Exception> OnException;
        public event Action<HttpRequestMessage, HttpResponseMessage> OnReceive;

        public TracingHttpHandler() : base() { }
        public TracingHttpHandler(HttpMessageHandler innerHandler) : base(innerHandler) { }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Trace the sending of the request
            TraceSend(request);
            HttpResponseMessage response;
            try
            {
                response = await base.SendAsync(request, cancellationToken);
            }
            catch (Exception exception)
            {
                TraceException(request, exception);
                throw;
            }
            TraceReceive(request, response);
            return response;
        }

        protected virtual void TraceReceive(HttpRequestMessage request, HttpResponseMessage response)
        {
            var handler = OnReceive;
            if (handler != null)
            {
                handler(request, response);
            }
        }

        protected virtual void TraceException(HttpRequestMessage request, Exception exception)
        {
            var handler = OnException;
            if (handler != null)
            {
                handler(request, exception);
            }
        }

        protected virtual void TraceSend(HttpRequestMessage request)
        {
            var handler = OnSend;
            if (handler != null)
            {
                handler(request);
            }
        }
    }
}
