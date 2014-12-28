using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace XBy2.RelevantEquals
{
    public static class RelevantEqualsExtensions
    {
        /// <summary>
        /// Do all the specified properties of that object equal those of this object?
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propSelectors">List of expressions selecting relevant properties of the class to compare.  All others will be ignored.</param>
        public static Boolean RelevantEquals<T>(this T thiz, T that, IEnumerable<Expression<Func<T, object>>> propSelectors) {
            return propSelectors.All(selector => {
                var expected = selector.Compile().Invoke(thiz);
                var actual = selector.Compile().Invoke(that);
                return expected == null || actual == null
                    ? expected == actual
                    : expected.Equals(actual);
            });
        }

        /// <summary>
        /// Do all of the objects in this enumerable have an equal in that enumerable who are equal, considering only the selected properties?
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propSelectors"></param>
        public static Boolean RelevantEquivalent<T>(this IEnumerable<T> thiz, IEnumerable<T> that, IEnumerable<Expression<Func<T, object>>> propSelectors) {
            return true
                && thiz.Count() == that.Count()
                && thiz.All(thizThing => that.Any(thatThing => thatThing.RelevantEquals<T>(thizThing, propSelectors)));
        }
    }
}
