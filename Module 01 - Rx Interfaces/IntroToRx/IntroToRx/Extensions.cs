using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx
{
    public static class Extensions
    {
        private static async void RunAsyncLoop<T>(IAsyncEnumerable<T> source, IObserver<T> observer)
        {
            await foreach (var item in source)
            {
                observer.OnNext(item);
            }

            observer.OnCompleted();
        }

        public static IObservable<T> ToObservable<T>(this IAsyncEnumerable<T> source)
        {
            return Observable.Create<T>(observer =>
            {
                RunAsyncLoop(source, observer);
                return Disposable.Empty;
            });
        }
    }

}
