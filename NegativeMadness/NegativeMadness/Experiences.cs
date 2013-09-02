using System;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;

namespace NegativeMadness
{
    public class Chips
    {
        public bool HalfAdderSum(bool bit1, bool bit2)
        {
            return bit1 ^ bit2;
        }

        public bool HalfAdderCarry(bool bit1, bool bit2)
        {
            return bit1 && bit2;
        }

        public bool FullAdderSum(bool carry, bool bit1, bool bit2)
        {
            //return 
            //    (carry && !(bit1 ^ bit2)) ||
            //    (!carry && (bit1 ^ bit2));

            //return HalfAdderSum(
            //    carry,
            //    HalfAdderSum(bit1, bit2)
            //    );

            return carry ^ bit1 ^ bit2;
        }

        public bool FullAdderCarry(bool carry, bool bit1, bool bit2)
        {
            return
                (carry && (bit1 ^ bit2)) ||
                (bit1 && (carry ^ bit2)) ||
                (carry && bit1);

        }

        public short FullAdder16(short a, short b)
        {
            short result = 0;
            var carry = false;
            for (byte i = 0; i < 16; i++)
            {
                var bit1 = a.IsBitOn(i);
                var bit2 = b.IsBitOn(i);
                result = result.ChangeBit(i, FullAdderSum(carry, bit1, bit2));
                carry = FullAdderCarry(carry, bit1, bit2);
            }
            return result;
        }
    }

    [TestFixture]
    public class ChipsTests
    {
        [TestCase(false, false, false)]
        [TestCase(false, true, true)]
        [TestCase(true, false, true)]
        [TestCase(true, true, false)]
        public void HalfAdderSumTests(bool bit1, bool bit2, bool expected)
        {
            var chips = new Chips();
            chips.HalfAdderSum(bit1, bit2).Should().Be(expected);
        }

        [TestCase(false, false, false)]
        [TestCase(false, true, false)]
        [TestCase(true, false, false)]
        [TestCase(true, true, true)]
        public void HalfAdderCarryTests(bool bit1, bool bit2, bool expected)
        {
            var chips = new Chips();
            chips.HalfAdderCarry(bit1, bit2).Should().Be(expected);
        }

        [TestCase(false, false, false, false)]
        [TestCase(true, false, false, true)]
        [TestCase(false, true, false, true)]
        [TestCase(false, false, true, true)]
        [TestCase(true, true, false, false)]
        [TestCase(true, false, true, false)]
        [TestCase(false, true, true, false)]
        [TestCase(true, true, true, true)]
        public void FullAdderSumTests(bool carry, bool bit1, bool bit2, bool expected)
        {
            var chips = new Chips();
            chips.FullAdderSum(carry, bit1, bit2).Should().Be(expected);
        }

        [TestCase(false, false, false, false)]
        [TestCase(true, false, false, false)]
        [TestCase(false, true, false, false)]
        [TestCase(false, false, true, false)]
        [TestCase(true, true, false, true)]
        [TestCase(true, false, true, true)]
        [TestCase(false, true, true, true)]
        [TestCase(true, true, true, true)]
        public void FullAdderCarryTests(bool carry, bool bit1, bool bit2, bool expected)
        {
            var chips = new Chips();
            chips.FullAdderCarry(carry, bit1, bit2).Should().Be(expected);
        }

        [TestCase(0, 0, 0)]
        [TestCase(1, 1, 2)]
        [TestCase(45, -26, 19)]
        public void FullAdder16Tests(short a, short b, short expected)
        {
            var chips = new Chips();
            chips.FullAdder16(a, b).Should().Be(expected); 
        }
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

            var b = (short) (1 << offset);
            return (short) (field | b);
        }

        public static short TurnBitOff(this short field, byte offset)
        {
            if (offset > 15)
                throw new ArgumentOutOfRangeException();

            return (short) (field & ~(1 << offset)); 
        }

        public static short ChangeBit(this short field, byte offset, bool value)
        {
            return value
                ? field.TurnBitOn(offset)
                : field.TurnBitOff(offset);
        }

        public static string ToBitString(this short field)
        {
            var sb = new StringBuilder(19);
            const byte offset = 4;
            for (byte i = 0; i < offset; i++)
            {
                for (byte j = 0; j < offset; j++)
                {
                    sb.Append(field.IsBitOn((byte) (15 - (i * offset + j))) ? "1" : "0");
                }

                if (i != 3) sb.Append(" ");
            }

            return sb.ToString();
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

        [TestCase(0, "0000 0000 0000 0000")]
        [TestCase(1, "0000 0000 0000 0001")]
        [TestCase(2, "0000 0000 0000 0010")]
        [TestCase(3, "0000 0000 0000 0011")]
        [TestCase(4, "0000 0000 0000 0100")]
        [TestCase(short.MinValue, "1000 0000 0000 0000")]
        public void ToBitString(short field, string expected)
        {
            field.ToBitString().Should().Be(expected);
        }
    }
}
