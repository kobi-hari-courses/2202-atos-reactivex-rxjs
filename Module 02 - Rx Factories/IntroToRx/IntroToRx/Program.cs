using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace IntroToRx
{
    // reactive factories
    // 1. Observable.Return(val)       -[val]-|
    // 2. Observable.Empty             -------|    
    // 3. Observable.Never             ----------
    // 4. Observable.Throw(err)        -----X(err)

    // 5. Observable.Create

    // 6. Observable.Range(2, 3)       --2-3-4-|
    // 7. Observable.Generate

    // 8. Observable.Interval(----)     ----1----2-----3----4----...
    // 9. Observable.Timer(---)         ---0|
    //    Observable.Timer(---, -)      ---0-1-2-3-4-5-6-7....

    // Converters
    // 1. Observable.Start(Func or Action). Warm observable
    //    Observable.Start(() => 42)   --42|
    // 2. Observable.StartASync(() => -----42)  -------42|
    // 3. Observble.FromEventPattern . Deferred Hot observable
    // 4. Observable.FromEvent
    // 5. Task.ToObservable()   --42  =>    --42|
    // 6. IEnumerable.ToObservable()





    class Program
    {
        public static int MyFunction()
        {
            Console.WriteLine("Hello World");
            return 42;
        }

        public static IEnumerable<int> CreateEnumerable()
        {
            var random = new Random();
            Console.WriteLine($"{DateTime.Now} Starting enumerable");
            yield return 20;
            yield return 30;
            yield return random.Next(50, 100);
            for (int i = 40; i < 60; i+=3)
            {
                yield return i;
            }
            Console.WriteLine($"{DateTime.Now} Ending enumerable");
        }

        public static async IAsyncEnumerable<int> CreateAsyncEnumerable()
        {
            var random = new Random();
            Console.WriteLine($"{DateTime.Now} Starting enumerable");
            yield return 20;

            await Task.Delay(1000);

            yield return 30;
            yield return random.Next(50, 100);
            for (int i = 40; i < 60; i += 3)
            {
                await Task.Delay(1000);
                yield return i;
            }
            Console.WriteLine($"{DateTime.Now} Ending enumerable");

        }

        public static async Task<int> MyAsyncFunction()
        {
            Console.WriteLine($"{DateTime.Now} My async function started");
            await Task.Delay(3000);
            Console.WriteLine($"{DateTime.Now} My async function completed");
            return 42;
        }

        public static void MyAction()
        {
            Console.WriteLine("Hello Universe");
        }

        static async Task Main(string[] args)
        {
            //var observable = Observable.Empty<int>();

            //var observable = Observable.Generate(
            //    initialState: (i: 1, j: 1), 
            //    condition: tpl => tpl.j < 100, 
            //    iterate: tpl => (i: tpl.j, j: tpl.i + tpl.j),
            //    resultSelector: tpl => tpl.i
            //    );

            //var observable = Observable.Interval(TimeSpan.FromSeconds(1));

            //var observable = Observable.Timer(TimeSpan.FromSeconds(2), 
            //                                  TimeSpan.FromSeconds(1));

            //Console.WriteLine($"{DateTime.Now} Here we go");

            //var observable = Observable.StartAsync(MyAsyncFunction);

            //var task = Task.Delay(2000).ContinueWith(t => 42);
            //var observable = task.ToObservable();

            //var observable = CreateEnumerable().ToObservable();

            var observable = CreateAsyncEnumerable().ToObservable();

            var observer1 = new ConsoleObserver<int>("First", ConsoleColor.White);
            var observer2 = new ConsoleObserver<int>("Second", ConsoleColor.Green);

            await Task.Delay(1000);
            Console.WriteLine($"{DateTime.Now} Subscription 1");
            observable.Subscribe(observer1);

            await Task.Delay(3000);

            Console.WriteLine($"{DateTime.Now} Subscription 2");
            var subscription = observable.Subscribe(observer2);

            await Task.Delay(4000);

            subscription.Dispose();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Disposed second subscription");

            await Task.Delay(10000);

        }
    }
}


/*

Reactive Operators

    | Factories      | Pipeable Operators | Combinators           |
    +----------------+--------------------+-----------------------+
    | 0 -> O$        |    O$ -> O$        |  Many O$ => O$        |



*/