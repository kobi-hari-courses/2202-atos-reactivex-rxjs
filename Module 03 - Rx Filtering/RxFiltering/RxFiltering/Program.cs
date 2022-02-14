using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace RxFiltering
{
    internal class Program
    {
        static IEnumerable<int> GetSomeValues()
        {
            "Get some values started".Print(ConsoleColor.Gray);

            yield return 12;
            yield return 42;
            yield return -10;
            yield return 50;
            yield return 120;
            yield return 42;
            yield return 30;
            yield return 30;
            yield return 50;
        }

        static async IAsyncEnumerable<int> GetSomeValuesAsync()
        {
            foreach (var item in GetSomeValues())
            {
                await Task.Delay(1000);
                yield return item;
            }
        }

        static async Task Main(string[] args)
        {
            var observable = GetSomeValuesAsync().ToObservable();

            var observableControl = Observable.Timer(TimeSpan.FromSeconds(3.5));


            var where = observable.Where(n => n >= 20);
            var distinct = observable.Distinct();
            var distinctUntilChanges = observable.DistinctUntilChanged();
            var ignoreElements = observable.IgnoreElements();

            observable.SubscribeToConsole("source", ConsoleColor.White);
            //where.SubscribeToConsole("where", ConsoleColor.Green);
            //distinct.SubscribeToConsole("distinct", ConsoleColor.Yellow);
            //distinctUntilChanges.SubscribeToConsole("distinct Until Changed", ConsoleColor.Magenta);
            //ignoreElements.SubscribeToConsole("ignore elements", ConsoleColor.Red);

            // Take, Skip
            var take = observable.Take(2);
            var skip = observable.Skip(3);
            var takeWhile = observable.TakeWhile(x => x < 100);
            var skipWhile = observable.SkipWhile(x => x < 100);
            var takeLast = observable.TakeLast(3);
            var skipLast = observable.SkipLast(3);

            var takeUntil = observable.TakeUntil(observableControl);
            var skipUntil = observable.SkipUntil(observableControl);

            take.SubscribeToConsole("take 2", ConsoleColor.Cyan);
            skip.SubscribeToConsole("skip 3", ConsoleColor.Yellow);
            takeWhile.SubscribeToConsole("takeWhile X < 100", ConsoleColor.Red);
            skipWhile.SubscribeToConsole("skipWhile X < 100", ConsoleColor.Green);
            takeLast.SubscribeToConsole("takeLast 3", ConsoleColor.Blue);
            skipLast.SubscribeToConsole("skipLast 3", ConsoleColor.Magenta);

            observableControl.SubscribeToConsole("control", ConsoleColor.Cyan);
            takeUntil.SubscribeToConsole("take Until Control", ConsoleColor.DarkCyan);
            skipUntil.SubscribeToConsole("skipUntil Control", ConsoleColor.DarkCyan);




            await observable;

            await Task.Delay(1000);
        }
    }
}
