using System.Collections.Generic;
using System.Diagnostics;

namespace Lab1.Tracer
{
    public class MethodInf
    {
        public MethodInf TParent = null;
        public List<MethodInf> TChilds = new List<MethodInf>();
        public int threadId { get; set; }
        public string methodName { get; set; }
        public string className { get; set; }
        public Stopwatch stopwatch = new Stopwatch();
        public bool isActive;

        public MethodInf(int id, string MethodName, string ClassName)
        {
            threadId = id;
            methodName = MethodName;
            className = ClassName;
        }

        //Запустить таймер
        public void StartTimer()
        {
            isActive = true;
            stopwatch.Start();
        }

        //Остановить таймер
        public void StopTimer()
        {
            bool flag = true;
            MethodInf curNode = this;
            foreach (MethodInf child in curNode.TChilds)
            {
                if (child.isActive)
                {
                    flag = false;
                    child.StopTimer();
                    break;
                }
            }

            if (flag)
            {
                isActive = false;
                stopwatch.Stop();
            }
        }

        //Получить результат таймера
        public long ResultTime()
        {
            return stopwatch.ElapsedMilliseconds;
        }

        //Добавления узла
        public void AddNode(MethodInf node)
        {
            MethodInf curNode = this;
            bool flag = false;
            while (!flag)
            {
                flag = true;
                foreach (MethodInf child in curNode.TChilds)
                {
                    if (child.isActive)
                    {
                        flag = false;
                        curNode = child;
                        break;
                    }
                }
            }
            curNode.TChilds.Add(node);
            node.TParent = curNode;
        }

        //Получить корневой узел
        public MethodInf GetHead()
        {
            MethodInf Head = this;
            while (Head.TParent != null)
            {
                Head = Head.TParent;
            }

            return Head;
        }

    }
}
