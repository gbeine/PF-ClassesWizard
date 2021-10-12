using System;

namespace PF_Core.Facades
{
    public class Harmony
    {
        public delegate void FastSetter(object source, object value);
        internal static FastSetter CreateFieldSetter<T>(string name) => CreateFieldSetter(typeof(T), name);
        static FastSetter CreateFieldSetter(Type type, string name)
        {
            return new FastSetter(Harmony12.FastAccess.CreateSetterHandler(Harmony12.AccessTools.Field(type, name)));
        }

        public delegate object FastGetter(object source);
        public static FastGetter CreateFieldGetter<T>(string name) => CreateFieldGetter(typeof(T), name);
        public static FastGetter CreateFieldGetter(Type type, string name)
        {
            return new FastGetter(Harmony12.FastAccess.CreateGetterHandler(Harmony12.AccessTools.Field(type, name)));
        }
    }
}
