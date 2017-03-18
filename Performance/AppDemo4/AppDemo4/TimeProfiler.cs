using System;
using System.Diagnostics;

namespace AppDemo4
{
    public class TimeProfiler : IDisposable
    {
        private readonly string _id;
        private readonly Stopwatch _watch;

        public TimeProfiler(string id)
        {
            _id = id;
            _watch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            var elapsedMs = _watch.ElapsedMilliseconds;
            Debug.WriteLine($"{_id} took {elapsedMs}ms");
        }
    }
}