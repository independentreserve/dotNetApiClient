using System.Text;

namespace IndependentReserve.DotNetClientApi.Helpers
{
    public static class Fnv1a
    {
        private const uint _fnvOffsetBasis = 2166136261;
        private const uint _fnvPrime = 16777619U;

        /// <summary>
        /// Get FNV-1a hash code of the string (32-bit).
        /// https://en.wikipedia.org/wiki/Fowler%E2%80%93Noll%E2%80%93Vo_hash_function
        /// </summary>
        public static uint GetFnv1aHashCode(byte[] data)
        {
            uint hash = _fnvOffsetBasis;
            for (int i = 0; i < data.Length; ++i)
            {
                hash ^= data[i];
                hash *= _fnvPrime;
            }

            return hash;
        }

        public static uint GetFnv1aHashCode(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            return GetFnv1aHashCode(bytes);
        }
    }
}
