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
        private ConsoleColor _color;
        private static object _lock = new object();

        public ConsoleObserver(string id, ConsoleColor color)
        {
            _id = id;
            _color = color;
        }

        private void Print(string txt)
        {
            lock(_lock)
            {
                var oldColor = Console.ForegroundColor;
                Console.ForegroundColor = _color;
                Console.WriteLine(txt);
                Console.ForegroundColor = oldColor;
            }
        }

        public void OnCompleted()
        {
            Print($"{DateTime.Now} Observer {_id} Completed");
        }

        public void OnError(Exception error)
        {
            Print($"{DateTime.Now} Observer {_id} Error {error.Message}");
        }

        public void OnNext(T value)
        {
            Print($"{DateTime.Now} Observer {_id} Next {value}");
        }
    }
}
