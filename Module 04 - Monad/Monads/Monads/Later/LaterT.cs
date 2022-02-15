using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monads
{
    internal class Later<T> : ILater<T>
    {
        private readonly Func<T> _evaluator;
        private T _value = default;
        private bool _isCalculated = false;
        private readonly object _lock = new object();

        public Later(Func<T> eval)
        {
            _evaluator = eval;
        }

        public T Value
        {
            get
            {
                Calucate();
                return _value;
            }
        }

        public bool IsCaluclated => _isCalculated;

        public void Calucate()
        {
            lock(_lock)
            {
                if (!_isCalculated)
                {
                    _value = _evaluator();
                    _isCalculated = true;
                }
            }
        }

        public override string ToString()
        {
            return _isCalculated ? Value.ToString() : "[LATER]";
        }
    }
}
