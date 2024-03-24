using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab1.Tracer
{
    public class TThread
    {
        public List<ThreadInf> TNodeHead = new List<ThreadInf>();
        public int id { get; }
        public long time { get; set; }

        public TThread(int id)
        {
            this.id = id;
        }

        public void getTrheadTime()
        {
            long Ttime = 0;
            foreach (ThreadInf node in TNodeHead)
            {
                Ttime += node.ResultTime();
            }
            time = Ttime;
        }
    }

    public class TraceResult
    {
        public List<TThread> trace = new List<TThread>();
        public void getTraceResult(List<ThreadInf> nodeList)
        {
            foreach (ThreadInf node in nodeList)
            {
                ThreadInf Head = node.GetHead();
                var targetThread = trace.FirstOrDefault(t => t.id == Head.threadId);
                if (targetThread != null)
                {
                    if (!targetThread.TNodeHead.Contains(Head))
                        targetThread.TNodeHead.Add(Head);
                }
                else
                {
                    TThread Ttrace = new TThread(Head.threadId);
                    Ttrace.TNodeHead.Add(Head);
                    trace.Add(Ttrace);
                }
            }

            foreach (TThread node in trace)
            {
                node.getTrheadTime();
            }
        }


        // Содержит функции серелизации json

        // Содержит функции серелизации xml
    }
}
