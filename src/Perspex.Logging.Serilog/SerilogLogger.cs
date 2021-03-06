﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System.Collections.Generic;
using Serilog;
using PerspexLogEventLevel = Perspex.Logging.LogEventLevel;
using SerilogLogEventLevel = Serilog.Events.LogEventLevel;

namespace Perspex.Logging.Serilog
{
    public class SerilogLogger : ILogSink
    {
        private readonly ILogger _output;
        private readonly Dictionary<string, ILogger> _areas = new Dictionary<string, ILogger>();

        public SerilogLogger(ILogger output)
        {
            _output = output;
        }

        public static void Initialize(ILogger output)
        {
            Logger.Sink = new SerilogLogger(output);
        }

        public void Log(
            PerspexLogEventLevel level, 
            string area, 
            object source, 
            string messageTemplate, 
            params object[] propertyValues)
        {
            ILogger areaLogger;

            if (!_areas.TryGetValue(area, out areaLogger))
            {
                areaLogger = _output.ForContext("Area", area);
                _areas.Add(area, areaLogger);
            }

            areaLogger.Write((SerilogLogEventLevel)level, messageTemplate, propertyValues);
        }
    }
}
