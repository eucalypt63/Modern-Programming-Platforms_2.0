using Lab1.Tracer;
using System.Threading;

namespace LConsole
{
    public class FuncConsol
    {
        public readonly TracerImpl tracer = new TracerImpl();
        public void Func1()
        {
            tracer.startTrace();

            Thread.Sleep(100);
            Func2();

            tracer.stopTrace();
        }

        public void Func2()
        {
            tracer.startTrace();

            Thread.Sleep(200);

            tracer.stopTrace();
        }

        public void Func3(int n)
        {
            tracer.startTrace();

            Thread.Sleep(50);
            if (n == 1)
                Func1();

            if (n != 0)
                Func3(--n);

            tracer.stopTrace();
        }

    }
}
