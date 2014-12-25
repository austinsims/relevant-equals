These are two extension methods to Object that compare instances based on the values of a subset of their properties.

RelevantEquals
--------------
Given a class:

    class F {
        public string Foo { get; set; }
        public string Bar { get; set; }
    }

using RelevantEquals will yield the following results:

    var a = new F { Foo = "Hello", Bar = "World" };
    var b = new F { Foo = "Goodbye", Bar = "World" };

    // true
    a.RelevantEquals<F>(b, new Expression<Func<F, object>>[] {
        (f) => f.Bar
    })

    // false
    a.RelevantEquals<F>(b, new Expression<Func<F, object>>[] {
        (f) => f.Foo,
        (f) => f.Bar
    })

RelevantEquivalent
----------------
This is similar to CollectionAssert.AreEquivalent.  It will return true if the two collections have the same instances as far as the specified properties go.

    var things = new[] {
        new F { Foo = "English", Bar = "England"},
        new F { Foo = "Swedish", Bar = "Sweden"},
    };

    var slightlyDifferentThings = new[] {
        new F { Foo = "Anglisch", Bar = "England"},
        new F { Foo = "Svenska", Bar = "Sweden"},
    };

    // true
    things.RelevantEquivalent<T>(slightlyDifferentThings, new Expression<Func<F, object>>[] {
        (f) => f.Bar
    });

    // false
    things.RelevantEquivalent<F>(slightlyDifferentThings, new Expression<Func<F, object>>[] {
        (f) => f.Foo,
        (f) => f.Bar
    });
