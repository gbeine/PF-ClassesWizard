using System;

namespace PF_Core.Extensions
{
    public static class UnitEngine_ObjectExtensions
    {
        internal static T CreateCopy<T>(this T original) where T : UnityEngine.Object =>
            CreateCopy(original, null);

        internal static T CreateCopy<T>(this T original, Action<T> action = null) where T : UnityEngine.Object
        {
            var clone = UnityEngine.Object.Instantiate(original);
            if (action != null)
            {
                action(clone);
            }
            return clone;
        }
    }
}
