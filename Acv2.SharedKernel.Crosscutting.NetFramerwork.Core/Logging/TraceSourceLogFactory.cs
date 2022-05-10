using System;
using System.Collections.Generic;
using System.Text;
using Acv2.SharedKernel.Crosscutting.Core.Logging;
using Acv2.SharedKernel.Crosscutting.NetFramerwork.Core.Logging;

namespace Acv2.SharedKernel.Crosscutting.NetFramerwork.Core.Logging
{
    /// <summary>
    /// A Trace Source base, log factory
    /// </summary>
    public class TraceSourceLogFactory
        : ILoggerFactory
    {
        /// <summary>
        /// Create the trace source log
        /// </summary>
        /// <returns>New ILog based on Trace Source infrastructure</returns>
        public ILogger Create()
        {
            return new TraceSourceLog();
        }
    }
}
