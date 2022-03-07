using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace SolutionToExercise
{
    internal class Program
    {
        private static string[] _companies = new string[] { "Appl", "Gogl", "Amzn", "Msft", "Fcbk" };
        private static Random _random = new Random();

        public static ImmutableDictionary<string, int> CreateInitialStock()
        {
            return _companies
                .Select(name => (name: name, value: _random.Next(100, 200)))
                .ToImmutableDictionary(pair => pair.name, pair => pair.value);
        }

        public static ImmutableDictionary<string, int> CreateNextStock(ImmutableDictionary<string, int> currentState)
        {
            var nameToChange = _companies[_random.Next(_companies.Length)];
            var newValue = currentState[nameToChange] + _random.Next(-50, 50);

            return currentState.SetItem(nameToChange, newValue);
        }

        public static IObservable<ImmutableDictionary<string, int>> CreateStockMarketObservable()
        {
            return Observable.Create<ImmutableDictionary<string, int>>(async observer =>
            {
                var currentStock = CreateInitialStock();
                observer.OnNext(currentStock);
                while(true)
                {
                    await Task.Delay(1000);
                    currentStock = CreateNextStock(currentStock);
                    observer.OnNext(currentStock);
                }
            });
        }

        static async Task Main(string[] args)
        {
            var markets = CreateStockMarketObservable()
                .Do(val => Console.WriteLine("A: " + string.Join(", ", val.Select(pair => (pair.Key, pair.Value)))));


            var solution = markets
                .SelectMany(val => val)
                //.Do(val => Console.WriteLine("B: " + val.ToString()))
                .GroupBy(kvp => kvp.Key)
                .Select(group => group
                    .Buffer(2, 1)
                    .Where(list => list.Count == 2)
                    .Select(list => (
                                name: list[0].Key, 
                                oldValue: list[0].Value, 
                                newValue: list[1].Value, 
                                diff: list[1].Value - list[0].Value,
                                ratio: (list[1].Value - list[0].Value)/((double)list[0].Value)))
                    //.Do(val => Console.WriteLine("C: " + val))
                    .Where(tpl => Math.Abs(tpl.ratio) > 0.1))
                .Merge()
                .Do(val => Console.WriteLine("NOTIFICATION: " + val.ToString()));


            solution.Subscribe();


            await Task.Delay(150000);
        }
    }
}
