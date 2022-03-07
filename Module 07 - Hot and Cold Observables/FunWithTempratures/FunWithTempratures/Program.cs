using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace FunWithTempratures
{
    internal class Program
    {
        private static Random _random = new Random();

        public static IObservable<int> CreateRandomValues()
        {
            return Observable.Create<int>(async observer =>
            {
                while(true)
                {
                    await Task.Delay(3000);
                    observer.OnNext(_random.Next(100));
                }
            });
        }

        static async Task Main(string[] args)
        {
            // Publish() ===== Multicast(new Subject())
            // Publish(initial value) ====== Multicast(new BehaviorSubject(initial value))
            // Replay(bufferCount?, windowLength?) ===== Multicast(new ReplaySubject(buffer, window))


            // Share() ===== Publish().RefCount()

            var obs = CreateRandomValues()
                .Replay(1)
                .RefCount();
                

            obs.SubscribeToConsole("First", ConsoleColor.Green);

            await Task.Delay(11000);

            obs.SubscribeToConsole("Second", ConsoleColor.Red);

            await Task.Delay(100000);


        }
    }
}
