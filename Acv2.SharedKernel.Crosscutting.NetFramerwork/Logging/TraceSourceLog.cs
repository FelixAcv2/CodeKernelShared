using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Security;
using System.Text;
using Acv2.SharedKernel.Crosscutting.Logging;

namespace Acv2.SharedKernel.Crosscutting.NetFramerwork.Logging
{


    /// <summary>
    /// Implementation of contract <see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/>
    /// using System.Diagnostics API.
    /// </summary>
    public sealed class TraceSourceLog
        : ILogger
    {
        #region Members

        TraceSource source;

        #endregion

        #region  Constructor

        /// <summary>
        /// Create a new instance of this trace manager
        /// </summary>
        public TraceSourceLog()
        {
            // Create default source
            source = new TraceSource("Acv2Log");
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Trace internal message in configured listeners
        /// </summary>
        /// <param name="eventType">Event type to trace</param>
        /// <param name="message">Message of event</param>
        void TraceInternal(TraceEventType eventType, string message)
        {

            if (source != null)
            {
                try
                {
                    source.TraceEvent(eventType, (int)eventType, message);
                }
                catch (SecurityException)
                {
                    //Cannot access to file listener or cannot have
                    //privileges to write in event log etc...
                }
            }
        }
        #endregion

        #region ILogger Members

        /// <summary>
        /// <see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/></param>
        /// <param name="args"><see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/></param>
        public void LogInfo(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                TraceInternal(TraceEventType.Information, messageToTrace);
            }
        }
        /// <summary>
        /// <see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/></param>
        /// <param name="args"><see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/></param>
        public void LogWarning(string message, params object[] args)
        {

            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                TraceInternal(TraceEventType.Warning, messageToTrace);
            }
        }

        /// <summary>
        /// <see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/></param>
        /// <param name="args"><see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/></param>
        public void LogError(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                TraceInternal(TraceEventType.Error, messageToTrace);
            }
        }

        /// <summary>
        /// <see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/></param>
        /// <param name="exception"><see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/></param>
        /// <param name="args"><see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/></param>
        public void LogError(string message, Exception exception, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message)
                &&
                exception != null)
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                var exceptionData = exception.ToString(); // The ToString() create a string representation of the current exception

                TraceInternal(TraceEventType.Error, string.Format(CultureInfo.InvariantCulture, "{0} Exception:{1}", messageToTrace, exceptionData));
            }
        }

        /// <summary>
        /// <see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/></param>
        /// <param name="args"><see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/></param>
        public void Debug(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                TraceInternal(TraceEventType.Verbose, messageToTrace);
            }
        }

        /// <summary>
        /// <see cref="Acv2.SharedKernel.Transversales.Logging.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/></param>
        /// <param name="exception"><see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/></param>
        /// <param name="args"><see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/></param>
        public void Debug(string message, Exception exception, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message)
                &&
                exception != null)
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                var exceptionData = exception.ToString(); // The ToString() create a string representation of the current exception

                TraceInternal(TraceEventType.Error, string.Format(CultureInfo.InvariantCulture, "{0} Exception:{1}", messageToTrace, exceptionData));
            }
        }

        /// <summary>
        /// <see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/>
        /// </summary>
        /// <param name="item"><see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/></param>
        public void Debug(object item)
        {
            if (item != null)
            {
                TraceInternal(TraceEventType.Verbose, item.ToString());
            }
        }

        /// <summary>
        /// <see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/></param>
        /// <param name="args"><see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/></param>
        public void Fatal(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                TraceInternal(TraceEventType.Critical, messageToTrace);
            }
        }

        /// <summary>
        /// <see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/></param>
        /// <param name="exception"><see cref="Acv2.SharedKernel.Crosscutting.Logging.ILogger"/></param>
        public void Fatal(string message, Exception exception, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message)
                &&
                exception != null)
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                var exceptionData = exception.ToString(); // The ToString() create a string representation of the current exception

                TraceInternal(TraceEventType.Critical, string.Format(CultureInfo.InvariantCulture, "{0} Exception:{1}", messageToTrace, exceptionData));
            }
        }


        #endregion
    }

}
