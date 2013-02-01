using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace CSharpFeatures.CallContext
{
    public class CallContextDemo
    {
        public void Start()
        {
            System.Runtime.Remoting.Messaging.CallContext.SetData("p1", new Person1 { Name = "xhan" });
            System.Runtime.Remoting.Messaging.CallContext.SetData("p2", new Person2 { Name = "xhan" });

            System.Runtime.Remoting.Messaging.CallContext.LogicalSetData("name", 36);
            System.Runtime.Remoting.Messaging.CallContext.LogicalSetData("p2x", new Person1 { Name = "zltest" });
            Action action = PrintPerson;

            action.BeginInvoke(null, null);

            PrintPerson();

            Console.Read();
        }
        public static void PrintPerson()
        {
            Console.WriteLine("logicalData:" + System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("name"));
            Console.WriteLine("logicalData:" + (System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("p2x") as Person1));

            Person1 p1 = System.Runtime.Remoting.Messaging.CallContext.GetData("p1") as Person1;
            Person2 p2 = System.Runtime.Remoting.Messaging.CallContext.GetData("p2") as Person2;
            if (p1 != null)
            {
                Console.WriteLine("thread Id:" + Thread.CurrentThread.ManagedThreadId + " p1.Name:" + p1.Name);

            }
            else
            {
                Console.WriteLine("thread Id:" + Thread.CurrentThread.ManagedThreadId + " p1.Name:null");
            }
            if (p2 != null)
            {
                Console.WriteLine("thread Id:" + Thread.CurrentThread.ManagedThreadId + " p2.Name:" + p2.Name);
            }
            else
            {
                Console.WriteLine("thread Id:" + Thread.CurrentThread.ManagedThreadId + " p2.Name:null");
            }
        }

    }
    [Serializable]
    public class Person1
    {
        public string Name { get; set; }
        public override string ToString()
        {
            return "Person1:" + Name;
        }
    }
    [Serializable]
    public class Person2 : ILogicalThreadAffinative
    {
        public string Name { get; set; }
    }
}