
namespace Csp_TilePlacement
{
    internal static class Helpers
    {
        public static int[][] CopyLayout( this int[][] tobeCopied)
        {
            return tobeCopied.Select(x => x.ToArray()).ToArray();
        }

        public static T[] SubArray<T>(this T[] array, int offset, int length)
        {
            T[] result = new T[length];
            Array.Copy(array, offset, result, 0, length);
            return result;
        }


    }
}
