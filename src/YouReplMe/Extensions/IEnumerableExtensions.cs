namespace System
{
    public static class IEnumerableExtensions
    {
        public static string Replicate(this string input, int length)
        {
            return string.Join(string.Empty, Enumerable.Range(0, length).Select(x => input));
        }

        public static void Tap<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }
    }
}