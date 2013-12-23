using System;
using System.Collections.Generic;
using System.Threading;

namespace PrimeMath
{
    internal class PrimeState
    {
        private readonly ReaderWriterLockSlim @lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        private ulong largestValueChecked;
        private int nextPrimeSquaredIndex;
        private List<ulong> primes;

        public PrimeState()
        {
            this.primes = new List<ulong> { 2 };
            this.largestValueChecked = 2;
            this.nextPrimeSquaredIndex = 0;
        }

        public IEnumerable<ulong> EnumeratePrimes()
        {
            var primeIndex = 0;
            while (true)
            {
                ulong prime;

                // acquire (@lock.ReadLock)
                #region {
                try
                {
                    @lock.EnterReadLock();
                #endregion
                    while (primeIndex >= this.primes.Count)
                    {
                        this.FillPrimesBelow(this.largestValueChecked + 1000);
                    }

                    prime = this.primes[primeIndex++];
                }
                #region .
                finally
                {
                    @lock.ExitReadLock();
                }
                #endregion

                yield return prime;
            }
        }

        public void FillPrimesBelow(ulong max)
        {
            // release (@lock.ReadLock)
            #region {
            try
            {
                @lock.ExitReadLock();
            #endregion
                // acquire (@lock.WriteLock)
                #region {
                try
                {
                    @lock.EnterWriteLock();
                #endregion
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
                #region .
                finally
                {
                    @lock.ExitWriteLock();
                }
                #endregion
            }
            #region .
            finally
            {
                @lock.EnterReadLock();
            }
            #endregion
        }

        public bool IsPrime(ulong value)
        {
            // acquire (@lock.ReadLock)
            #region {
            try
            {
                @lock.EnterReadLock();
            #endregion
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
            #region .
            finally
            {
                @lock.ExitReadLock();
            }
            #endregion
        }
    }
}
