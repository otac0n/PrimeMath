namespace PrimeMath.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class PrimeMathTests
    {
        private int[] composites = { 4, 6, 8, 9, 10, 1000, 62615533 };

        private int[] primes = { 2, 3, 5, 7, 11, 6833, 7919 };

        [Test]
        [TestCaseSource("composites")]
        public void IsComposite_WhenTheNumberIsAKnownComposite_ReturnsTrue(int num)
        {
            Assert.That(PrimeMath.IsComposite(num), Is.True);
        }

        [Test]
        [TestCaseSource("primes")]
        public void IsComposite_WhenTheNumberIsAKnownPrime_ReturnsFalse(int num)
        {
            Assert.That(PrimeMath.IsComposite(num), Is.False);
        }

        [Test]
        public void IsComposite_WhenTheNumberIsLongMinValue_ReturnsTrue()
        {
            Assert.That(PrimeMath.IsComposite(long.MinValue), Is.True);
        }

        [Test]
        public void IsComposite_WhenTheNumberIsNegativeOne_ReturnsFalse()
        {
            Assert.That(PrimeMath.IsComposite(-1), Is.False);
        }

        [Test]
        public void IsComposite_WhenTheNumberIsOne_ReturnsFalse()
        {
            Assert.That(PrimeMath.IsComposite(1), Is.False);
        }

        [Test]
        [TestCaseSource("composites")]
        public void IsComposite_WhenTheNumberIsTheNegativeOfAKnownComposite_ReturnsTrue(int num)
        {
            Assert.That(PrimeMath.IsComposite(-num), Is.True);
        }

        [Test]
        [TestCaseSource("primes")]
        public void IsComposite_WhenTheNumberIsTheNegativeOfAKnownPrime_ReturnsFalse(int num)
        {
            Assert.That(PrimeMath.IsComposite(-num), Is.False);
        }

        [Test]
        public void IsComposite_WhenTheNumberIsZero_ReturnsFalse()
        {
            Assert.That(PrimeMath.IsComposite(0), Is.False);
        }

        [Test]
        [TestCaseSource("composites")]
        public void IsPrime_WhenTheNumberIsAKnownComposite_ReturnsFalse(int num)
        {
            Assert.That(PrimeMath.IsPrime(num), Is.False);
        }

        [Test]
        [TestCaseSource("primes")]
        public void IsPrime_WhenTheNumberIsAKnownPrime_ReturnsTrue(int num)
        {
            Assert.That(PrimeMath.IsPrime(num), Is.True);
        }

        [Test]
        public void IsPrime_WhenTheNumberIsLongMinValue_ReturnsFalse()
        {
            Assert.That(PrimeMath.IsPrime(long.MinValue), Is.False);
        }

        [Test]
        public void IsPrime_WhenTheNumberIsNegativeOne_ReturnsFalse()
        {
            Assert.That(PrimeMath.IsPrime(-1), Is.False);
        }

        [Test]
        public void IsPrime_WhenTheNumberIsOne_ReturnsFalse()
        {
            Assert.That(PrimeMath.IsPrime(1), Is.False);
        }

        [Test]
        [TestCaseSource("composites")]
        public void IsPrime_WhenTheNumberIsTheNegativeOfAKnownComposite_ReturnsFalse(int num)
        {
            Assert.That(PrimeMath.IsPrime(-num), Is.False);
        }

        [Test]
        [TestCaseSource("primes")]
        public void IsPrime_WhenTheNumberIsTheNegativeOfAKnownPrime_ReturnsTrue(int num)
        {
            Assert.That(PrimeMath.IsPrime(-num), Is.True);
        }

        [Test]
        public void IsPrime_WhenTheNumberIsZero_ReturnsFalse()
        {
            Assert.That(PrimeMath.IsPrime(0), Is.False);
        }
    }
}
