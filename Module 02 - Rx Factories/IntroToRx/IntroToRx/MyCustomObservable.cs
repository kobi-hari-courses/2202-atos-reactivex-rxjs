using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx
{
    public class MyCustomObservable : IObservable<int>
    {
        private async void RunObserverSequence(IObserver<int> observer)
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

        public IDisposable Subscribe(IObserver<int> observer)
        {
            RunObserverSequence(observer);

            return Disposable.Empty;
        }
    }
}
