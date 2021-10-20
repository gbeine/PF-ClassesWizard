using System.Linq;

namespace PF_Core.Extensions
{
    public static class Generic
    {
        public static T[] RemoveFromArray<T>(this T[] array, T value)
        {
            var list = array.ToList();
            return list.Remove(value) ? list.ToArray() : array;
        }
    }
}
