global using OCLab1;

List<Philosopher> philosophers = new()
{
    new("1"),
    new("2"),
    new("3"),
    new("4"),
    new("5")
};

Meeting.SetPhilosophers(philosophers);
Meeting.Start();