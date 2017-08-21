using System;
using System.Globalization;
using BanKai.Basic.Extensions;
using Xunit;

namespace BanKai.Basic
{
    // ReSharper disable ConvertNullableToShortForm
    // ReSharper disable ConditionIsAlwaysTrueOrFalse
    // ReSharper disable ReturnValueOfPureMethodIsNotUsed
    // ReSharper disable PossibleInvalidOperationException
    // ReSharper disable RedundantCast
    // ReSharper disable EqualExpressionComparison

    public class NullableTypes
    {
        [Fact]
        public void should_never_be_null_for_value_type()
        {
            const string shortProgram = 
                "using System;" +
                "public class Program {" +
                "  public static void Main() {" +
                "    int value = null;" +
                "  }" +
                "}";

            var compilerResults = SimpleCSharpCompiler.CompileWithoutRun(shortProgram);

            bool containsSyntaxError = compilerResults.Errors.Count > 0;

            // change the variable value to fix the test.            
            const bool expectedContainsSyntaxError = true;// ??????????

            Assert.Equal(expectedContainsSyntaxError, containsSyntaxError);
            Assert.Equal(
                "CS0037", /* cannot pass null to a not null type */
                compilerResults.Errors[0].ErrorNumber);
        }

        [Fact]
        public void should_be_clear_that_a_nullable_type_is_a_value_type()
        {
            Type nullableType = typeof(Nullable<int>);//Nullable表示可以为空的值类型

            bool isValueType = nullableType.IsValueType;

            // change the variable value to fix the test.            
            const bool expectedIsValueType = true;

            Assert.Equal(expectedIsValueType, isValueType);
        }

        [Fact]
        public void should_use_nullable_type_in_case_you_need_null_for_value_type_anyway()
        {
            Nullable<int> nullableInt = null;

            // change the variable value to fix the test.
            const bool expectedEquals = true;

            Assert.Equal(expectedEquals, nullableInt == null);
        }

        [Fact]
        public void should_use_simplified_syntax_for_nullable_type()
        {
            int? nullableInt = null;
            //值类型后面加问号表示可为空null(Nullable 结构)
            //int？：表示可空类型，就是一种特殊的值类型，它的值可以为null
            //用于给变量设初值得时候，给变量（int类型）赋值为null，而不是0
            //int？？：用于判断并赋值，先判断当前变量是否为null，如果是就可以赋役个新值，否则跳过
    
            // change the variable value to fix the test.
            const bool expectedEquals = true;

            Assert.Equal(expectedEquals, nullableInt == null);
        }

        [Fact]
        public void should_use_has_value_to_determine_if_a_nullable_type_has_value_type_instance()
        {
            int? nullableIntWithoutValue = null;
            int? nullableIntWithValue = 2;

            bool hasValueForWithoutValue = nullableIntWithoutValue.HasValue;//HasValue判断可空类型是否有值
            bool hasValueForWithValue = nullableIntWithValue.HasValue;

            //Value 的类型与基础类型相同。如果 HasValue 为 true，则说明 Value 包含有意义的值。如果 HasValue 为 false，则访问 Value 将引发 InvalidOperationException。

            // change the variable values for the following 2 lines to fix test.
            const bool expectedHasValueForWithoutValue = false;
            const bool expectedHasValueForWithValue = true;
            
            Assert.Equal(expectedHasValueForWithoutValue, hasValueForWithoutValue);
            Assert.Equal(expectedHasValueForWithValue, hasValueForWithValue);
        }

        [Fact]
        public void should_throw_if_you_use_value_without_checking()
        {
            int? nullableIntWithoutValue = null;

            Action useValueWithoutChecking =
                () => nullableIntWithoutValue.Value.ToString(CultureInfo.InvariantCulture);

            Exception unhandledException = useValueWithoutChecking.RunAndGetUnhandledException();

            // change the variable value to fix the test.
            Type expectedExceptionType = typeof(InvalidOperationException);

            Assert.IsType(expectedExceptionType, unhandledException);
        }

        [Fact]
        public void should_be_able_to_explicitly_cast_to_target_value_type()
        {
            int? nullableIntWithValue = 2;
            var valueForNullableInstance = (int) nullableIntWithValue;

            // change the variable value to fix the test.
            const int expectedValue = 2;

            Assert.Equal(expectedValue, valueForNullableInstance);
        }

        [Fact]
        public void should_bring_operators_for_underlaying_type()
        {
            int? larger = 5;
            int? smaller = 1;

            // change the variable value to fix the test.
            const bool expectedCompareResult = true;

            Assert.Equal(expectedCompareResult, larger > smaller);
        }

        [Fact]
        public void should_returns_false_if_one_of_the_nullable_instance_has_no_value_when_doing_operator_lifting()
        {
            // change the variable values for the following 4 lines to fix the test.
            const bool expectedResultFor5IsLargerThanNull = false;
            const bool expectedResultFor5IsSmallerThanNull = false;
            const bool expectedResultForNullIsLargerThanNull = false;

            Assert.Equal(expectedResultFor5IsLargerThanNull, (int?) 5 > (int?) null);
            Assert.Equal(expectedResultFor5IsSmallerThanNull, (int?) 5 < (int?) null);
            Assert.Equal(expectedResultForNullIsLargerThanNull, (int?) null > (int?) null);
        }

        [Fact]
        public void should_tell_equlity_for_nullable_types()
        {
            // change the variable values for the following 2 lines to fix the test.
            const bool expectedResultForNullToNull = false;
            const bool expectedResultFor5To5 = false;

            Assert.Equal(expectedResultForNullToNull, (int?) null == (int?) null);
            Assert.Equal(expectedResultFor5To5, (int?) 5 == (int?) 5);
        }
    }

    // ReSharper restore EqualExpressionComparison
    // ReSharper restore RedundantCast
    // ReSharper restore PossibleInvalidOperationException
    // ReSharper restore ReturnValueOfPureMethodIsNotUsed
    // ReSharper restore ConditionIsAlwaysTrueOrFalse
    // ReSharper restore ConvertNullableToShortForm
}