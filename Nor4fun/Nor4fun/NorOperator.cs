using NUnit.Framework;
using SharpTestsEx;

namespace Nor4fun
{
    [TestFixture]
    public class NorOperator
    {
        public bool Nor(bool a, bool b)
        {
            return !(a || b);
        }

        [TestCase(false, false, true)]
        [TestCase(true, false, false)]
        [TestCase(false, true, false)]
        [TestCase(true, true, false)]
        public void NorTests(bool a, bool b, bool expected)
        {
            Nor(a, b).Should().Be(expected);
        }

        public bool Not(bool a)
        {
            return Nor(a, a);
        }

        [TestCase(false, true)]
        [TestCase(true, false)]
        public void NotTests(bool a, bool expected)
        {
            Not(a).Should().Be(expected);
        }

        public bool Or(bool a, bool b)
        {
            //return Not(Nor(a, b));
            return Nor(Nor(a, b), Nor(a, b));
        }

        [TestCase(false, false, false)]
        [TestCase(true, false, true)]
        [TestCase(false, true, true)]
        [TestCase(true, true, true)]
        public void OrTests(bool a, bool b, bool expected)
        {
            Or(a, b).Should().Be(expected);
        }
    }
}
