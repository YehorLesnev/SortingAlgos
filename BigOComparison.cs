using SortingAlgos.Algos;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        const int N = 10_000;
        var rnd = new Random();
        var data = new int[N];
        for (int i = 0; i < N; i++) data[i] = rnd.Next(N);

        var sw = new Stopwatch();

        Console.WriteLine($"N = {N}\n");

        // O(1)
        sw.Start();
        int last = data[N - 1];
        sw.Stop();
        Console.WriteLine($"O(1)   ConstantOperation → took {sw.ElapsedTicks} ticks (last element = {last})");

        // O(n)
        sw.Restart();
        long sum = LinearOperation(data);
        sw.Stop();
        Console.WriteLine($"O(n)   LinearOperation  → took {sw.ElapsedTicks} ticks (sum = {sum})");

        // O(log n)
        Array.Sort(data);
        sw.Restart();
        int target = data[N / 2];
        int idx = BinarySearch(data, target);
        sw.Stop();
        Console.WriteLine($"O(log n) BinarySearch   → took {sw.ElapsedTicks} ticks (found at {idx})");

        // O(n^2)
        var copy1 = (int[])data.Clone();
        sw.Restart();
        BubbleSort.Sort(copy1);
        sw.Stop();
        Console.WriteLine($"O(n^2)  BubbleSort       → took {sw.ElapsedMilliseconds} ms");

        // O(n log n) – QuickSort
        var copy2 = (int[])data.Clone();
        sw.Restart();
        QuickSort.Sort(copy2, 0, copy2.Length - 1);
        sw.Stop();
        Console.WriteLine($"O(n log n) QuickSort   → took {sw.ElapsedMilliseconds} ms");

        // O(n log n) – MergeSort
        var copy3 = (int[])data.Clone();
        sw.Restart();
        MergeSort.Sort(copy3, 0, copy3.Length - 1);
        sw.Stop();
        Console.WriteLine($"O(n log n) MergeSort   → took {sw.ElapsedMilliseconds} ms");

        // O(2^n)
        const int F = 30;
        sw.Restart();
        int fib = ExponentialOperation(F);
        sw.Stop();
        Console.WriteLine($"O(2ⁿ)  Exponential({F})  → took {sw.ElapsedMilliseconds} ms (value = {fib})");
    }

    static long LinearOperation(int[] arr)
    {
        long s = 0;
        foreach (var v in arr) s += v;
        return s;
    }

    // O(log n)
    static int BinarySearch(int[] arr, int target)
    {
        int left = 0, right = arr.Length - 1;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (arr[mid] == target) return mid;
            if (arr[mid] < target) left = mid + 1;
            else right = mid - 1;
        }
        return -1;
    }

    // O(2^n)
    static int ExponentialOperation(int n)
    {
        if (n < 2) return n;
        return ExponentialOperation(n - 1) + ExponentialOperation(n - 2);
    }
}
