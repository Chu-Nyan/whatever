namespace Chunity.Core
{
    public static class IDGenerator
    {
        private static int _lastID = 0;

        public static int Count
        {
            get => _lastID;
        }

        public static int Next()
        {
            _lastID++;
            return _lastID;
        }
    }
}
