using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monads
{
    // when we say "out T" it is called Covariance
    // when we say "in T" it is called Contravariance

    public interface IMyInterface<out T1, in T2>
    {
    }
}
