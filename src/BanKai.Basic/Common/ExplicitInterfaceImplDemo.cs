namespace BanKai.Basic.Common
{
    internal interface ITextStream
    {
        string Read();
        void Write(string text);
    }

    internal class 
        ReadOnlyStream : ITextStream
    {
        private string m_storage = "This is the result";

        public string Read()
        {
            return m_storage;
        }

        void ITextStream.Write(string text)//这里写上ITextStream. 的方式，就不能直接用ReadOnlyStream的对象直接使用。只有是借口类型的时候才能调用此方法。 
            //作用就是不想让某方法直接暴露给该类的对象
        {
            m_storage = text;
        }
    }
}