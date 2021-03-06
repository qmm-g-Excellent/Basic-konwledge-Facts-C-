﻿using System;
using System.Text;
using BanKai.Basic.Common;
using Xunit;

namespace BanKai.Basic
{
    // ReSharper disable EmptyGeneralCatchClause

    public class TryStatement
    {
        [Fact]
        public void should_catch_nothing_if_no_exception_is_thrown()
        {
            bool isCatchBlockCovered = false;

            try
            {
                new TryCatchDemoClass().WillNotThrowException();
            }
            catch (Exception)
            {
                isCatchBlockCovered = true;
            }

            // change the variable value to fix the test.
            const bool expectedCatchBlockCovered = false;

            Assert.Equal(expectedCatchBlockCovered, isCatchBlockCovered);
        }

        [Fact]
        public void should_catch_matched_exception()
        {
            bool isCatchBlockCovered = false;

            try
            {
                new TryCatchDemoClass().WillThrowFormatException();
            }
            catch (FormatException)
            {
                isCatchBlockCovered = true;
            }

            // change the variable value to fix the test.
            const bool expectedCatchBlockCovered = true;

            Assert.Equal(expectedCatchBlockCovered, isCatchBlockCovered);
        }

        [Fact]
        public void should_only_catch_mostly_matched_exception()
        {
            bool isFormatExceptionCatched = false;
            bool isExceptionCatched = false;

            try
            {
                new TryCatchDemoClass().WillThrowFormatException();
            }

            catch (FormatException)//按顺序走的，所有的异常都是继承至Exception
            {
                isFormatExceptionCatched = true;
            }
            catch (Exception)
            {
                isExceptionCatched = true;
            }


            // change the variable values for the following 2 lines to fix the test.
            const bool expectedFormatExceptionCatched = true;
            const bool expectedExceptionCatched = false;

            Assert.Equal(expectedFormatExceptionCatched, isFormatExceptionCatched);
            Assert.Equal(expectedExceptionCatched, isExceptionCatched);
        }

        [Fact]
        public void should_find_matched_catch_clause_through_inherit_chain()
        {
            bool isCatchBlockCovered = false;

            try
            {
                new TryCatchDemoClass().WillThrowFormatException();
            }
            catch (SystemException)//System.SystemException 类是所有预定义的系统异常的基类。
            {
                isCatchBlockCovered = true;
            }

            // change the variable value to fix the test.
            const bool expectedCatchBlockCovered = true;

            Assert.Equal(expectedCatchBlockCovered, isCatchBlockCovered);
        }

        [Fact]
        public void should_throw_exception_if_no_catch_matched()
        {
            Action noCatchMatched = () =>
            {
                try
                {
                    new TryCatchDemoClass().WillThrowFormatException();
                }
                catch (ArgumentException)
                {
                }
            };

            // change the variable value to fix the test.
            Type expectedExceptionType = typeof(FormatException);

            Assert.Throws(expectedExceptionType, () => noCatchMatched());
        }

        [Fact]
        public void should_run_finally_block_anyway()
        {
            var tracer = new StringBuilder();

            try
            {
                tracer.AppendLine("try block executed.");
                throw new FormatException();
            }
            catch (FormatException)
            {
                tracer.AppendLine("FormatException catched.");
            }
            finally
            {
                tracer.AppendLine("finally blocked executed.");
            }//如果不写catch，finally都会执行，执行完后，异常将无法被处理，代码就会在这里结束

            // change the variable value to fix the test.            
            const string expectedTracingMessage = "try block executed.\r\nFormatException catched.\r\nfinally blocked executed.\r\n";

            Assert.Equal(expectedTracingMessage, tracer.ToString());
        }
    }

    // ReSharper restore EmptyGeneralCatchClause
}