namespace OCLab41;

class Program
{
    public static int[,] MatrixNM;
    public static int[,] MatrixMK;
    public static int[,] MatrixNK;
    private static Random _rand = new Random();

    private static int _n = 3;
    private static int _m = 2;
    private static int _k = 1;

    static void Main(string[] args)
    {
        Thread[] threads = new Thread[_n * _k];
        Node pos;
        MatrixNM = new int[_n, _m];
        MatrixMK = new int[_m, _k];
        MatrixNK = new int[_n, _k];

        MatrixNM = GenerateMatrix(_n, _m);
        MatrixMK = GenerateMatrix(_m, _k);

        //MatrixNM = FillMatrix(_n, _m);
        //MatrixMK = FillMatrix(_m, _k);

        Console.WriteLine("A:");
        PrintMatrix(MatrixNM, _n, _m);
        Console.WriteLine();
        Console.WriteLine("B:");
        PrintMatrix(MatrixMK, _m, _k);
        Console.WriteLine();

        int p = 0;
        for (int i = 0; i < _n * _k; i++)
        {
            threads[i] = new Thread(new ParameterizedThreadStart(Mult));

        }
        for (int i = 0; i < _n; i++)
        {
            for (int j = 0; j < _k; j++)
            {
                pos = new Node(i, j);
                threads[p].Start(pos);
                p++;
            }
        }

        Thread.Sleep(200);
        //PrintMatrix(MatrixNK, _n, _k);
    }


    public static void Mult(object p)
    {
        Node pos = (Node)p;
        int res = 0;
        int r = pos.N;
        int c = pos.K;

        for (int i = 0; i < _m; i++)
            res += MatrixNM[r, i] * MatrixMK[i, c];

        MatrixNK[r, c] = res;

        Console.WriteLine(res + "  ");
    }


    public static void PrintMatrix(int[,] Matrix, int n, int m)
    {
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
                Console.Write(Matrix[i, j] + "  ");
            Console.WriteLine();
    }

    public static int[,] GenerateMatrix(int n, int m)
    {
        var res = new int[n, m];

        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
                res[i, j] = _rand.Next(10);

        return res;
    }

    private static int[,] FillMatrix(int n, int m)
    {
        var res = new int[n, m];

        for (int i = 0; i < _n; i++)
            for (int j = 0; j < _m; j++)
                res[i, j] = Convert.ToInt32(Console.ReadLine());

        return res;
    }
}
