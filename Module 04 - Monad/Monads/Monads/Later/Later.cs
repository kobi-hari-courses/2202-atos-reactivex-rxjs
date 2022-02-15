using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monads
{
    public static class Later
    {
        // Monad - <T> that holds a value of T or - to be more percise - the logic to calculate it
        // 1. Unit method. T => M<T>
        // 2. Bind method M<T>, (T => M<R>) => M<R>

        public static ILater<T> Create<T>(Func<T> func)
        {
            return new Later<T>(func);
        }

        public static ILater<T> Of<T>(T value)
        {
            return Create(() => value);
        }

        public static ILater<TResult> Map<T, TResult>(this ILater<T> @this, Func<T, TResult> func)
        {
            return Create(() => func(@this.Value));
        }

        public static ILater<T> Flatten<T>(this ILater<ILater<T>> @this)
        {
            return Create(() => @this.Value.Value);
        }

        public static ILater<TResult> FlatMap<T, TResult>(this ILater<T> @this, Func<T, ILater<TResult>> func)
        {
            return @this.Map(func).Flatten();
        }

        public static ILater<TResult> Mix<T1, T2, TResult>(
            this ILater<T1> @this, 
            ILater<T2> that, 
            Func<T1, T2, TResult> mixer)
        {
            return Create(() => mixer(@this.Value, that.Value));
        } 

        public static ILater<TResult> Select<T, TResult>(this ILater<T> @this, Func<T, TResult> func)
        {
            return @this.Map(func);
        }

        public static ILater<TResult> SelectMany<T, TSelect, TResult>(
            this ILater<T> @this,
            Func<T, ILater<TSelect>> selector,
            Func<T, TSelect, TResult> resultSelector)
        {
            var later2 = @this.FlatMap(selector);
            return @this.Mix(later2, resultSelector);
        }


    }
}
