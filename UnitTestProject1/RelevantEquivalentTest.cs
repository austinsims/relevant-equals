using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XBy2.RelevantEquals
{
    [TestClass]
    public class RelevantEquivalentTest
    {
        [TestMethod]
        public void Positive()
        {
            Assert.IsTrue(a.RelevantEquivalent<SomeClass>(b, new Expression<Func<SomeClass, object>>[] {
                (sc) => sc.Foo,
                (sc) => sc.Baz
            }));
        }

        [TestMethod]
        public void Negative()
        {
            Assert.IsFalse(a.RelevantEquivalent<SomeClass>(b, new Expression<Func<SomeClass, object>>[] {
                (sc) => sc.Foo,
                (sc) => sc.Bar,
                (sc) => sc.Baz
            }));
        }

        // Now some practical examples with an oversimplified File class

        [TestMethod]
        public void PracticalPositive() {
            Assert.IsTrue(myRepository.RelevantEquivalent<File>(
                yourRepository,
                new Expression<Func<File, object>>[] {
                    // Using an Expression lets you project the properties into the result of a method call if you like
                    // Let's get just the file name
                    (f) => f.Path.Substring(f.Path.LastIndexOf(@"\") + 1)
                }
            ));
        }

        [TestMethod]
        public void PracticalNegative() {
            Assert.IsFalse(myRepository.RelevantEquivalent<File>(
                yourRepository,
                new Expression<Func<File, object>>[] {
                    // compare whole path
                    (f) => f.Path
                }
            ));
        }


        // Classes used in examples

        private class SomeClass {
            public string Foo { get; set; }
            public int Bar { get; set; }
            public double Baz { get; set; }
        }

        private class File {
            public string Path { get; set; }
        }


        // Data for tests

        private readonly SomeClass[] a = new[] {
            new SomeClass {
                Foo = "Bacon",
                Bar = 42,
                Baz = 4.5
            },
            new SomeClass {
                Foo = "Sirlon",
                Bar = 18,
                Baz = 10.1
            },
            new SomeClass {
                Foo = "Ham Hock",
                Bar = 99,
                Baz = 22.2
            },
        };

        // Same as  a  except with different values of Bar
        private readonly SomeClass[] b = new[] {
            new SomeClass {
                Foo = "Bacon",
                Bar = 0,
                Baz = 4.5
            },
            new SomeClass {
                Foo = "Sirlon",
                Bar = 1234,
                Baz = 10.1
            },
            new SomeClass {
                Foo = "Ham Hock",
                Bar = 16,
                Baz = 22.2
            },
        };

        // A more practical example
        private readonly File[] myRepository = new[] {
            new File { Path = @"c:\repo\Program.cs"},
            new File { Path = @"c:\repo\Test.cs"},
            new File { Path = @"c:\repo\Project.csproj"},
        };

        private readonly File[] yourRepository = new[] {
            new File { Path = @"c:\Documents\repo\Program.cs"},
            new File { Path = @"c:\Documents\repo\Test.cs"},
            new File { Path = @"c:\Documents\repo\Project.csproj"},
        };
    }
}
