using System;

namespace BanKai.Basic.Common
{
    internal class CustomizeEventArgsDemoClass
    {
        public event EventHandler<GreetingEventArgs> Greeting;
        

        //Event是delegate的派生类，加上event后Greeting就变成了事件，所以只能Add和remove
        //不加Event时，delegate可以直接赋值

        public void Greet(string name)
        {
            OnGreeting("Hello " + name);
        }

        private void OnGreeting(string content)
        {
            EventHandler<GreetingEventArgs> handler = Greeting;
            if (handler != null)
            {
                handler(this, new GreetingEventArgs(content));
            }
        }
    }

    internal class GreetingEventArgs : EventArgs //实现EventArgs的派生类，目的是可以传递自己的参数
    {
        private readonly string m_greetingContent;

        public GreetingEventArgs(string greetingContent)
        {
            m_greetingContent = greetingContent;
        }

        public string GreetingContent
        {
            get { return m_greetingContent; }
        }
    }
}