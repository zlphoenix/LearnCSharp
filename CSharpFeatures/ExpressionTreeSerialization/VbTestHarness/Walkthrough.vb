Option Strict On

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Xml.Linq
Imports ExpressionSerialization
Imports RemoteQueryService

Module Walkthrough
    ' Very simple serialization example
    Sub BasicExpressionSerialization()
        Console.WriteLine("BASIC SAMPLE - Serialize/Deserialize Simple Expression:")

        Dim addExpr As Expression(Of Func(Of Integer, Integer, Integer)) = Function(x, y) x + y
        Dim serializer As New ExpressionSerializer()
        Dim addXml = serializer.Serialize(addExpr)
        Dim addExpResult = serializer.Deserialize(Of Func(Of Integer, Integer, Integer))(addXml)
        Dim addExpResultFunc = addExpResult.Compile()
        Dim result = addExpResultFunc(1, 2)  ' evaluates to 3

        Console.WriteLine("Deserialized Expression Tree:")
        Console.WriteLine(" " & addExpResult.ToString())
        Console.WriteLine()
    End Sub

    ' Serializing an expression tree representing a query expression
    Sub ComplexExpressionSerializationSamples()
        Console.WriteLine("COMPLEX SAMPLE - Serialize/Deserialize In-Memory Query Expression:")

        Dim queryExp As Expression(Of Func(Of IEnumerable(Of Integer))) = Function() From i In Enumerable.Range(1, 10) _
                                                                Where i Mod 2 = 0 _
                                                                Select i * i

        Dim serializer = New ExpressionSerializer()
        Dim queryXml = serializer.Serialize(queryExp)
        Dim queryExpResult = serializer.Deserialize(Of Func(Of IEnumerable(Of Integer)))(queryXml)

        ' Print out the expression tree: "(x, y) => x + y"
        Console.WriteLine("Deserialized Expression Tree:")
        Console.WriteLine(" " + queryExpResult.ToString())

        ' Call it
        Dim f = queryExpResult.Compile()
        Dim result = f()
        Console.WriteLine("\nResults: ")
        For Each item In result
            Console.WriteLine(" " & item)
        Next
        Console.WriteLine()
    End Sub


    ' Example of scenario such as storing a query in a database and retreiving it 
    ' later into the same object model.
    Sub DLinqQuerySerializationSamples()
        Console.WriteLine("DLINQ BASIC SAMPLE - Single Object Model used on both sides of serialization:")

        ' Write the query against RemoteTable<Customer>, a dummy implementation 
        ' of IQueryable
        Dim customers = New RemoteTable(Of Customer)()

        Dim query = From c In customers _
                        Where c.City = "London" _
                        Select c.CompanyName, c

        'Dim query = From c In customers Let j = c _
        '            Where c.City = "London" _
        '            Order By c.CustomerID _
        '            Select (From o In c.Orders Select o.OrderDate).First(), c

        Dim queryXml = query.SerializeQuery()

        ' On the other side - create a new DataContext, and deserialize 
        ' the query xml into a query against this datacontext
        Dim dbOther = New NorthwindDataContext()
        Dim queryAfter = dbOther.DeserializeQuery(queryXml)

        ' Print out the IQueryable: "(x, y) => x + y"
        Console.WriteLine("Deserialized IQueryable:")
        Console.WriteLine(" " + queryAfter.ToString())

        Console.WriteLine("\n Results: ")
        For Each item In queryAfter
            Console.WriteLine(" " + item.ToString())
        Next
        Console.WriteLine()
    End Sub


    ' Example of querying using LINQ against a LINQ to SQL implementation hidden behind a WCF service
    ' Note that no database is being directly referenced - all types and calls are proxies
    Sub AcrossTheWireSerializationSamples()
        Console.WriteLine("DLINQ ACROSS THE WIRE SAMPLE - Query against an IQueryable wrapper over a web service:")

        ' Query is against a RemoteTable which is a proxy for the the WCF service which executes the DLinq query
        ' on the server.  Note that the elements are the service-reference generated types that align with the 
        ' DLinq mapping types via the DataContracts.

        Dim queryService = New RemoteTable(Of RemoteQueryService.ServiceReference.Customer)()

        'Dim query = From c In queryService _
        '            Where c.City = "London" _
        '            Order By c.CustomerID _
        '            Select (From o In c.Orders Select o.OrderDate).First(), c

        Dim query = From c In queryService _
                        Where c.City = "London" _
                        Order By c.CustomerID _
                        Select (From o In c.Orders _
                                Select o.OrderDate).First()


        Console.WriteLine("Query Results: ")

        For Each c In query
            Console.WriteLine(" " & c.Value.ToShortDateString())
        Next
    End Sub
End Module
