using System;
using System.Collections;
using System.Collections.Generic;
using BanKai.Basic.Common;
using BanKai.Basic.Extensions;
using Xunit;

namespace BanKai.Basic
{
    // ReSharper disable UnusedVariable
    // ReSharper disable LoopCanBeConvertedToQuery

    public class EnumerationRelated
    {
        [Fact]
        public void should_always_call_move_next_before_using_dereferenced_value()
        {
            var collection = new[] {1, 2, 3};

            IEnumerator enumerator = collection.GetEnumerator(); //getEnumerator()方法是获取枚举数
            //枚举数的初始位置是-1,所以要想获取第一个元素就必须执行MoveNext()方法. 
            //            Console.WriteLine(enumerator.Current);
            Action getCurrentValueWithoutMoveNext = () =>
            {
                object value = enumerator.Current;
            };

            Exception error = getCurrentValueWithoutMoveNext.RunAndGetUnhandledException();

            // change the variable value to fix the test.
            Type expectedExceptionType = typeof(InvalidOperationException);

            Assert.Equal(expectedExceptionType, error.GetType());
        }

        [Fact]
        public void should_return_true_if_not_iterating_to_the_end()
        {
            var collection = new List<string> {"good", "morning"};

            List<string>.Enumerator enumerator = collection.GetEnumerator();
            Console.WriteLine(enumerator);

            bool notEndOfIteration = enumerator.MoveNext();
            //MoveNext是把枚举数位置前进到集合的下一项的方法,它返回布尔值,指示新位置是有效位置还是已经超过了序列的尾部.如果是已经到达了尾部,则返回false. 
            //枚举数的初始位置是-1,所以要想获取第一个元素就必须执行MoveNext()方法. 
            //如果枚举数成功地推进到下一个元素，则为 true；如果枚举数越过集合的结尾，则为 false。


            // change the variable value to fix the test.
            const bool expected = true;

            Assert.Equal(expected, notEndOfIteration);
        }

        [Fact]
        public void should_get_value_using_current_property()
        {
            var collection = new LinkedList<int>(new[] {1, 2, 3});

            LinkedList<int>.Enumerator enumerator = collection.GetEnumerator();

            Console.WriteLine(enumerator.Current);//0

            enumerator.MoveNext();
            //所以MoveNext()方法就是将枚举数的位置移到下一个位置初始值为-1， 移动一个就是0.
            //Current返回序列中当前项的属性,它是一个只读属性.初始值为0. 返回object类型的引用,所以可以返回任何类型. 

            int currentValue = enumerator.Current;//枚举数位置为0的地方的值
           
            // change the variable value to fix the test.
            const int expectedCurrentValue = 1;

            Assert.Equal(expectedCurrentValue, currentValue);
        }

        [Fact]
        public void should_iterate_through_collection()
        {
            var collection = new SortedSet<int> {10, 2, 3, 5};

            var copyOfCollection = new List<int>();

            using (SortedSet<int>.Enumerator enumerator = collection.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Console.WriteLine(enumerator.Current);
                    copyOfCollection.Add(enumerator.Current);
                }
            }

            // change the variable value to fix the test.
            var expectedCopyResult = new List<int> { 2, 3, 5, 10};

            Assert.Equal(expectedCopyResult, copyOfCollection);
        }

        [Fact]
        public void should_be_simplified_using_for_each()
        {
            var collection = new SortedSet<int> { 10, 2, 3, 5 };//SortedSet<T>是一个排序的无重复数据集合
            Console.WriteLine(collection);
            var copyOfCollection = new List<int>();

            foreach (int valueInCollection in collection)
            {
                copyOfCollection.Add(valueInCollection);
            }

            // change the variable value to fix the test.
            var expectedCopyResult = new List<int> {  2, 3, 5,10 };

            Assert.Equal(expectedCopyResult, copyOfCollection);
        }

        [Fact]
        public void should_implement_iterator_using_yield()
        {
            var demoObject = new ImplIteratorUsingYieldDemoClass();
            var numberStorage = new List<int>();

            foreach (int number in demoObject.GetOneToTen())
            {
                numberStorage.Add(number);
            }

            // change the variable value to fix the test.
            var expectedNumberStorage = new List<int> {1,2,3,4,5,6,7,8,9,10};

            Assert.Equal(expectedNumberStorage, numberStorage);
        }

        [Fact]
        public void should_treat_yield_as_part_of_the_iteration_stream()
        {
            var demoObject = new ImplIteratorUsingYieldDemoClass();
            var numberStorage = new List<int>();

            foreach (int number in demoObject.GetOneToThreeWithMultipleYields())
            {
                numberStorage.Add(number);
            }

            // change the variable value to fix the test.
            var expectedNumberStorage = new List<int> {1,2,3};//多个yield return ？？？

            Assert.Equal(expectedNumberStorage, numberStorage);
        }

        [Fact]
        public void should_using_yield_break_to_stop_iterating_early()
        {
            var demoObject = new ImplIteratorUsingYieldDemoClass();
            var numberStorage = new List<int>();

            foreach (int number in demoObject.GetOnToThreeButBreakingAtTwo())
            {
                numberStorage.Add(number);
            }

            // change the variable value to fix the test.
            var expectedNumberStorage = new List<int> { 1, 2 };

            Assert.Equal(expectedNumberStorage, numberStorage);
        }

        [Fact]
        public void should_composing_iterating_sequences()
        {
            var demoObject = new ImplIteratorUsingYieldDemoClass();
            var numberStorage = new List<int>();

            foreach (int number in demoObject.GetEvenNumber(demoObject.GetOneToTen()))
            {
                numberStorage.Add(number);
            }

            // change the variable value to fix the test.
            var expectedNumberStorage = new List<int> {  2,  4,  6, 8, 10 };

            Assert.Equal(expectedNumberStorage, numberStorage);
        }
    }

    // ReSharper restore LoopCanBeConvertedToQuery
    // ReSharper restore UnusedVariable
}