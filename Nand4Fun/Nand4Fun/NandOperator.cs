using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpTestsEx;

namespace Nand4Fun
{
    [TestFixture]
    public class NandOperator
    {

        public bool Nand(bool a, bool b)
        {
            return !(a && b);
        }

        [TestCase(true, true, false)]
        [TestCase(true, false, true)]
        [TestCase(false, true, true)]
        [TestCase(false, false, true)]
        public void NandTests(bool a, bool b, bool expected)
        {
            Nand(a, b).Should().Be(expected);
        }

        public bool Not(bool a)
        {
            return Nand(a, a);
        }

        [TestCase(false, true)]
        [TestCase(true, false)]
        public void NotTests(bool a, bool expected)
        {
            Not(a).Should().Be(expected);
        }

        public bool And(bool a, bool b)
        {
            //return Not(Nand(a, b));
            return Nand(Nand(a, b), Nand(a, b));
        }

        [TestCase(true, true, true)]
        [TestCase(true, false, false)]
        [TestCase(false, true, false)]
        [TestCase(false, false, false)]
        public void AndTests(bool a, bool b, bool expected)
        {
            And(a, b).Should().Be(expected);
        }


    }
}
