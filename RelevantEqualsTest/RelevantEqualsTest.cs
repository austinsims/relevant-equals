using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XBy2.RelevantEquals;

namespace XBy2.RelevantEquals {
    class SomeClass {
        public string Foo { get; set; }
        public string Bar { get; set; }
        public string Norf { get; set; }
        public int Baz { get; set; }
        public double Qux { get; set; }
    }

    [TestClass]
    public class RelevantEqualsTest {
        private readonly SomeClass a = new SomeClass {
            Foo = "hello",
            Bar = "Haai",
            Norf = "Tjike",
            Baz = 33,
            Qux = 8.9
        };

        private readonly SomeClass b = new SomeClass {
            Foo = "hello",
            Bar = "Haai",
            Norf = "What?",
            Baz = 33,
            Qux = 17.4
        };

        [TestMethod]
        public void Positive() {
            Assert.IsTrue(a.RelevantEquals<SomeClass>(b, new Expression<Func<SomeClass, object>>[] {
                (sc) => sc.Foo,
                (sc) => sc.Bar,
                (sc) => sc.Baz,
            }));
        }

        [TestMethod]
        public void Negative() {
            Assert.IsFalse(a.RelevantEquals<SomeClass>(b, new Expression<Func<SomeClass, object>>[] {
                (sc) => sc.Foo,
                (sc) => sc.Bar,
                (sc) => sc.Baz,
                (sc) => sc.Qux,
            }));
        }

    }
}
