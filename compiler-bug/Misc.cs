using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.UI.MVVM.ObservedProperty
{
    internal delegate void EventHandler();
    internal delegate void EventHandler<T>(T value);

    internal delegate TOut ValueConverter<TIn, out TOut>(TIn input);

    internal static class Converter
    {
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        public static ValueConverter<ITuple, TOut> Curry<TIn1, TIn2, TOut>(ValueConverter<(TIn1, TIn2), TOut> sketch)
        {
            return t => sketch(((Const<TIn1>)t[0]!, (Const<TIn2>)t[1]!));
        }

        public static ValueConverter<ITuple, TOut> Curry<TIn1, TIn2, TIn3, TOut>(ValueConverter<(TIn1, TIn2, TIn3), TOut> sketch)
        {
            return t => sketch(((Const<TIn1>)t[0]!, (Const<TIn2>)t[1]!, (Const<TIn3>)t[2]!));
        }

        public static ValueConverter<ITuple, TOut> Curry<TIn1, TIn2, TIn3, TIn4, TOut>(ValueConverter<(TIn1, TIn2, TIn3, TIn4), TOut> sketch)
        {
            return t => sketch(((Const<TIn1>)t[0]!, (Const<TIn2>)t[1]!, (Const<TIn3>)t[2]!, (Const<TIn4>)t[3]!));
        }

        public static ValueConverter<ITuple, TOut> Curry<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(ValueConverter<(TIn1, TIn2, TIn3, TIn4, TIn5), TOut> sketch)
        {
            return t => sketch(((Const<TIn1>)t[0]!, (Const<TIn2>)t[1]!, (Const<TIn3>)t[2]!, (Const<TIn4>)t[3]!, (Const<TIn5>)t[4]!));
        }

        public static ValueConverter<ITuple, TOut> Curry<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut>(ValueConverter<(TIn1, TIn2, TIn3, TIn4, TIn5, TIn6), TOut> sketch)
        {
            return t => sketch(((Const<TIn1>)t[0]!, (Const<TIn2>)t[1]!, (Const<TIn3>)t[2]!, (Const<TIn4>)t[3]!, (Const<TIn5>)t[4]!, (Const<TIn6>)t[5]!));
        }
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
    }
}
