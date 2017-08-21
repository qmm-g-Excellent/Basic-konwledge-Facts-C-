using System;
using System.Text;
using Xunit;

namespace BanKai.Basic
{
    // ReSharper disable UnusedVariable
    // ReSharper disable ConvertToConstant.Local

    public class StringAndCharOperations
    {
        [Fact]
        public void should_concat_string()
        {
            const string title = "Mr. ";
            const string name = "Hall";

            // change "default(string)" to correct value.
            const string expectedResult ="Mr. Hall";

            Assert.Equal(expectedResult, (title + name));//不能再原来的基础上修改，必须重新分配内存来存储新的修改
        }

        [Fact]
        public void should_using_stringbuilder_to_concat_string_efficiently()
        {
            const string title = "Mr. ";
            const string name = "Hall";

            var builder = new StringBuilder();
            // add at most 2 lines of code here concating variable "title" and "name".
            builder.Append(title);//不用再重新创建新的内存，在原来的内存储上修改，超出时
            builder.Append(name);
            Assert.Equal("Mr. Hall", builder.ToString());
        }

        [Fact]
        public void should_create_a_new_string_for_replace_operation()
        {
            string originalString = "Original String";
            string replacement = originalString.Replace("Str", "W");//产生一个新的字符串，并且全部替换所有匹配的字符串

            // change "" in the following 2 lines to correct values.
            const string expectedOrignalString = "Original String";
            const string expectedReplacement = "Original Wing";
            
            Assert.Equal(expectedOrignalString, originalString);
            Assert.Equal(expectedReplacement, replacement);
        }

        [Fact]
        public void should_use_string_builder_for_inplace_string_replacement()
        {
            var builder = new StringBuilder("Original String");
            builder.Replace("Str", "W");//更改原来的字符串

            // change "" in the following line to correct value.
            const string expectedResult = "Original Wing";

            Assert.Equal(expectedResult, builder.ToString());
        }

        [Fact]
        public void should_locate_certain_character_using_indexer()
        {
            const string originalString = "Original String";
            char characterAtIndex2 = originalString[2];

            // change "default(char)" to correct value.
            const char expectedResult ='i';

            Assert.Equal(expectedResult, characterAtIndex2);
        }

        [Fact]
        public void should_compare_string_value()
        {
            const string str = "Original String";
            string equivalent = "Original" + " String";

            // change "default(bool)" to correct value.
            const bool expectedResult = true; //==比较的只是内容

            Assert.Equal(expectedResult, (str == equivalent));
        }

        [Fact]
        public void should_use_equal_method_to_test_equaility_in_a_more_flexible_way()
        {
            const string originalString = "Original String";
            const string inDifferentCase = "oRiginal String";//utf16编码方式，选择的字符集，导致选择的编码方式不同

            // change the variable values in the following 2 lines.
            var caseSensitiveComparison = StringComparison.InvariantCulture;//固定文化,敏感大小写
            var caseInsensitiveComparison = StringComparison.InvariantCultureIgnoreCase;//固定的文化，忽略大小写
           
            Assert.False(originalString.Equals(inDifferentCase, caseSensitiveComparison));
            Assert.True(originalString.Equals(inDifferentCase, caseInsensitiveComparison));
        }
    }

    // ReSharper restore ConvertToConstant.Local
    // ReSharper restore UnusedVariable
}