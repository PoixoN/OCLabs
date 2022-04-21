namespace OCLab1;

public class Fork
{
    public string Id;
    public bool IsUsed { get; set; } = false;

    public Fork(string id) => Id = id;

    public void Put()
    {
        IsUsed = false;
    }
}
