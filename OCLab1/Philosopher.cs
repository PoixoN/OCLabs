namespace OCLab1;

public class Philosopher
{
    public string Id;
    public Fork LeftFork { get; set; }
    public Fork RightFork { get; set; }

    public Philosopher(string id) => Id = id;

    public void TakeForks(Fork leftFork, Fork rightFork)
    {
        LeftFork = leftFork;
        RightFork = rightFork;
    }

    public void PutLeftFork()
    {
        LeftFork.Put();
    }
    public void PutRightFork()
    {
        RightFork.Put();
    }
    public void StartEating()
    {
        Console.WriteLine($"Philosopher {Id} has started eating");
    }

    public void StartThinking()
    {
        Console.WriteLine($"Philosopher {Id} has started thinking");
    }
}


