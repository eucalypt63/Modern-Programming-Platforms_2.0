using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;


namespace Lab1.Tracer
{
    public class TracerImpl : ITracer
    {
        public List<MethodInf> methodList = new List<MethodInf>();

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
            var newMethod = new MethodInf(threadId, callingMethodName, callingClassName);

            var targetMethod = methodList.FirstOrDefault(t => t.threadId == threadId && t.isActive == true);
            if (targetMethod != null)
            {
                targetMethod.AddNode(newMethod);
            }
            methodList.Add(newMethod);
            newMethod.StartTimer();
        }

        public void stopTrace()
        {
            //Получение id потока
            var threadId = Environment.CurrentManagedThreadId;

            //Поиск обрабатываемого мтеода
            var targetMethod = methodList.FirstOrDefault(t => t.threadId == threadId && t.isActive == true);

            targetMethod.StopTimer();
        }

        //Доработать клласс и создать функции для вывода результата
        public TraceResult getTraceResult()
        {
            var trace = new TraceResult();
            trace.getThreadList(methodList);
            return trace;
        }
    }
}
