using OCLab2CSharp;
using System.IO.MemoryMappedFiles;

var _ = new EventWaitHandle(false, EventResetMode.AutoReset, "QueueHandler");

StreamReader input = new StreamReader(@"C:\Users\LiubomyrMaievskyi\Desktop\Education\3 курс 2 семестр\My\ОС\OCLab1\OCLab2CSharp\input.txt");
int.TryParse(input.ReadLine(), out int x);
input.Close();

var resultG = G(x);
var resultF = F(x);
bool result = resultF.Result | resultG.Result;

Console.WriteLine($"Result is {result}");

StreamWriter output = new StreamWriter(@"C:\Users\LiubomyrMaievskyi\Desktop\Education\3 курс 2 семестр\My\ОС\OCLab1\OCLab2CSharp\output.txt");
output.WriteLine(result);
output.Close();

async Task<bool> F(int x) 
{
    Console.WriteLine("Start F");
    var qHandler = EventWaitHandle.OpenExisting("QueueHandler"); ;
    var messageBrokerQ = MemoryMappedFile.CreateOrOpen("MessageBrokerQueue", 68, MemoryMappedFileAccess.ReadWrite);
    var accessor = messageBrokerQ.CreateViewAccessor();

    MyMessage message = new()
    {
        Value = x + x
    };

    accessor.Write(0, ref message);

    Console.WriteLine($"Sent {message.Value}");

    qHandler.Set();

    Console.WriteLine("End F");

    return x > 0; 
}

async Task<bool> G(int x) 
{
    Console.WriteLine("Start G");

    var messageBrokerQ = MemoryMappedFile.CreateOrOpen("MessageBrokerQueue", 68, MemoryMappedFileAccess.ReadWrite);
    var accessor = messageBrokerQ.CreateViewAccessor();
    var qHandler = EventWaitHandle.OpenExisting("QueueHandler");

    int cnt = 0;

    while (true)
    {
        Console.WriteLine("While G");

        bool hasMessage = qHandler.WaitOne(100);

        if (hasMessage)
        {
            Console.WriteLine("IF G");

            accessor.Read(0, out MyMessage message);

            Console.WriteLine($"Received {message.Value}");

            Console.WriteLine("End G");

            return x - message.Value > 0;
        }
        else
        {
            Console.WriteLine("ELSE Start G");

            if (cnt == 10)
            {
                Console.WriteLine("ELSE Choice G");

                Console.WriteLine("So long. Maybe do you wanna stop the process?");

                Console.Write("Please use Escape to exit");

                bool exit = Console.ReadKey().Key == ConsoleKey.Escape;

                if (exit)
                    return false;
            }

            cnt++;
            await Task.Delay(400);

            Console.WriteLine("ELSE Ende G");
        }
    }
}
