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

        public bool Or(bool a, bool b)
        {
            //return Not(And(Not(a), Not(b)));
            return Nand(Nand(a, a), Nand(b, b));
        }

        [TestCase(true, true, true)]
        [TestCase(true, false, true)]
        [TestCase(false, true, true)]
        [TestCase(false, false, false)]
        public void OrTests(bool a, bool b, bool expected)
        {
            Or(a, b).Should().Be(expected);
        }


        public bool AndNot(bool a, bool b)
        {
            //return And(a, Not(b));
            return Nand(
                Nand(
                    a,
                    Nand(b, b)
                    ),
                Nand(
                    a,
                    Nand(b, b)
                    ));
        }

        [TestCase(true, true, false)]
        [TestCase(true, false, true)]
        [TestCase(false, true, false)]
        [TestCase(false, false, false)]
        public void AndNotTests(bool a, bool b, bool expected)
        {
            AndNot(a, b).Should().Be(expected);
        }

        public bool NotAnd(bool a, bool b)
        {
            //return And(Not(a), b);
            return Nand(
                Nand(
                    Nand(a, a),
                    b
                    ),
                Nand(
                    Nand(a, a),
                    b
                    ));
        }

        [TestCase(true, true, false)]
        [TestCase(true, false, false)]
        [TestCase(false, true, true)]
        [TestCase(false, false, false)]
        public void NotAndTests(bool a, bool b, bool expected)
        {
            NotAnd(a, b).Should().Be(expected);
        }

        public bool Xor(bool a, bool b)
        {
            //return And(Or(a, b), Not(And(a, b)));
            //return AndNot(Or(a, b), And(a, b));

            //return AndNot(
            //    Nand(Nand(a, a), Nand(b, b)),
            //    Nand(Nand(a, b), Nand(a, b))
            //    );

            return Nand(
                Nand(
                    Nand(Nand(a, a), Nand(b, b)),
                    Nand(
                        Nand(Nand(a, b), Nand(a, b)),
                        Nand(Nand(a, b), Nand(a, b))
                        )
                    ),
                Nand(
                    Nand(Nand(a, a), Nand(b, b)),
                    Nand(
                        Nand(Nand(a, b), Nand(a, b)),
                        Nand(Nand(a, b), Nand(a, b))
                        )
                    ));

            //return false;
        }

        [TestCase(true, true, false)]
        [TestCase(true, false, true)]
        [TestCase(false, true, true)]
        [TestCase(false, false, false)]
        public void XorTests(bool a, bool b, bool expected)
        {
            Xor(a, b).Should().Be(expected);
        }

       

    }
}
