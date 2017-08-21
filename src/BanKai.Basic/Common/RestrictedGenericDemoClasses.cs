namespace BanKai.Basic.Common
{
    internal class ValueTypeRestrictedGenericDemoClass<T>
        where T : struct 
    {
        public ValueTypeRestrictedGenericDemoClass()
        {
            // since all struct declared type has default constructor.
            Value = default(T);
        }

        public T Value { get; set; }
    }

    internal class RefTypeRestrictedGenericDemoClass<T>
        where T : class //限定T的类型
    {
        public RefTypeRestrictedGenericDemoClass()
        {
            Value = default(T);
        }

        public T Value { get; set; }
    }

    internal class DefaultCtorRestrictedGenericDemoClass<T>
        where T : new() // 表示限定T里面必须要有一个无参的构造函数
    {
        public DefaultCtorRestrictedGenericDemoClass()
        {
            Value = new T();
        }

        public T Value { get; set; }
    }

    internal class InterfaceRestrictedGenericDemoClass<T>
        where T : ITalkable, new()//必须实现这个接口
    {
        public override string ToString()
        {
            var talkable = new T();
            return talkable.Talk();
        }
    }

    internal class SayHelloByDefault
    {
        private readonly string m_value;

        public SayHelloByDefault()
        {
            m_value = "Hello";
        }

        public override string ToString()
        {
            return m_value;
        }
    }
}