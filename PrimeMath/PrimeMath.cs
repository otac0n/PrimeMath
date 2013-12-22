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

        private class PrimeState
        {
            private ulong largestValueChecked;
            private int nextPrimeSquaredIndex;
            private List<ulong> primes;

            public PrimeState()
            {
                this.primes = new List<ulong> { 2 };
                this.largestValueChecked = 2;
                this.nextPrimeSquaredIndex = 0;
            }

            public void FillPrimesBelow(ulong max)
            {
                var nextPrimeSquared = this.primes[this.nextPrimeSquaredIndex];
                nextPrimeSquared *= nextPrimeSquared;

                for (var num = this.largestValueChecked + 1; num <= max; this.largestValueChecked = num, num++)
                {
                    if (num == nextPrimeSquared)
                    {
                        var i = this.nextPrimeSquaredIndex + 1;
                        this.nextPrimeSquaredIndex = i;
                        nextPrimeSquared = this.primes[i];
                        nextPrimeSquared *= nextPrimeSquared;
                        continue;
                    }

                    var prime = true;
                    for (var primeIndex = 0; primeIndex < this.nextPrimeSquaredIndex; primeIndex++)
                    {
                        if (num % this.primes[primeIndex] == 0)
                        {
                            prime = false;
                            break;
                        }
                    }

                    if (prime)
                    {
                        this.primes.Add(num);
                    }
                }
            }

            public bool IsPrime(ulong value)
            {
                if (this.largestValueChecked >= value)
                {
                    return this.primes.BinarySearch(value) >= 0;
                }
                else
                {
                    var maxFactor = (ulong)Math.Sqrt(value);

                    var primeIndex = 0;
                    while (true)
                    {
                        var prime = this.primes[primeIndex];

                        if (prime > maxFactor)
                        {
                            break;
                        }
                        else if (value % prime == 0)
                        {
                            return false;
                        }

                        primeIndex++;

                        if (primeIndex >= this.primes.Count)
                        {
                            if (this.largestValueChecked >= maxFactor)
                            {
                                break;
                            }
                            else
                            {
                                this.FillPrimesBelow(maxFactor);

                                if (primeIndex >= this.primes.Count)
                                {
                                    break;
                                }
                            }
                        }

                        prime = this.primes[primeIndex];
                    }

                    return true;
                }
            }
        }
    }
}
