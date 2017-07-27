﻿using System;
using log4net;
using OfficeLocationMicroservice.Core.SharedContext.Services;

namespace Logging
{
    public class LogForNetSystemLog : ISystemLog
    {
        private readonly ILog _log;

        public LogForNetSystemLog()
        {
            _log = LogManager.GetLogger(this.GetType());
        }

        public void Info(string message)
        {
            _log.Info(message);
        }

        public void Info(string message, Exception ex)
        {
            _log.Info(message, ex);
        }

        public void Error(string message, Exception ex)
        {
            _log.Error(message, ex);
        }

        public void Error(string message)
        {
            _log.Error(message);
        }

        public void Warn(string message)
        {
            _log.Warn(message);
        }

        public void Warn(string message, Exception ex)
        {
            _log.Warn(message, ex);
        }

        public void Debug(string message)
        {
            _log.Debug(message);
        }
    }

}