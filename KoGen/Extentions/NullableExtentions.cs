using System;

namespace KoGen.Extentions
{
    public static class NullableExtentions
    {
        public static void IfPresent<T>(this Nullable<T> nullable, Action<T> action) where T : struct
        {
            if (nullable.HasValue)
                action.Invoke(nullable.Value);
        }

        public static void IfPresent<T>(T value, Action<T> action) where T : class
        {
            if (value != null)
                action.Invoke(value);
        }

        public static TResult IfPresent<T, TResult>(T value, Func<T, TResult> ifTrue, TResult ifFalse = null) where T : class where TResult : class
        {
            if (value != null)
                return ifTrue.Invoke(value);
            return ifFalse;
        }
    }


}
