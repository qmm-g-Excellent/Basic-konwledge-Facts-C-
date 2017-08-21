using System;
using System.Text;
using BanKai.Basic.Common;
using Xunit;

namespace BanKai.Basic
{
    public class Disposable
    {
        [Fact]
        public void should_call_dispose_anyway_using_try_finally()
        {
            var tracer = new StringBuilder();
            Console.WriteLine("ppp"+tracer + "oooo");

            DisposableWithTracingDemoClass demoDisposable = null;

            try
            {
                demoDisposable = new DisposableWithTracingDemoClass(tracer); //demoDisposable is a instance DisposeableWithTracingDemoClass class
            }
            finally
            {
                if (demoDisposable != null)
                {
                    demoDisposable.Dispose();
                }
            }

            // change variable value to fix test.
            const string expectedTracingMessage = "constructor called.\r\ndispose called.\r\n";

            Assert.Equal(expectedTracingMessage, tracer.ToString());
        }

        [Fact]
        public void should_use_using_statement_for_simplicity()
        {
            var tracer = new StringBuilder();

            using (var demoDisposable = new DisposableWithTracingDemoClass(tracer))
            {
                // blah, blah, ...
            }
            //定义一个范围，在范围结束时处理对象
            //在某个代码段中使用了某实例，离开这个代码段就自动调用这个类实例的Dispose方法

            // change the variable value to fix the test.
            const string expectedTracingMessage = "constructor called.\r\ndispose called.\r\n";

            Assert.Equal(expectedTracingMessage, tracer.ToString());
        }
    }
}