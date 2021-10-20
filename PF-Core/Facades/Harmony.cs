using System;
using System.Threading;

namespace PF_Core.Facades
{
    public static class Harmony
    {
        internal delegate void FastSetter(object source, object value);
        internal static FastSetter CreateFieldSetter<T>(string name) => CreateFieldSetter(typeof(T), name);
        internal static FastSetter CreateFieldSetter(Type type, string name)
        {
            return new FastSetter(Harmony12.FastAccess.CreateSetterHandler(Harmony12.AccessTools.Field(type, name)));
        }

        internal delegate object FastGetter(object source);
        internal static FastGetter CreateFieldGetter<T>(string name) => CreateFieldGetter(typeof(T), name);
        internal static FastGetter CreateFieldGetter(Type type, string name)
        {
            return new FastGetter(Harmony12.FastAccess.CreateGetterHandler(Harmony12.AccessTools.Field(type, name)));
        }

        internal static Type GetType<T>(string name)
        {
            return Harmony12.AccessTools.Inner(typeof(T), name);
        }

        public static T GetField<T>(this object obj, string name)
        {
            return (T)Harmony12.AccessTools.Field(obj.GetType(), name).GetValue(obj);
        }

        public static void SetField(this object obj, string name, object value)
        {
            Harmony12.AccessTools.Field(obj.GetType(), name).SetValue(obj, value);
        }
    }
}
