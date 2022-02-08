using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace IntroToRx
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var observable = Observable.Empty<int>();

            var observer1 = new ConsoleObserver<int>("First");
            var observer2 = new ConsoleObserver<int>("Second");

            observable.Subscribe(observer1);

            await Task.Delay(2000);

            observable.Subscribe(observer2);

        }
    }
}
