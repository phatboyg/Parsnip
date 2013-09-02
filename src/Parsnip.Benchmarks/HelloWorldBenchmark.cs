namespace Parsnip.Benchmarks
{
    using System.Collections.Generic;
    using Benchmarque;


    public class HelloWorldBenchmark :
        Benchmark<StringParsing>
    {
        const string Subject = "Hello, World.\r\nHow are you?";

        public void WarmUp(StringParsing instance)
        {
            instance.Parse(Subject);
        }

        public void Shutdown(StringParsing instance)
        {
        }

        public void Run(StringParsing instance, int iterationCount)
        {
            for (int i = 0; i < iterationCount; i++)
                instance.Parse(Subject);
        }

        public IEnumerable<int> Iterations
        {
            get
            {
                yield return 10;
                yield return 20000;
            }
        }
    }
}