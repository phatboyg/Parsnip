namespace Parsnip
{
    public static class CursorExtensions
    {
        public static Cursor<T[]> Skip<T>(this Cursor<T[]> cursor, int skip)
        {
            int offset = cursor.Offset + skip;
            Cursor<T[]> result;
            if (cursor.Input.TryGet(offset, cursor.Count - skip, out result))
                return result;

            cursor.Input.TryGet(cursor.Offset + cursor.Count - offset, 0, out result);
            return result;
        }

        public static Cursor<string> Skip(this Cursor<string> cursor, int skip)
        {
            int offset = cursor.Offset + skip;
            Cursor<string> result;
            if (cursor.Input.TryGet(offset, cursor.Count - skip, out result))
                return result;

            cursor.Input.TryGet(cursor.Offset + cursor.Count - offset, 0, out result);
            return result;
        }
    }
}