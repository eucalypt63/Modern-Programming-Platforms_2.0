using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Tracer.serializer.ClassSerializer
{
    public class jsonSerializer : ITraceSerializer
    {
        public string serialize(TraceResult traceResult)
        {
            string result = "";
            result += "{\n    \"threads\": [\n";

            foreach (TThread thread in traceResult.threads)
            {
                result = result.PadRight(result.Length + 8);
                result += "{\n";

                result += addThreads(thread);

                result = result.PadRight(result.Length + 8);
                result += "},\n";
            }
            result = result.TrimEnd(',', '\n') + "\n";
            result += "    ]\n}";
            return result;
        }

        //PadRight(result.Length + 8); Добавление пробелов
        //TrimEnd(',', '\n') + "\n"; Удаление последней запятой

        //Добавления потока
        public string addThreads(TThread trace)
        {
            string result = "";

            result = result.PadRight(result.Length + 12);
            result += $"\"id\": \"{trace.id}\",\n";

            result = result.PadRight(result.Length + 12);
            result += $"\"time\": \"{trace.time}\",\n";

            result = result.PadRight(result.Length + 12);
            result += $"\"methods\": [\n";

            foreach (ThreadInf method in trace.TNodeHead)
            {
                result = result.PadRight(result.Length + 16);
                result += "{\n";

                result += addMethod(method, 20);

                result = result.PadRight(result.Length + 16);
                result += "},\n";
            }
            result = result.TrimEnd(',', '\n') + "\n";
            result = result.PadRight(result.Length + 12);
            result += $"]\n";

            return result;
        }

        //Добавление метода
        public string addMethod(ThreadInf method, int step)
        {
            string result = "";

            result = result.PadRight(result.Length + step);
            result += $"\"name\": \"{method.methodName}\",\n";

            result = result.PadRight(result.Length + step);
            result += $"\"class\": \"{method.className}\",\n";

            result = result.PadRight(result.Length + step);
            result += $"\"time\": \"{method.ResultTime()}\",\n";

            result = result.PadRight(result.Length + step);
            result += $"\"methods\": [\n";

            foreach (ThreadInf child in method.TChilds)
            {
                result = result.PadRight(result.Length + step + 4);
                result += "{\n";

                result += addMethod(child, step + 8);

                result = result.PadRight(result.Length + step + 4);
                result += "},\n";
            }
            result = result.TrimEnd(',', '\n') + "\n";
            result = result.PadRight(result.Length + step);
            result += "]\n";

            return result;
        }
    }
}
