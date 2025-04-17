using Lab6;
using System.Diagnostics;
static void PopulateHashTable<TKey, TValue>(IHashTable<string, int> hashTable, int count)
{
    if (hashTable == null) throw new ArgumentNullException(nameof(hashTable));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count), "Count must be non-negative.");

    for (int i = 0; i < count; i++)
    {
        hashTable.Add($"{i}", i);
    }
}

static long[] Evaluate(Type type)
{
    Stopwatch stopwatch = new Stopwatch();
    long[] values = new long[3];

    var table = (IHashTable<string, int>)Activator.CreateInstance(type, 100);
    PopulateHashTable<string, int>(table, 100);

    for (int i = 0; i < 5; i++)
    {
        table.Find("somekey"); // warm-up
    }

    // N = 100  
    stopwatch.Start();
    table.Find("56");
    stopwatch.Stop();
    values[0] = stopwatch.ElapsedTicks;
    Console.WriteLine($"Time taken to find 56 in hash table with 100 elements: {stopwatch.ElapsedTicks}00 ns");

    // N = 10000  
    table = (IHashTable<string, int>)Activator.CreateInstance(type, 10000);
    PopulateHashTable<string, int>(table, 10000);
    stopwatch.Restart();
    table.Find("1234");
    stopwatch.Stop();
    values[1] = stopwatch.ElapsedTicks;
    Console.WriteLine($"Time taken to find 1234 in hash table with 10000 elements: {stopwatch.ElapsedTicks}00 ns");

    // N = 1000000  
    table = (IHashTable<string, int>)Activator.CreateInstance(type, 1000000);
    PopulateHashTable<string, int>(table, 1000000);
    stopwatch.Restart();
    table.Find("12345");
    stopwatch.Stop();
    values[2] = stopwatch.ElapsedTicks;
    Console.WriteLine($"Time taken to find 12345 in hash table with 1000000 elements: {stopwatch.ElapsedTicks}00 ns");

    return values;
}

long[] evaluated;

Evaluate(typeof(HashTableLinear<string, int>));

Console.WriteLine("HashTableLinear:");
evaluated = Evaluate(typeof(HashTableLinear<string, int>));
Plotting.Plot(evaluated[0], evaluated[1], evaluated[2], "Linear");
Console.WriteLine("HashTableQuadratic:");
evaluated = Evaluate(typeof(HashTableQuadratic<string, int>));
Plotting.Plot(evaluated[0], evaluated[1], evaluated[2], "Quadratic");