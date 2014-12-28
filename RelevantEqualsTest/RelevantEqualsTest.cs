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
        public int? Nullable { get; set; }
    }

    [TestClass]
    public class RelevantEqualsTest {
        private readonly SomeClass a = new SomeClass {
            Foo = "hello",
            Bar = "Haai",
            Norf = "Tjike",
            Baz = 33,
            Qux = 8.9,
            Nullable = null
        };

        private readonly SomeClass b = new SomeClass {
            Foo = "hello",
            Bar = "Haai",
            Norf = "What?",
            Baz = 33,
            Qux = 17.4,
            Nullable = null
        };

        [TestMethod]
        public void Positive() {
            var propSelectors = new Expression<Func<SomeClass, object>>[] {
                (sc) => sc.Foo,
                (sc) => sc.Bar,
                (sc) => sc.Baz,
            };
            Assert.IsTrue(a.RelevantEquals(b, propSelectors));

            // make sure RelevantEquals is symmetric
            Assert.IsTrue(b.RelevantEquals(a, propSelectors));

            // make sure RelevantEquals is reflexive
            Assert.IsTrue(a.RelevantEquals(a, propSelectors));
            Assert.IsTrue(b.RelevantEquals(b, propSelectors));
        }

        [TestMethod]
        public void Negative() {
            Assert.IsFalse(a.RelevantEquals(b, new Expression<Func<SomeClass, object>>[] {
                (sc) => sc.Foo,
                (sc) => sc.Bar,
                (sc) => sc.Baz,
                (sc) => sc.Qux,
            }));
        }

        // Make sure nullable properties are handled properly
        [TestMethod]
        public void NullableProperty()
        {
            Assert.IsTrue(a.RelevantEquals(b, new Expression<Func<SomeClass, object>>[]
            {
                (sc) => sc.Nullable
            }));
        }

    }
}
