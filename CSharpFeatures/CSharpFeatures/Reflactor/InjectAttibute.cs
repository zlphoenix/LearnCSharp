using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace CSharpFeatures.Reflactor
{

    public class DemoClass
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.AppendPrivatePath("");
            //得到新类型
            Type myType = BuildTypeWithCustomAttributesOnMethod();

            //创建myType的实例
            object myInstance = Activator.CreateInstance(myType);

            //获取myType上应用的所有Attribute
            object[] customAttrs = myType.GetCustomAttributes(true);

            Console.WriteLine("Custom Attributes for Type 'MyType':" + "\n");
            object attrVal = null;

            foreach (object customAttr in customAttrs)
            {
                //获取ClassCreatorAttribute中的Creator属性值
                attrVal = typeof(ClassCreatorAttribute).InvokeMember("Creator", BindingFlags.GetProperty, null, customAttr, new object[] { });
                Console.WriteLine(String.Format("-- Attribute: [{0} = \"{1}\"]", customAttr, attrVal) + "\n");
            }

            Console.WriteLine("Custom Attributes for Method 'HelloWorld()' in 'MyType':" + "\n");
            //获取myType中的HelloWorld方法上的所有Attribute
            customAttrs = myType.GetMember("HelloWorld")[0].GetCustomAttributes(true);

            foreach (object customAttr in customAttrs)
            {
                //获取DateLastUpdatedAttribute的DateUpdated属性值
                attrVal = typeof(DateLastUpdatedAttribute).InvokeMember("DateUpdated", BindingFlags.GetProperty, null, customAttr, new object[] { });
                Console.WriteLine(String.Format("-- Attribute: [{0} = \"{1}\"]", customAttr, attrVal) + "\n");
            }

            Console.WriteLine("---" + "\n");
            //动态调用myType实例中的HelloWorld方法
            Console.WriteLine(myType.InvokeMember("HelloWorld", BindingFlags.InvokeMethod, null, myInstance, new object[] { }) + "\n");


            Console.ReadKey();
        }

        /// <summary>
        /// 创建一个应用了ClassCreatorAttribute、DateLastUpdatedAttribute的类型
        /// </summary>
        /// <returns></returns>
        public static Type BuildTypeWithCustomAttributesOnMethod()
        {

            AppDomain currentDomain = Thread.GetDomain();

            AssemblyName myAsmName = new AssemblyName();
            myAsmName.Name = "MyAssembly";

            //动态创建一个程序集
            AssemblyBuilder myAsmBuilder = currentDomain.DefineDynamicAssembly(myAsmName, AssemblyBuilderAccess.Run);

            //动态创建一个模块
            ModuleBuilder myModBuilder = myAsmBuilder.DefineDynamicModule("MyModule");

            //动态创建一个类型：MyType
            TypeBuilder myTypeBuilder = myModBuilder.DefineType("MyType", TypeAttributes.Public);

            //定义构造器参数
            Type[] ctorParams = new Type[] { typeof(string) };

            //获取构造器信息
            ConstructorInfo classCtorInfo = typeof(ClassCreatorAttribute).GetConstructor(ctorParams);

            //动态创建ClassCreatorAttribute
            CustomAttributeBuilder myCABuilder = new CustomAttributeBuilder(
                           classCtorInfo,
                           new object[] { "Joe Programmer" });

            //将上面动态创建的Attribute附加到(动态创建的)类型MyType
            myTypeBuilder.SetCustomAttribute(myCABuilder);

            //动态创建一个无返回值，无参数的，公有方法HelloWorld
            MethodBuilder myMethodBuilder = myTypeBuilder.DefineMethod("HelloWorld", MethodAttributes.Public, null, new Type[] { });

            ctorParams = new Type[] { typeof(string) };

            //获取DateLastUpdatedAttribute的构造函数信息
            classCtorInfo = typeof(DateLastUpdatedAttribute).GetConstructor(ctorParams);

            //动态创建DateLastUpdatedAttribute
            CustomAttributeBuilder myCABuilder2 = new CustomAttributeBuilder(
                           classCtorInfo,
                           new object[] { DateTime.Now.ToString() });

            //将上面动态创建的Attribute附加到(动态创建的)方法HelloWorld
            myMethodBuilder.SetCustomAttribute(myCABuilder2);

            ILGenerator myIL = myMethodBuilder.GetILGenerator();
            myIL.EmitWriteLine("Hello, world!");//在HelloWorld方法中，创建一行等效于Console.Write("Hello,world!");的代码
            myIL.Emit(OpCodes.Ret);//HelloWorld方法的return语句

            return myTypeBuilder.CreateType();

        }


    }



    /// <summary>
    /// 创建一个自定义的Attribute，稍后将它应用在动态创建的“类型”上
    /// </summary>
    public class ClassCreatorAttribute : Attribute
    {
        private string creator;
        public string Creator
        {
            get
            {
                return creator;
            }
        }

        public ClassCreatorAttribute(string name)
        {
            this.creator = name;
        }

    }

    /// <summary>
    /// 创建一个自定义的Attribute，稍后将它应用在动态创建的“方法”上
    /// </summary>
    public class DateLastUpdatedAttribute : Attribute
    {
        private string dateUpdated;
        public string DateUpdated
        {
            get
            {
                return dateUpdated;
            }
        }

        public DateLastUpdatedAttribute(string theDate)
        {
            this.dateUpdated = theDate;
        }

    }


}