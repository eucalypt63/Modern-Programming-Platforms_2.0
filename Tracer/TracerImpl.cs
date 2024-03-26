using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;


namespace Lab1.Tracer
{
    public class TracerImpl : ITracer
    {
        public List<ThreadInf> threadList = new List<ThreadInf>();

        public void startTrace()
        {
            //Получение id потока
            var threadId = Environment.CurrentManagedThreadId;

            var stackTrace = new StackTrace(); //Получение информации о вызывающем методе
            var callingMethodName = stackTrace.GetFrame(1).GetMethod().Name; //Получить имя метода

            //Получить имя класса
            var callingMethod = stackTrace.GetFrame(1).GetMethod();
            var callingClassName = callingMethod.DeclaringType.Name;

            //Создание класса обрабатываемого метода
            var newThread = new ThreadInf(threadId, callingMethodName, callingClassName);

            var targetThread = threadList.FirstOrDefault(t => t.threadId == threadId && t.GetHead().isActive == true);
            if (targetThread != null)
            {
                targetThread.AddNode(newThread);
            }
            threadList.Add(newThread);
            newThread.StartTimer();
        }

        public void stopTrace()
        {
            //Получение id потока
            var threadId = Environment.CurrentManagedThreadId;

            var stackTrace = new StackTrace(); //Получение информации о вызывающем методе
            var callingMethodName = stackTrace.GetFrame(1).GetMethod().Name; //Получить имя метода

            //Получить имя класса
            var callingMethod = stackTrace.GetFrame(1).GetMethod();
            var callingClassName = callingMethod.DeclaringType.Name;

            //Поиск обрабатываемого мтеода
            var targetThread = threadList.FirstOrDefault(t =>
              t.threadId == threadId &&
              t.methodName == callingMethodName &&
              t.className == callingClassName &&
              t.GetHead().isActive == true);

            targetThread.StopTimer();
        }

        //Доработать клласс и создать функции для вывода результата
        public TraceResult getTraceResult()
        {
            var trace = new TraceResult();
            trace.getThreadList(threadList);
            return trace;
        }
    }
}
