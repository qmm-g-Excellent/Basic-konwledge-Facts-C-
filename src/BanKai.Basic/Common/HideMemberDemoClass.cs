namespace BanKai.Basic.Common
{
    internal class HideMemberDemoClassBase
    {
        public string MethodToHide()
        {
            return "HideMemberDemoClassBase::MethodToHide()";
        }
    }

    internal class HideMemberDemoClass : HideMemberDemoClassBase
    {
        public new string MethodToHide()//这里的new就会打断继承链
        {
            return "HideMemberDemoClass::MethodToHide()";
        }
    }
}