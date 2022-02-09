using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx
{
    public class ConsoleObserver<T> : IObserver<T>
    {
        private string _id;

        public ConsoleObserver(string id)
        {
            _id = id;
        }

        public void OnCompleted()
        {
            Console.WriteLine($"{DateTime.Now} Observer {_id} Completed");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"{DateTime.Now} Observer {_id} Error {error.Message}");
        }

        public void OnNext(T value)
        {
            Console.WriteLine($"{DateTime.Now} Observer {_id} Next {value}");
        }
    }
}
