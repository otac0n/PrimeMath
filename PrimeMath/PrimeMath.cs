// -----------------------------------------------------------------------
// <copyright file="PrimeMath.cs" company="(none)">
//   Copyright © 2013 John Gietzen.  All Rights Reserved.
//   This source is subject to the MIT license.
//   Please see license.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace PrimeMath
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides static methods for prime-related mathematical functions.
    /// </summary>
    public static class PrimeMath
    {
        private static readonly PrimeState State = new PrimeState();

        /// <summary>
        /// Enumerates all prime numbers.
        /// </summary>
        /// <returns>An infinite enumerable collection of all prime numbers.</returns>
        public static IEnumerable<ulong> EnumeratePrimes()
        {
            return State.EnumeratePrimes();
        }

        /// <summary>
        /// Returns a value that indicates whether the specified value is a composite number.
        /// </summary>
        /// <param name="value">A number to test.</param>
        /// <returns><c>true</c> if <paramref name="value"/> is considered composite; otherwise, <c>false</c>.</returns>
        public static bool IsComposite(long value)
        {
            return IsComposite(Abs(value));
        }

        /// <summary>
        /// Returns a value that indicates whether the specified value is a composite number.
        /// </summary>
        /// <param name="value">A number to test.</param>
        /// <returns><c>true</c> if <paramref name="value"/> is considered composite; otherwise, <c>false</c>.</returns>
        [CLSCompliant(false)]
        public static bool IsComposite(ulong value)
        {
            if (value <= 3)
            {
                return false;
            }

            return !IsPrime(value);
        }

        /// <summary>
        /// Returns a value that indicates whether the specified value is a prime number.
        /// </summary>
        /// <param name="value">A number to test.</param>
        /// <returns><c>true</c> if <paramref name="value"/> is considered prime; otherwise, <c>false</c>.</returns>
        public static bool IsPrime(long value)
        {
            return IsPrime(Abs(value));
        }

        /// <summary>
        /// Returns a value that indicates whether the specified value is a prime number.
        /// </summary>
        /// <param name="value">A number to test.</param>
        /// <returns><c>true</c> if <paramref name="value"/> is considered prime; otherwise, <c>false</c>.</returns>
        [CLSCompliant(false)]
        public static bool IsPrime(ulong value)
        {
            return State.IsPrime(value);
        }

        private static ulong Abs(long value)
        {
            if (value == long.MinValue)
            {
                return ((ulong)long.MaxValue) + 1;
            }
            else
            {
                return (ulong)Math.Abs(value);
            }
        }
    }
}
