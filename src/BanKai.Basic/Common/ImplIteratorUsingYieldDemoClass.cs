using System.Collections.Generic;

namespace BanKai.Basic.Common
{
#pragma warning disable 162
    // ReSharper disable HeuristicUnreachableCode
    // ReSharper disable LoopCanBeConvertedToQuery

    internal class ImplIteratorUsingYieldDemoClass
    {
        public IEnumerable<int> GetOneToTen()
        {
            for (int i = 1; i <= 10; ++i)
            {
                yield return i;//客户端每调用一次，yield return就返回一个值给客户端，是"按需供给"。
                //使用yield return为什么能保证每次循环遍历的时候从前一次停止的地方开始执行呢？
               // --因为，编译器会生成一个状态机来维护迭代器的状态。
            }
        }

        public IEnumerable<int> GetOneToThreeWithMultipleYields()
        {
            yield return 1;
            yield return 2;
            yield return 3;
        }

        public IEnumerable<int> GetOnToThreeButBreakingAtTwo()
        {
            yield return 1;
            yield return 2;
            yield break;
            yield return 3;
        }

        public IEnumerable<int> GetEvenNumber(IEnumerable<int> getOneToTen)
        {
            foreach (int numberInCollection in getOneToTen)
            {
                if (numberInCollection % 2 == 0)
                {
                    yield return numberInCollection;
                }
            }
        }
    }

    // ReSharper restore LoopCanBeConvertedToQuery
    // ReSharper restore HeuristicUnreachableCode
#pragma warning restore 162
}