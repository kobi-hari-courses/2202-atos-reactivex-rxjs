using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace HigherOrderObservables
{
    internal class Program
    {
        /*
         * 1. Higher Order Observables: int[][], Observable<Observable<int>>, Func<Func<int>>, Observable<int[]>
         * 2. Cold - Hot Observables, Subjects
         * 3. Aggregation, Reduction, Scan, State Machine
         *  - Combinators
         * 
         * 
         * Flat -> High Order
         * 1. Select
         * 2. Group By
         * 3. Window, Buffer
         * 
         * High Order -> Flat
         * 1. Switch
         * 2. Merge
         * 3. Select Many
         * 4. Concat
         * 5. 
         */

        static async Task Main(string[] args)
        {
            var rnd = new Random();
            var randoms = Observable.Interval(TimeSpan.FromSeconds(1))
                .Select(_ => rnd.Next(100))
                .Do(x => Console.WriteLine($"Randoms: {x}"));

            var filtered = randoms
                .Buffer(2, 1)
                .Where(x => (x.Count == 2) && (x[1] * 1.0 > x[0] * 1.2))
                .Select(pair => $"{pair[0]} > {pair[1]}");

            filtered.Subscribe(x => Console.WriteLine($"Filtered: {x}"));

            var groups = Observable.Interval(TimeSpan.FromSeconds(1000))
                .GroupBy(i => i % 3)
                .Switch();

            var x = Observable.Interval(TimeSpan.FromSeconds(1))
                .Buffer(3)
                .SelectMany(outer => outer);




            await Task.Delay(50000);
        }
    }
}


// Input: 10, 20        , 15,       16,     30,     36,     40 ----|
// Output: --"10 > 20"--------------------"16 > 30"-"30 > 36"------|



// ---{Appl: 50, MSFT: 55, GOOGL: 30}-----{Appl: 50: MSFT: 54, GOOGL: 31}----{Appl: 60: MSFT: 45: Google: 30}


// -----------------------------------------------------------------------------{APPL: 50 -> 60}, {MSFT: 54 -> 45}