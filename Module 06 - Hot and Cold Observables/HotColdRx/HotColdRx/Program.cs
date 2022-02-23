using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace HotColdRx
{
    internal class Program
    {
        public static string ConvertToString<T>(IEnumerable<T> source) 
        {
            return string.Join(", ", source);
        }

        public static IObservable<int> CreateColdRandomValues()
        {
            return Observable.Create<int>(async observer =>
            {
                var random = new Random();
                while (true)
                {
                    await Task.Delay(3000);
                    observer.OnNext(random.Next(100));
                }
            });
        }

        public static IObservable<ImmutableList<int>> CreateColdObservable()
        {
            return Observable.Create<ImmutableList<int>>(async observer =>
            {
                var val = ImmutableList.Create(1, 2, 3, 4);
                var random = new Random();
                observer.OnNext(val);

                while (true)
                {
                    await Task.Delay(1000);
                    var whatToDo = random.Next(2);
                    if (whatToDo == 0)
                    {
                        val = val.Add(random.Next(100));

                    }
                    else
                    {
                        val = val.RemoveAt(random.Next(val.Count));
                    }

                    observer.OnNext(val);
                }
            });

        }

        //private static Action<ImmutableList<int>> _whenSomethingHappens = val => { };

        private static async Task RunSequenceLogic(IObserver<ImmutableList<int>> observer)
        {
            var val = ImmutableList.Create(1, 2, 3, 4, 5, 6, 7);
            var random = new Random();

            while (true)
            {
                await Task.Delay(1000);
                var whatToDo = random.Next(2);
                if (whatToDo == 0)
                {
                    val = val.Add(random.Next(100));

                }
                else
                {
                    val = val.RemoveAt(random.Next(val.Count));
                }

                observer.OnNext(val);
            }
        }

        public static IObservable<ImmutableList<int>> CreateHotObservable()
        {
            var subject = new Subject<ImmutableList<int>>();
            var t = RunSequenceLogic(subject);

            //return Observable.Create<ImmutableList<int>>(observer =>
            //{
            //    _whenSomethingHappens += val => observer.OnNext(val);
            //    return Disposable.Empty;
            //});


            return subject;

        }

        static async Task Main(string[] args)
        {

            var cold = CreateColdRandomValues();

            var hot = cold
                .Multicast(new ReplaySubject<int>(4))
                .RefCount();

            await Task.Delay(2000);

            hot.Subscribe(val => Console.WriteLine($"A {DateTime.Now} {val}"));

            await Task.Delay(8000);


            hot.Subscribe(val => Console.WriteLine($"B {DateTime.Now} {val}"));

            await Task.Delay(5000);

            await Task.Delay(100000);

            // Publish
            // Share
        }

    }


}
