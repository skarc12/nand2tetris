using System;
using System.Diagnostics;
using NUnit.Framework;
using SharpTestsEx;

namespace NegativeMadness
{
    public class Experiences
    {
    }

    public static class BitExtensions
    {
        public static bool IsBitOn(this short field, byte offset)
        {
            if (offset > 15)
                throw new ArgumentOutOfRangeException();

            var b = 1 << offset;
            return (field & b) == b;
        }

        public static short TurnBitOn(this short field, byte offset)
        {
            if (offset > 15)
                throw new ArgumentOutOfRangeException();

            var b = 1 << offset;
            return (short) (field | b);
        }

        public static short TurnBitOff(this short field, byte offset)
        {
            if (offset > 15)
                throw new ArgumentOutOfRangeException();

            return (short) (field & ~(1 << offset)); 
            
        }
    }

    [TestFixture]
    public class BitExtensionsTests
    {
        [TestCase(1, 0)]
        [TestCase(2, 1)]
        [TestCase(4, 2)]
        [TestCase(8, 3)]
        public void IsBitOnReturnsTrueIfBitIsOn(short field, byte offset)
        {
            field.IsBitOn(offset).Should().Be.True();
        }

        [TestCase(0, 0)]
        [TestCase(2, 0)]
        [TestCase(4, 1)]
        [TestCase(8, 2)]
        public void IsBitOnReturnsFalseIfBitIsOff(short field, byte offset)
        {
            field.IsBitOn(offset).Should().Be.False();
        }

        [TestCase(0, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 4)]
        [TestCase(3, 8)]
        public void TurningBitOn(byte offset, short expected)
        {
            const short seed = 0;
            seed.TurnBitOn(offset).Should().Be(expected);
        }

        [TestCase(3, 0, 2)]
        [TestCase(5, 0, 4)]
        public void TurningBitOff(short field, byte offset, short expected)
        {
            field.TurnBitOff(offset).Should().Be(expected);
        }
    }
}
