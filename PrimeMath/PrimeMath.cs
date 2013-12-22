using System;

namespace PrimeMath
{
    public static class PrimeMath
    {
        public static bool IsComposite(long num)
        {
            return IsComposite(Abs(num));
        }

        public static bool IsComposite(ulong num)
        {
            if (num <= 3)
            {
                return false;
            }

            return !IsPrime(num);
        }

        public static bool IsPrime(long num)
        {
            return IsPrime(Abs(num));
        }

        public static bool IsPrime(ulong num)
        {
            if (num <= 1)
            {
                return false;
            }

            for (ulong i = 2; i < num; i++)
            {
                if (num % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        private static ulong Abs(long num)
        {
            if (num == long.MinValue)
            {
                return ((ulong)long.MaxValue) + 1;
            }
            else
            {
                return (ulong)Math.Abs(num);
            }
        }
    }
}
