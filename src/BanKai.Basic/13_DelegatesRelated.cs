using System;
using BanKai.Basic.Common;
using Xunit;

namespace BanKai.Basic
{
#pragma warning disable 183
    // ReSharper disable DelegateSubtraction

    public class DelegatesRelated
    {
        private static int EquivalentTransformation(int x)
        {
            return x;
        }

        private static int DoubleTransformation(int x)
        {
            return x * 2;
        }

        private static int PassingDelegateAsArgument(TransformerDelegateDemo transformer)
        {
            return transformer(2) + 1;
        }

        [Fact]
        public void delegate_is_a_type_wrapping_method_call()
        {
            TransformerDelegateDemo transformer = EquivalentTransformation;

            Console.WriteLine(typeof(TransformerDelegateDemo));
            // change variable value to fix test.
            const string expectedDelegateType = "TransformerDelegateDemo";

            Assert.Equal(expectedDelegateType, typeof(TransformerDelegateDemo).Name);
            Assert.True(transformer is Delegate);
        }

        [Fact]
        public void should_call_original_method_when_invoking_delegate()
        {
            TransformerDelegateDemo transformer = EquivalentTransformation;

            int transformResult = transformer(2);  // ?????????????

            // change variable value to fix test.
            const int expectedResult = 2;  

            Assert.Equal(expectedResult, transformResult);
        }

        [Fact]
        public void should_pass_delegate_instance_as_normal_variable()
        {
            TransformerDelegateDemo transformer = DoubleTransformation;//transformer是一个委托，把这个函数赋值为transformer

            int actualResult = PassingDelegateAsArgument(transformer);

            // change variable value to fix test.
            const int expectedResult = 5; // ???????????

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void should_multicast_delegate()
        {
            var demoObject = new MulticastDelegateDemoClass();

            Action theDelegate = demoObject.OneMethod;//Action是一个委托，一个委托可以绑定多个函数
            theDelegate += demoObject.AnotherMethod;//绑定多个函数，按照添加的顺序执行的

            theDelegate();

            // change variable value to fix test.
            var expectedTrace = new string[] { "MulticastDelegateDemoClass.OneMethod() called" , "MulticastDelegateDemoClass.AnotherMethod() called" };

            Assert.Equal(expectedTrace, demoObject.Trace);
        }

        [Fact]
        public void should_unbind_multicast_delegate()
        {
            var demoObject = new MulticastDelegateDemoClass();

            Action theDelegate = demoObject.OneMethod;
            theDelegate += demoObject.AnotherMethod;
            theDelegate -= demoObject.OneMethod;

            theDelegate();

            // change variable value to fix test.
            var expectedTrace = new string[] { "MulticastDelegateDemoClass.AnotherMethod() called" };

            Assert.Equal(expectedTrace, demoObject.Trace);
        }

        [Fact]
        public void should_be_immutable_and_create_new_delegate_on_substraction_change()
        {
            var demoObject = new MulticastDelegateDemoClass();

            Action theDelegate = demoObject.OneMethod;
            Action copy = theDelegate;
            //delegate是不可变的！！！
            theDelegate += demoObject.AnotherMethod;//和String一样，不会再原来的基础上修改（不能变），只会重新创建一个新的空间，来存储修改后的内容

            // change variable value to fix test.
            const bool areReferenceEqual = false;

            Assert.Equal(areReferenceEqual, ReferenceEquals(theDelegate, copy));
        }

        [Fact]
        public void should_be_list_subtraction_rather_than_scalar_substraction()
        {
            var demoObject = new DelegateSubtractionDemoClass();

            Action a = demoObject.A;
            Action b = demoObject.B;
            Action c = demoObject.C;

            ((a + b + c) - (a + c))();//delegate是有执行顺序的，必须减执行顺序相同的，比如这里可以减ab，减bc，但是减ac是不能成功的

            // change variable value to fix test.
            const string expectedOutput = "A,B,C";

            Assert.Equal(expectedOutput, demoObject.ToString());
        }

        [Fact]
        public void should_get_result_of_last_called_function()
        {
            var demoObject = new MulticastFuncDelegateDemoClass();

            Func<int> returnsOne = demoObject.ReturnsOne;
            Func<int> returnsTwo = demoObject.ReturnsTwo;
            Func<int> returnsThree = demoObject.ReturnsThree;

            int returnedResult = (returnsOne + returnsThree + returnsTwo)();

            // change variable value to fix test.
            const int expectedResult = 2;//返回的是delegate委托最后执行的那个的返回值

            Assert.Equal(expectedResult, returnedResult);
        }

        [Fact]
        public void should_be_return_type_covariance()
        {
            var demoObject = new DelegateTypeVarianceDemoClass();

            Func<object> delegateReturnsObject = demoObject.ReturnsMoreSpecificType;

            object returnedValue = delegateReturnsObject();

            // change variable value to fix test.
            object expectedValue = "Hello";

            Assert.Equal(expectedValue, returnedValue);
        }

        [Fact]
        public void should_be_parameter_contravariance()
        {
            var demoObject = new DelegateTypeVarianceDemoClass();

            Func<object, string> delegateAcceptsObject = demoObject.InputMoreGeneralType;
            Func<string, string> delegateAcceptsString = delegateAcceptsObject;

            string returnedValue = delegateAcceptsString("Good");

            // change variable value to fix test.
            const string expectedValue = "Good";

            Assert.Equal(expectedValue, returnedValue);
        }
    }

    // ReSharper restore DelegateSubtraction
#pragma warning restore 183
}