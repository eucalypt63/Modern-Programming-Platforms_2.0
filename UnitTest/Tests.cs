using Lab1.Tracer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using LConsole;
using FluentAssertions;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class Tests
    {
        TThread TestThreadRes = new TThread(1);
        MethodInf TestMethod1 = new MethodInf(1, "MethodName1", "ClassName1");
        MethodInf TestMethod2 = new MethodInf(1, "MethodName2", "ClassName2");

        [TestMethod]
        [TestInitialize]
        public void Initialize()
        {
            TestMethod1.AddNode(TestMethod2);

            TestMethod1.StartTimer();
            Thread.Sleep(100);
            TestMethod1.StopTimer();
            TestThreadRes.TNodeHead.Add(TestMethod1);

            TestMethod2.StartTimer();
            Thread.Sleep(100);
            TestMethod2.StopTimer();
            TestThreadRes.TNodeHead.Add(TestMethod2);

            TestThreadRes.getTrheadTime();
        }

        [TestMethod]
        public void TestFunc()
        {
            FuncConsol Function1 = new FuncConsol();
            Function1.Func1();
            Function1.tracer.methodList.Count.Should().Be(2);

            FuncConsol Function2 = new FuncConsol();
            Function2.Func2();
            Function2.tracer.methodList.Count.Should().Be(1);

            FuncConsol Function3 = new FuncConsol();
            Function3.Func3(2);
            Function3.tracer.methodList.Count.Should().Be(5);
        }

        [TestMethod]
        public void TestGetTrheadTime()
        {
            TestThreadRes.time.Should().BeInRange(200, 240);
        }

        [TestMethod]
        public void TestGetHead()
        {
            TestMethod2.GetHead().Should().Be(TestMethod1);
        }

        [TestMethod]
        public void TestGetResult()
        {
            List<MethodInf> threadList = new List<MethodInf>{
                TestMethod1,
                TestMethod2
            };

            TraceResult result = new TraceResult();
            result.getThreadList(threadList);

            result.threads.Count.Should().Be(1);
        }
    }
}
