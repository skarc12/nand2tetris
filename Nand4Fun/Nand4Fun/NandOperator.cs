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


    }
}
