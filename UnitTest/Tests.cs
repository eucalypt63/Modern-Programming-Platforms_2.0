using Lab1.Tracer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using LConsole;
using FluentAssertions;

namespace UnitTest
{
    [TestClass]
    public class Tests
    {
        TThread TestThreadRes = new TThread(1);
        ThreadInf TestMethod1 = new ThreadInf(1, "MethodName1", "ClassName1");
        ThreadInf TestMethod2 = new ThreadInf(1, "MethodName2", "ClassName2");

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
            Function1.tracer.threadList.Count.Should().Be(2);

            FuncConsol Function2 = new FuncConsol();
            Function2.Func2();
            Function2.tracer.threadList.Count.Should().Be(1);

            FuncConsol Function3 = new FuncConsol();
            Function3.Func3(2);
            Function3.tracer.threadList.Count.Should().Be(5);
        }

        [TestMethod]
        public void TestGetTrheadTime()
        {
            TestThreadRes.time.Should().BeGreaterThan(200);
            TestThreadRes.time.Should().BeLessThan(240);
        }

        [TestMethod]
        public void TestGetHead()
        {
            TestMethod2.GetHead().Should().Be(TestMethod1);
        }
    }
}
