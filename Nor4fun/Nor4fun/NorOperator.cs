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
        
    }
}
