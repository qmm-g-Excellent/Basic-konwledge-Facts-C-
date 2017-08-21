namespace BanKai.Basic.Common
{
    public class PolymorphismDemoClassBase
    {
        public virtual string VirtualMethod()
        {
            return "BaseClass"; // virtual 表示必须要加上才能被子类重写， java中默认所有的函数都是虚函数，可以被子类重写
        }
    }

    public class PolymorphismDemoClass : PolymorphismDemoClassBase
    {
        public override string VirtualMethod()
        {
            return "DerivedClass";
        }
    }
}