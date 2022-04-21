namespace OCLab1;

public static class Meeting
{
    private static readonly object _locker = new object();

    private static readonly List<Fork> _forks = new List<Fork>();
    public static List<Philosopher> Philosophers { get; private set; } = new List<Philosopher>();


    public static void Start()
    {
        if (!Philosophers.Any())
            throw new ArgumentNullException("There are no philosophers!");

        InitForks();
        GiveForks();

        foreach (var Philosopher in Philosophers)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(Manager));
            thread.Start(Philosopher);
        }
    }

    public static void SetPhilosophers(List<Philosopher> philosophers)
    {
        Philosophers = philosophers;
    }

    private static void InitForks()
    {
        foreach (var id in Philosophers.Select(x => x.Id))
            _forks.Add(new($"{id}"));
    }

    private static void GiveForks()
    {
        var count = Philosophers.Count;

        for (int i = 0; i < count;)
        {
            var philosopher = Philosophers[i];
            var leftFork = _forks[i];
            i++;
            var rightFork = _forks[i == count ? 1 : i];

            philosopher.TakeForks(leftFork, rightFork);
        }
    }

    static public void Manager(object obj)
    {
        var philosopher = obj as Philosopher;

        if (philosopher == null)
            throw new ArgumentNullException("Philosopher is null");

        var rand = new Random();

        while (!Console.KeyAvailable)
        {
            if (!philosopher.RightFork.IsUsed)
            {
                lock (_locker)
                    philosopher.RightFork.IsUsed = true;

                if (!philosopher.LeftFork.IsUsed)
                {
                    lock (_locker)
                        philosopher.LeftFork.IsUsed = true;

                    Eat(philosopher, rand);
                    philosopher.PutRightFork();
                    philosopher.PutLeftFork();
                    Think(philosopher, rand);
                }
                else
                {
                    philosopher.PutRightFork();
                    Think(philosopher, rand);
                }
            }

        }
        Environment.Exit(0);

        void Eat(Philosopher philosopher, Random rand) { philosopher.StartEating(); Thread.Sleep(rand.Next(1000, 5000)); };
        void Think(Philosopher philosopher, Random rand) { philosopher.StartThinking(); Thread.Sleep(rand.Next(7000, 10000)); };
    }
}
