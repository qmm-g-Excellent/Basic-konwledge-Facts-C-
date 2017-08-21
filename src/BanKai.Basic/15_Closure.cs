using System;
using System.Collections.Generic;
using Xunit;

namespace BanKai.Basic
{
    // ReSharper disable ConvertToConstant.Local
    // ReSharper disable AccessToModifiedClosure
    // ReSharper disable LoopCanBeConvertedToQuery

    public class Closure
    {
        [Fact]
        public void should_capture_outer_variable()
        {
            int variableDeclaredOutsideAnonymousMethod = 1;

            Func<int> methodCapturingVariable = () => variableDeclaredOutsideAnonymousMethod;//使用了外部变量，不在本匿名函数的范围之内，就叫 闭包

            int returnedValue = methodCapturingVariable();

            // change variable value to correct test.
            const int expectedReturnedValue = 1;

            Assert.Equal(expectedReturnedValue, returnedValue);
        }

        [Fact]
        public void should_modify_outer_variable()
        {
            int outerVariable = 1;

            Action methodChangeVariableValue = () => outerVariable += 1;
            methodChangeVariableValue();
            // 函数执行完后，函数所在的栈就解退了，就会把变量outerVariable 放到堆上
            // change variable value to correct test.
            const int expectedOuterVariableValue = 2;

            Assert.Equal(expectedOuterVariableValue, outerVariable); 
        }

        [Fact]
        public void should_use_caution_when_capturing_outer_variable_in_loop()
        {
            var functionList = new List<Func<int>>();

            for (int i = 0; i < 3; ++i)
            {
                int a = i;
                functionList.Add(() => a);//i闭包，
            }

            //任务分解==》 估时

            int sum = 0;
            foreach (Func<int> func in functionList)
            {
                sum += func();
            }

            const int expectedSum = 9;

            Assert.Equal(expectedSum, sum);
        }
    }

    // ReSharper restore LoopCanBeConvertedToQuery
    // ReSharper restore AccessToModifiedClosure
    // ReSharper restore ConvertToConstant.Local
}