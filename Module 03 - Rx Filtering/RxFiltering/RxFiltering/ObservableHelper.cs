using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxFiltering
{
    public static class ObservableHelper
    {
        private static object _ConsoleLock = new object();

        public static void Print(this string txt, ConsoleColor color)
        {
            lock(_ConsoleLock)
            {
                var oldColor = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.WriteLine(txt);
                Console.ForegroundColor = oldColor;
            }
        }

        public static IDisposable SubscribeToConsole<T>(this IObservable<T> @this, string name, ConsoleColor color)
        {
            return @this.Subscribe(
                onNext: val => $"{name} onNext: {val}".Print(color),
                onError: e => $"{name} onError: {e}".Print(color),
                onCompleted: () => $"{name} onCompleted".Print(color));
        }

        public static IObservable<T> ToObservable<T>(this IAsyncEnumerable<T> @this)
        {
            return Observable.Create<T>(async observer =>
            {
                await foreach (var item in @this)
                {
                    observer.OnNext(item);
                }

                observer.OnCompleted();
            });
        }
    }
}
