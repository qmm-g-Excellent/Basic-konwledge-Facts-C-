using System;
using BanKai.Basic.Common;
using Xunit;

namespace BanKai.Basic
{
    public class EventsRelated
    {
        [Fact]
        public void event_is_subset_of_delegate_with_event_handler_type()
        {
            var demoObject = new BasicEventDemoClass();
            var eventIsCalled = false;

            EventHandler eventHandler = (sender, eventArgs) =>
            {
                eventIsCalled = true;
            };

            demoObject.Event += eventHandler;//只能add， remove。

            demoObject.TriggerEvent();

            // change the variable value to fix the test.
            const bool expectedIsEventCalled = true;

            Assert.Equal(expectedIsEventCalled, eventIsCalled);
        }

        [Fact]
        public void should_unbind_event()
        {
            var demoObject = new BasicEventDemoClass();
            var eventIsCalled = false;

            EventHandler eventHandler = (sender, eventArgs) =>
            {
                eventIsCalled = true;
            };

            demoObject.Event += eventHandler;//Add
            demoObject.Event -= eventHandler;//remove

            demoObject.TriggerEvent();

            // change the variable value to fix the test.
            const bool expectedIsEventCalled = false; //什么也没有做

            Assert.Equal(expectedIsEventCalled, eventIsCalled);
        }

        [Fact]
        public void should_be_able_to_customize_event_args()
        {
            var demoObject = new CustomizeEventArgsDemoClass();
            string greetingContent = string.Empty;

            EventHandler<GreetingEventArgs> eventHandler = (sender, eventArgs) =>
            {             
                greetingContent = eventArgs.GreetingContent;//  这里的eventArgs是GreetingEventArgs类的一个实例，具体请进入GreetingEventArgs中的看
            };//定义委托时使用泛型GreetingEventArgs的目的是为了封装，实现一个派生类GreetingEventArgs,来传递我们自己的参数

            demoObject.Greeting += eventHandler; //EventHandler是一个委托，只能add ，remove； 这里把上面那个匿名函数add给了Greeting委托

            demoObject.Greet("World");

            // change the variable value to fix the test.
            const string expectedContent = "Hello World";//其实就是带参数的函数的一种委托 ，函数调用

            Assert.Equal(expectedContent, greetingContent);
        }

        [Fact]
        public void should_customize_event_accessor()
        {
            var demoObject = new CustomizeEventAccessorDemoClass();

            // change the variable value to fix the test.
            var expectedExceptionType = typeof(ArgumentNullException);

            Assert.Throws(expectedExceptionType, () => demoObject.Event += null);
        }
    }
}