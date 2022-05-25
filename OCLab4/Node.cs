namespace OCLab41;

class Node
{
    private int n;

    public int N
    {
        get { return n; }
        set { n = value; }
    }

    private int k;

    public int K
    {
        get { return k; }
        set { k = value; }
    }


    public Node(int n, int k)
    {
        N = n;
        K = k;
    }

}