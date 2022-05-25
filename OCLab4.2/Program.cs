using System.Diagnostics;

namespace OCLab42;

class Program
{
    private const bool _withLogging = true;
    private const bool _withLocker = true;
    private const int _numIter = 100000;
    private const int _threadsNum = 100;

    private static object _locker = new object();
    private static readonly Stopwatch _sw = new Stopwatch();

    static void Main()
    {
        Thread[] threads = new Thread[_threadsNum];

        _sw.Start();
        Console.WriteLine($"Start");

        for (int i = 0; i < _threadsNum; i++)
            if (!_withLocker)
                threads[i] = new Thread(new ParameterizedThreadStart(F));
            else
                threads[i] = new Thread(new ParameterizedThreadStart(FWithLocker));

        for (int i = 0; i < _threadsNum; i++)
            threads[i].Start(0);

        for (int i = 0; i < _threadsNum; i++)
            threads[i].Join();

        _sw.Stop();
        TimeSpan time = _sw.Elapsed;

        Console.WriteLine($"In general It has taken time: {time.Seconds}:{time.Milliseconds / 10}");
    }
    public static void F(object obj)
    {
        int x = (int)obj;
        for (int i = 0; i < _numIter; i++, x++)
            if (_withLogging)
                Console.WriteLine($"Thread Id: {Thread.CurrentThread.ManagedThreadId}, X: {x}");
    }

    public static void FWithLocker(object obj)
    {
        int x = (int)obj;
        lock (_locker)
            for (int i = 0; i < _numIter; i++, x++)
                if (_withLogging)
                    Console.WriteLine($"Thread Id: {Thread.CurrentThread.ManagedThreadId}, X: {x}");
    }
}

