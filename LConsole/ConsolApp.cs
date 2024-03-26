using System;
using System.Threading;
using System.IO;
using Lab1.Tracer.serializer.ClassSerializer;
using LConsole;
using System.Collections.Concurrent;

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
            _thread.TryAdd(thread1.ManagedThreadId, thread1);
            _thread.TryAdd(thread2.ManagedThreadId, thread2);

            foreach (var thread in _thread)
                thread.Value.Start();

            foreach (var thread in _thread)
                thread.Value.Join();
            
            func.Func3(4);
            func.Func1();
            var traceResult = func.tracer.getTraceResult();

            //xml serializ 
            xmlSerializer xmlSerializ = new xmlSerializer();
            string messageXml = xmlSerializ.serialize(traceResult);
            Console.WriteLine(messageXml);
            File.WriteAllText("trace.xml", messageXml);

            Console.WriteLine("\n--------------------------------------\n");

            //json serializ 
            jsonSerializer jsonSerializ = new jsonSerializer();
            string messageJson = jsonSerializ.serialize(traceResult);
            Console.WriteLine(messageJson);
            File.WriteAllText("trace.json", messageJson);
        }
    }
}

