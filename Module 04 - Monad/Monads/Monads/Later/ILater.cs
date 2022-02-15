using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monads
{
    public interface ILater<out T>
    {
        bool IsCaluclated { get; }

        T Value { get; }

        void Calucate();
    }
}
