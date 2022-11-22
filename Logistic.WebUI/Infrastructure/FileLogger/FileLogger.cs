﻿using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Logistic.WebUI.Infrastructure.FileLogger
{
    //
    public class FileLogger : ILogger
    {
        private readonly object _lock = new();
        private readonly string filePath;

        public FileLogger(string path)
        {
            filePath = path;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            //return logLevel == LogLevel.Trace;
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
                lock (_lock)
                {
                    File.AppendAllText(filePath, formatter(state, exception) + Environment.NewLine);
                }
        }
    }
}