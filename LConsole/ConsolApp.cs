using System;
using Lab1.Tracer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;
using Lab1.Tracer.serializer.ClassSerializer;
using LConsole;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;

namespace Lab1.LConsol
{
    public class ConsolApp
    {
        public static FuncConsol func = new FuncConsol();
        public static ConcurrentDictionary<int, Thread> _thread = new ConcurrentDictionary<int, Thread>();

        public static void Main()
        {
            var thread1 = new Thread(func.Func1);
            var thread2 = new Thread(func.Func2);
            _thread.TryAdd(0, thread1);
            _thread.TryAdd(1, thread2);

            _thread[0].Start();
            _thread[1].Start();

            _thread[0].Join();
            _thread[1].Join();

            func.Func3(4);
            func.Func1();
            func.Func1();
            var traceResult = func.tracer.getTraceResult();

            xmlSerializer xmlSerializ = new xmlSerializer();
            string messageXml = xmlSerializ.serialize(traceResult);
            Console.WriteLine(messageXml);
            File.WriteAllText("trace.xml", messageXml);

            Console.WriteLine("\n--------------------------------------\n");

            jsonSerializer jsonSerializ = new jsonSerializer();
            string messageJson = jsonSerializ.serialize(traceResult);
            Console.WriteLine(messageJson);
            File.WriteAllText("trace.json", messageJson);
        }
    }
}

