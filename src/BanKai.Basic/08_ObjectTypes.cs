using System;
using System.Collections.Generic;
using BanKai.Basic.Common;
using Xunit;

namespace BanKai.Basic
{
    // ReSharper disable ConvertToConstant.Local
    // ReSharper disable PossibleInvalidCastException


   












    public class ObjectTypes
    {
        [Fact]
        public void all_types_are_derived_from_object()
        {
            var stringInstance = "a string";
            var annonymousInstance = new { };//匿名类
            var valueTypeInstance = 2;

            // change the variable values for the following 3 lines to fix the test.
            const bool isStringInstanceObject = true;
            const bool isAnnonymousInstanceObject = true;
            const bool isValueTypeInstanceObject = true;

            Assert.Equal(
                isStringInstanceObject,
                stringInstance.GetType().IsSubclassOf(typeof(object)));//IsSubclassOf()表示是否是派生类
            Assert.Equal(
                isAnnonymousInstanceObject,
                annonymousInstance.GetType().IsSubclassOf(typeof(object)));
            Assert.Equal(
                isValueTypeInstanceObject,
                valueTypeInstance.GetType().IsSubclassOf(typeof(object)));
        }

        [Fact]
        public void should_cast_to_object_for_any_instance()
        {
            var objectList = new List<object> {"String", 2, new RefTypeClass(2)};//泛型，可以表示多种类型的数据，
            //new String[]. 这种方式和上面的定义数组的方式是不同的，上面定义数组后的类型是List, 可以有很多自己的api使用，这种方式就没有

            object itemAtPosition0 = objectList[0];
            object itemAtPosition1 = objectList[1];
            object itemAtPosition2 = objectList[2];

            // change the variable values for the following 3 lines to fix the test.
            Type expectedTypeForItemAtPosition0 = typeof(String);
            Type expectedTypeForItemAtPosition1 = typeof(int);
            Type expectedTypeForItemAtPosition2 = typeof(RefTypeClass);

            Assert.Equal(expectedTypeForItemAtPosition0, itemAtPosition0.GetType());
            Assert.Equal(expectedTypeForItemAtPosition1, itemAtPosition1.GetType());
            Assert.Equal(expectedTypeForItemAtPosition2, itemAtPosition2.GetType());
        }

        [Fact]
        public void should_derived_from_value_type_for_value_type()
        {
            int intObject = 1;
            DateTime dateTimeObject = DateTime.Now;
            //DateTime是值类型，因为DateTime是结构体，而结构体继承自System.ValueType，属于值类型

            var customValueTypeObject = new ValueTypeDemoClass();// 因为ValueTypeDemoClass是struct类型的， 结构体是值类型

            // change the variable values for the following 3 lines to fix the test.
            const bool isIntObjectValueType = true;
            const bool isDateTimeObjectValueType = true;
            const bool isCustomValueTypeObjectValueType = true;

            Assert.Equal(
                isIntObjectValueType, 
                intObject.GetType().IsSubclassOf(typeof(ValueType)));// 
            Assert.Equal(
                isDateTimeObjectValueType,
                dateTimeObject.GetType().IsSubclassOf(typeof(ValueType)));
            Assert.Equal(
                isCustomValueTypeObjectValueType,
                customValueTypeObject.GetType().IsSubclassOf(typeof(ValueType)));
        }

        [Fact]
        public void should_be_sealed_for_value_type()
        {
            var customValueTypeObject = new ValueTypeDemoClass();//my:不能用 C# 重写 internal virtual 方法

            // change the variable value to fix the test.
            const bool isValueTypeSealed = true;

            Assert.Equal(isValueTypeSealed, customValueTypeObject.GetType().IsSealed); // IsSealed和java中的final相似，表示不能被继承
        }

        [Fact]
        public void should_perform_real_copy_operation_for_value_type()
        {
            var original = new ValueTypeDemoClass();

            ValueTypeDemoClass copy = original;

            //，值类型变量的赋值操作，仅仅是2个实际数据之间的复制。值类型变量中保存的是实际数据，在赋值的时候只是把数据复制一份，然后赋给另一个变量。
            //而引用类型变量的赋值操作，复制的是引用，即内存地址，由于赋值后二者都指向同一内存地址，所以改变其中一个，另一个也会跟着改变，二者就像绑定在了一起。
            bool isSameReference;

            unsafe
            {
                ValueTypeDemoClass* originalPtr = &original;
                ValueTypeDemoClass* copyPtr = &copy;

                isSameReference = originalPtr == copyPtr;
            }

        
            // change the variable value to fix the test.
            const bool expectedIsSameReference = false;

            Assert.Equal(expectedIsSameReference, isSameReference);
        }

        [Fact]
        public void should_as_if_nothing_different_occurrs_when_doing_boxing_operation()
        {
            int intObject = 1;
            var boxed = (object) intObject;//可以成功转换，编译器会默认装箱，但是它的类型还是没有改变。作用可能用于传递object类型的参数时，用到，自己会装箱
            // change the variable values for the following 3 lines to fix the test.
            Type expectedType = typeof(int);
            const bool isBoxedTypeSealed = true;
            const bool isValueType = true;

            Assert.Equal(expectedType, boxed.GetType());
            Assert.Equal(isBoxedTypeSealed, boxed.GetType().IsSealed);
            Assert.Equal(isValueType, boxed.GetType().IsValueType);
        }

        [Fact]
        public void should_make_explicity_cast_when_doing_unboxing_operation()
        {
            //double int ..这些是值类型，结构也是值类型，类是引用类型。
            //值类型与引用类型的区别在于存储空间不同，值类型存在栈，后进先出，引用类型在托管堆先进先出。
            
            int intObject = 1;
            var boxed = (object) intObject;
            long longObject = 0;
            Exception errorWhenCasting = null;

            try
            {
                longObject = (long) boxed;//因为上面装箱时装成了int，所有这里拆箱必须拆为int，才能赋值给long类型的变量
            }
            catch (Exception error)
            {
                errorWhenCasting = error;
            }

            // change the variable values for the following 3 lines to fix the test.
            const bool isExceptionOccurred = true;
            Type expectedExceptionType = typeof(InvalidCastException);
            const long expectedLongObjectValue = 0;

            Assert.Equal(isExceptionOccurred, (errorWhenCasting != null));
            Assert.Equal(expectedExceptionType, errorWhenCasting.GetType());
            Assert.Equal(expectedLongObjectValue, longObject);
        }

        [Fact]
        public void should_get_most_derived_type_when_call_get_type_method()
        {
            var derivedClassObject = new InheritMemberAccessDemoClass();
            var castedToBaseClass = (InheritMemberAccessDemoBaseClass)derivedClassObject;
            Type type = castedToBaseClass.GetType(); //???????????

            // change the variable value to fix the test.
            Type expectedType = typeof(InheritMemberAccessDemoClass);

            Assert.Equal(expectedType, type);
        }

        [Fact]
        public void should_print_object_type_if_no_override_is_available_for_to_string_method()
        {
            //C# 中的namespace是和文件名和路径没有任何关系
            var objectWithoutToStringOverride = new RefTypeClass(2);
            Console.WriteLine(objectWithoutToStringOverride);
            Console.WriteLine(objectWithoutToStringOverride.ToString());

            // change the variable value to fix the test.
            const string expectedToStringResult = "BanKai.Basic.Common.RefTypeClass";

            string toStringResult = objectWithoutToStringOverride.ToString();

            Assert.Equal(expectedToStringResult, toStringResult);
        }
    }

    // ReSharper restore PossibleInvalidCastException
    // ReSharper restore ConvertToConstant.Local
}