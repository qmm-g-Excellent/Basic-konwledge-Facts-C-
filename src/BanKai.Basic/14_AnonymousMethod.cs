using System;
using Xunit;

namespace BanKai.Basic
{
    // ReSharper disable ConvertToLambdaExpression

    public class AnonymousMethod
    {
        [Fact]
        public void should_write_anonymous_inplace()
        {
            Func<int, int> doubleTransform = delegate(int x)//匿名方法 ，可以卸载内部，不被外界看到
            {
                return x * 2;
            };

            int transformResult = doubleTransform(2);

            // change variable value to fix test.
            const int expectedResult = 4;

            Assert.Equal(expectedResult, transformResult);
        }

        [Fact]
        public void should_write_anonymous_method_in_a_more_simple_way()
        {
            Func<int, int> doubleTransform = x => x * 2;//Lamnd表达式可以隐式的转换为匿名函数(提倡这种)

            int transformResult = doubleTransform(2);

            // change variable value to fix test.
            const int expectedResult = 4;

            Assert.Equal(expectedResult, transformResult);
        }
    }

    // ReSharper restore ConvertToLambdaExpression
}