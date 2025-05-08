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

static List<long> Evaluate(IHashTable<string, int>[] hashTables)
{
    PopulateHashTable<string, int>(hashTables[0], 100);
    PopulateHashTable<string, int>(hashTables[1], 10000);
    PopulateHashTable<string, int>(hashTables[2], 1000000);
    Stopwatch stopwatch = new Stopwatch();
    Random random = new();
    List<long> values = [];

    for (int i = 0; i < 10; i++)
    {
        // N = 100  
        var key = random.Next(0, 100).ToString();
        stopwatch.Start();
        hashTables[0].Find(key);
        stopwatch.Stop();
        values.Add(stopwatch.ElapsedTicks);
        Console.WriteLine($"Time taken to find {key} in hash table with 100 elements: {stopwatch.ElapsedTicks}00 ns");

        // N = 10000
        key = random.Next(0, 10000).ToString();
        stopwatch.Restart();
        hashTables[1].Find(key);
        stopwatch.Stop();
        values.Add(stopwatch.ElapsedTicks);
        Console.WriteLine($"Time taken to find {key} in hash table with 10000 elements: {stopwatch.ElapsedTicks}00 ns");

        // N = 1000000
        key = random.Next(0, 1000000).ToString();
        stopwatch.Restart();
        hashTables[2].Find(key);
        stopwatch.Stop();
        values.Add(stopwatch.ElapsedTicks);
        Console.WriteLine($"Time taken to find {key} in hash table with 1000000 elements: {stopwatch.ElapsedTicks}00 ns");
    }

    return values;
}

List<long> evaluated;
HashTableLinear<string, int>[] hashTablesLinear = new HashTableLinear<string, int>[3];
HashTableQuadratic<string, int>[] hashTablesQuadratic = new HashTableQuadratic<string, int>[3];

foreach (var i in Enumerable.Range(0, 3))
{
    hashTablesLinear[i] = new HashTableLinear<string, int>(i == 0 ? 100 : i == 1 ? 10000 : 1000000);
    hashTablesQuadratic[i] = new HashTableQuadratic<string, int>(i == 0 ? 100 : i == 1 ? 10000 : 1000000);
}

Console.WriteLine("HashTableLinear:");
evaluated = Evaluate(hashTablesLinear);
Plotting.Plot(evaluated, "Linear");
Console.WriteLine("HashTableQuadratic:");
evaluated = Evaluate(hashTablesQuadratic);
Plotting.Plot(evaluated, "Quadratic");