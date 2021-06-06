using System;
using System.Diagnostics;

namespace Otus10
{
    public static class FuncElapser
    {
        public static ElapseResult<T> ElapseWithResult<T>(Func<T> func)
        {
            var sw = new Stopwatch();

            sw.Start();
            var result = func();
            sw.Stop();

            return new ElapseResult<T>
            {
                Elapsed = sw.Elapsed,
                Result = result
            };
        }
    }
}
