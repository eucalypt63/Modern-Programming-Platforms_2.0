using System.Collections.Generic;
using System.Linq;

namespace Lab1.Tracer
{
    public class TThread
    {
        public List<MethodInf> TNodeHead = new List<MethodInf>(); //Лист корневых узлов
        public int id { get; }
        public long time { get; set; }
        public TThread(int id)
        {
            this.id = id;
        }
        
        //Получение времени работы потока
        public void getTrheadTime()
        {
            long Ttime = 0;
            foreach (MethodInf node in TNodeHead)
            {
                Ttime += node.ResultTime();
            }
            time = Ttime;
        }
    }
    public class TraceResult
    {
        public List<TThread> threads = new List<TThread>();

        //Получение листа потоков
        public void getThreadList(List<MethodInf> nodeList)
        {
            foreach (MethodInf node in nodeList)
            {
                MethodInf Head = node.GetHead();
                var targetThread = threads.FirstOrDefault(t => t.id == Head.threadId);
                if (targetThread != null)
                {
                    if (!targetThread.TNodeHead.Contains(Head))
                        targetThread.TNodeHead.Add(Head);
                }
                else
                {
                    TThread Ttrace = new TThread(Head.threadId);
                    Ttrace.TNodeHead.Add(Head);
                    threads.Add(Ttrace);
                }
            }

            foreach (TThread node in threads)
            {
                node.getTrheadTime();
            }
        }
    }
}
