﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Tracer
{
    public interface ITracer
    {
        void startTrace();

        void stopTrace();

        TraceResult getTraceResult();
    }
}
