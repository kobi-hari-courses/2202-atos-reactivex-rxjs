using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx
{
    static public class CustomRxObservable
    {
        private static async void RunSequnece(IObserver<int> observer)
        {
            observer.OnNext(42);

            await Task.Delay(2000);

            observer.OnNext(100);

            await Task.Delay(2000);

            observer.OnCompleted();

            await Task.Delay(1000);
            observer.OnNext(-10);
            observer.OnError(new InvalidOperationException("This can not be"));
        }

        public static IObservable<int> CreateCustomObservable()
        {

            return Observable.Create<int>(observer =>
            {
                RunSequnece(observer);
                return Disposable.Empty;
            });
        }
    }
}
