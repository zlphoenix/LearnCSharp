
Imports System.Linq.Expressions
Imports ExpressionSerialization

Module UnitTests
    Public Sub Test()
        Dim serializer = New ExpressionSerializer()

        Console.WriteLine(vbCrLf & " TEST - 2")
        Dim e2 As Expression(Of Func(Of Int16)) = Function() 1

        Dim xml2 = serializer.Serialize(e2.Body)
        Dim result2 = serializer.Deserialize(xml2)
        Console.WriteLine("{0} " & vbCrLf & "should be the same as" & vbCrLf & "{1}", e2.Body.ToString(), result2.ToString())

        Console.WriteLine(vbCrLf & " TEST - 3")
        Dim e3 As Expression(Of Func(Of ExpressionType)) = Function() ExpressionType.Add
        Dim xml3 = serializer.Serialize(e3.Body)
        Dim result3 = serializer.Deserialize(xml3)
        Console.WriteLine("{0} " & vbCrLf & "should be the same as" & vbCrLf & "{1}", e3.Body.ToString(), result3.ToString())

        Console.WriteLine(vbCrLf & " TEST - 4")
        Dim e4 As Expression(Of Func(Of Boolean)) = Function() True
        Dim xml4 = serializer.Serialize(e4.Body)
        Dim result4 = serializer.Deserialize(xml4)
        Console.WriteLine("{0} " & vbCrLf & "should be the same as" & vbCrLf & "{1}", e4.Body.ToString(), result4.ToString())

        Console.WriteLine(vbCrLf & " TEST - 5")
        Dim e5 As Expression(Of Func(Of Decimal, Decimal)) = Function(d) d + 1
        Dim xml5 = serializer.Serialize(e5.Body)
        Dim result5 = serializer.Deserialize(xml5)
        Console.WriteLine("{0} " & vbCrLf & "should be the same as" & vbCrLf & "{1}", e5.Body.ToString(), result5.ToString())

        Console.WriteLine(vbCrLf & " TEST - 6")
        Dim e6 As Expression(Of Func(Of Decimal, Decimal)) = Function(d) d + 1D
        Dim xml6 = serializer.Serialize(e6)
        Dim result6 = serializer.Deserialize(xml6)
        Console.WriteLine("{0} " & vbCrLf & "should be the same as" & vbCrLf & "{1}", e6.ToString(), result6.ToString())
        Console.WriteLine((CType(result6, Expression(Of Func(Of Decimal, Decimal))).Compile())(3))

        Console.WriteLine(vbCrLf & " TEST - 7")
        Dim e7 As Expression(Of Func(Of String, Integer)) = Function(s) Integer.Parse(s)
        Dim xml7 = serializer.Serialize(e7)
        Dim result7 = serializer.Deserialize(xml7)
        Console.WriteLine("{0} " & vbCrLf & "should be the same as" & vbCrLf & "{1}", e7.ToString(), result7.ToString())
        Console.WriteLine((CType(result7, Expression(Of Func(Of String, Integer))).Compile())("1234"))

        Console.WriteLine(vbCrLf & " TEST - 8")
        Dim e8 As Expression(Of Func(Of String, String)) = Function(s) s.PadLeft(4)
        Dim xml8 = serializer.Serialize(e8)
        Dim result8 = serializer.Deserialize(xml8)
        Console.WriteLine("{0} " & vbCrLf & "should be the same as" & vbCrLf & "{1}", e8.ToString(), result8.ToString())
        Console.WriteLine((CType(result8, Expression(Of Func(Of String, String))).Compile())("1"))

        Console.WriteLine(vbCrLf & " TEST - 9")
        Dim e9 As Expression(Of Func(Of String, Integer)) = Function(s) Foo(Of String, Integer)(s, 1)
        Dim xml9 = serializer.Serialize(e9)
        Dim result9 = serializer.Deserialize(xml9)
        Console.WriteLine("{0} " & vbCrLf & "should be the same as" & vbCrLf & "{1}", e9.ToString(), result9.ToString())
        Console.WriteLine((CType(result9, Expression(Of Func(Of String, Integer))).Compile())("abcdac"))

        Console.WriteLine(vbCrLf & " TEST - 10")
        Dim e10 As Expression(Of Func(Of String, Char())) = Function(s) s.Where(Function(c) c <> "a"c).ToArray()
        Dim xml10 = serializer.Serialize(e10)
        Dim result10 = serializer.Deserialize(xml10)
        Console.WriteLine("{0} " & vbCrLf & "should be the same as" & vbCrLf & "{1}", e10.ToString(), result10.ToString())
        Console.WriteLine((CType(result10, Expression(Of Func(Of String, Char()))).Compile())("abcdac"))

        Console.WriteLine(vbCrLf & " TEST - 11")
        Dim e11 As Expression(Of Func(Of String, Char())) = _
            Function(s) _
                (From c In s _
                 Where c <> "a"c _
                 Select CChar(c & 1)).ToArray()

        Dim xml11 = serializer.Serialize(e11)
        Dim result11 = serializer.Deserialize(xml11)
        Console.WriteLine("{0} " & vbCrLf & "should be the same as" & vbCrLf & "{1}", e11.ToString(), result11.ToString())
        Console.WriteLine((CType(result11, Expression(Of Func(Of String, Char()))).Compile())("abcdac"))

        Console.WriteLine(vbCrLf & " TEST - 12")
        Dim e12 As Expression(Of Func(Of Integer, IEnumerable(Of Order()))) = _
         Function(n) _
                From c In GetCustomers() _
                Where (c.ID < n) _
                Select c.Orders.ToArray()

        Dim xml12 = serializer.Serialize(e12)
        Dim result12 = serializer.Deserialize(xml12)
        Console.WriteLine("{0} " & vbCrLf & "should be the same as" & vbCrLf & "{1}", e12.ToString(), result12.ToString())
        Console.WriteLine((CType(result12, Expression(Of Func(Of Integer, IEnumerable(Of Order())))).Compile())(5))

        Console.WriteLine(vbCrLf & " TEST - 13")
        Dim e13 As Expression(Of Func(Of List(Of Integer))) = Function() New List(Of Integer)(New Integer() {1, 2, 3})
        Dim xml13 = serializer.Serialize(e13)
        Dim result13 = serializer.Deserialize(xml13)
        Console.WriteLine("{0} " & vbCrLf & "should be the same as" & vbCrLf & "{1}", e13.ToString(), result13.ToString())
        Console.WriteLine((CType(result13, Expression(Of Func(Of List(Of Integer)))).Compile())())

        Console.WriteLine(vbCrLf & " TEST - 14")
        Dim e14 As Expression(Of Func(Of List(Of List(Of Integer)))) = Function() New List(Of List(Of Integer))(New List(Of Integer)() {New List(Of Integer)(New Integer() {1, 2, 3}), _
                                                                                                                                        New List(Of Integer)(New Integer() {2, 3, 4}), _
                                                                                                                                        New List(Of Integer)(New Integer() {3, 4, 5})})
        Dim xml14 = serializer.Serialize(e14)
        Dim result14 = serializer.Deserialize(xml14)
        Console.WriteLine("{0} " & vbCrLf & "should be the same as" & vbCrLf & "{1}", e14.ToString(), result14.ToString())
        Console.WriteLine((CType(result14, Expression(Of Func(Of List(Of List(Of Integer))))).Compile())())

        Console.WriteLine(vbCrLf & " TEST - 15")
        Dim e15 As Expression(Of Func(Of Customer)) = Function() New Customer() With {.Name = "Bob", .Orders = New List(Of Order)(New Order() {New Order() With {.OrderInfo = New OrderInfo With {.TrackingNumber = 123}, .ID = "12", .Quantity = 2}})}
        Dim xml15 = serializer.Serialize(e15)
        Dim result15 = serializer.Deserialize(xml15)
        Console.WriteLine("{0} " & vbCrLf & "should be the same as" & vbCrLf & "{1}", e15.ToString(), result15.ToString())
        Console.WriteLine((CType(result15, Expression(Of Func(Of Customer))).Compile())())

        Console.WriteLine(vbCrLf & " TEST - 16")
        Dim e16 As Expression(Of Func(Of Boolean, Integer)) = Function(b) If(b, 1, 2)
        Dim xml16 = serializer.Serialize(e16)
        Dim result16 = serializer.Deserialize(xml16)
        Console.WriteLine("{0} " & vbCrLf & "should be the same as" & vbCrLf & "{1}", e16.ToString(), result16.ToString())
        Console.WriteLine((CType(result16, Expression(Of Func(Of Boolean, Integer))).Compile())(False))

        Console.WriteLine(vbCrLf & " TEST - 17")
        Dim e17 As Expression(Of Func(Of Integer, Integer())) = Function(n) New Integer() {n}
        Dim xml17 = serializer.Serialize(e17)
        Dim result17 = serializer.Deserialize(xml17)
        Console.WriteLine("{0} " & vbCrLf & "should be the same as" & vbCrLf & "{1}", e17.ToString(), result17.ToString())
        Console.WriteLine((CType(result17, Expression(Of Func(Of Integer, Integer()))).Compile())(7))

        Console.WriteLine(vbCrLf & " TEST - 18")
        Dim e18 As Expression(Of Func(Of Integer, Integer())) = Function(n) New Integer(n) {}
        Dim xml18 = serializer.Serialize(e18)
        Dim result18 = serializer.Deserialize(xml18)
        Console.WriteLine("{0} " & vbCrLf & "should be the same as" & vbCrLf & "{1}", e18.ToString(), result18.ToString())
        Console.WriteLine((CType(result18, Expression(Of Func(Of Integer, Integer()))).Compile())(7))

        Console.WriteLine(vbCrLf & " TEST - 19")
        Dim e19 As Expression(Of Func(Of Object, String)) = Function(o) CStr(o)
        Dim xml19 = serializer.Serialize(e19)
        Dim result19 = serializer.Deserialize(xml19)
        Console.WriteLine("{0} " & vbCrLf & "should be the same as" & vbCrLf & "{1}", e19.ToString(), result19.ToString())
        Console.WriteLine((CType(result19, Expression(Of Func(Of Object, String))).Compile())(7))

        Console.WriteLine(vbCrLf & " TEST - 20")
        Dim e20 As Expression(Of Func(Of Object, Boolean)) = Function(o) TypeOf o Is String
        Dim xml20 = serializer.Serialize(e20)
        Dim result20 = serializer.Deserialize(xml20)
        Console.WriteLine("{0} " & vbCrLf & "should be the same as" & vbCrLf & "{1}", e20.ToString(), result20.ToString())
        Console.WriteLine((CType(result20, Expression(Of Func(Of Object, Boolean))).Compile())(7))

        Console.WriteLine(vbCrLf & " TEST - 21")
        Dim e21 As Expression(Of Func(Of IEnumerable(Of String))) = Function() From m In GetType(String).GetMethods() _
                                                                               Where Not m.IsStatic() _
                                                                                Group m By Key = m.Name Into g = Group _
                                                                                Select Key & g.Count().ToString()

        Dim xml21 = serializer.Serialize(e21)
        Dim result21 = serializer.Deserialize(xml21)
        Console.WriteLine("{0} " & vbCrLf & "should be the same as" & vbCrLf & "{1}", e21.ToString(), result21.ToString())
        Console.WriteLine((CType(result21, Expression(Of Func(Of IEnumerable(Of String)))).Compile())())

        Console.WriteLine(vbCrLf & " TEST - 22 (may take a while)")
        Dim e22 As Expression(Of Func(Of IEnumerable(Of Integer))) = Function() _
                                                                         From a In Enumerable.Range(1, 13) _
                                                                         Join b In Enumerable.Range(1, 13) On 4 * a Equals b _
                                                                         From c In Enumerable.Range(1, 13) _
                                                                         Join d In Enumerable.Range(1, 13) On 5 * c Equals d _
                                                                         From e In Enumerable.Range(1, 13) _
                                                                         Join f In Enumerable.Range(1, 13) On 3 * e Equals 2 * f _
                                                                         Join g In Enumerable.Range(1, 13) On 2 * (c + d) Equals 3 * g _
                                                                         From h In Enumerable.Range(1, 13) _
                                                                         Join i In Enumerable.Range(1, 13) On 3 * h - 2 * (e + f) Equals 3 * i _
                                                                         From j In Enumerable.Range(1, 13) _
                                                                         Join k In Enumerable.Range(1, 13) On 3 * (a + b) + 2 * j - 2 * (g + c + d) Equals k _
                                                                         From l In Enumerable.Range(1, 13) _
                                                                         Join m In Enumerable.Range(1, 13) On (h + i + e + f) - l Equals 4 * m _
                                                                         Where (4 * (l + m + h + i + e + f) = 3 * (j + k + g + a + b + c + d)) _
                                                                         Select a + b + c + d + e + f + g + h + i + j + k + l + m

        Dim xml22 = serializer.Serialize(e22)
        Dim result22 = serializer.Deserialize(xml22)
        Console.WriteLine("{0} " & vbCrLf & "should be the same as" & vbCrLf & "{1}", e22.ToString(), result22.ToString())
        Console.WriteLine((CType(result22, Expression(Of Func(Of IEnumerable(Of Integer)))).Compile())().FirstOrDefault())

        Console.WriteLine(vbCrLf & " TEST - 23")
        Dim e23 As Expression(Of Func(Of Integer, Integer)) = Function(n) CType(Function(x) x + 1, Func(Of Integer, Integer))(n)
        Dim xml23 = serializer.Serialize(e23)
        Dim result23 = serializer.Deserialize(xml23)
        Console.WriteLine("{0} " & vbCrLf & "should be the same as" & vbCrLf & "{1}", e23.ToString(), result23.ToString())
        Console.WriteLine((CType(result23, Expression(Of Func(Of Integer, Integer))).Compile())(7))


        Console.WriteLine(vbCrLf & " TEST - 24")
        Dim e24 As Expression(Of Func(Of IEnumerable(Of Integer))) = Function() From x In Enumerable.Range(1, 10) _
                                                       From y In Enumerable.Range(1, 10) _
                                                       Where x < y _
                                                       Select x * y

        Dim xml24 = serializer.Serialize(e24)
        Dim result24 = serializer.Deserialize(xml24)
        Console.WriteLine("{0} " & vbCrLf & "should be the same as" & vbCrLf & "{1}", e24.ToString(), result24.ToString())
        Console.WriteLine((CType(result24, Expression(Of Func(Of IEnumerable(Of Integer)))).Compile())())

        Console.WriteLine(vbCrLf & " TEST - 25")
        Dim e25 As Expression(Of Func(Of DateTime)) = Function() New DateTime(10000)
        Dim xml25 = serializer.Serialize(e25)
        Dim result25 = serializer.Deserialize(xml25)
        Console.WriteLine("{0} " & vbCrLf & "should be the same as" & vbCrLf & "{1}", e25.ToString(), result25.ToString())
        Console.WriteLine((CType(result25, Expression(Of Func(Of DateTime))).Compile())())
    End Sub

    Public Function Foo(Of T)(ByVal t1 As T) As Integer
        Return 1
    End Function

    Public Function Foo(Of T, U)(ByVal t1 As T, ByVal u1 As U) As Integer
        Return 2
    End Function

    Public Function GetCustomers() As IEnumerable(Of Customer)
        Return New Customer() { _
            New Customer() With { _
                .ID = 0, _
                .Name = "Bob", _
                .Orders = New List(Of Order)(New Order() { _
                    New Order() With { _
                        .ID = "0", _
                        .Quantity = 5 _
                    }, _
                    New Order() With { _
                        .ID = "1", _
                        .Quantity = 123 _
                    }})}, _
            New Customer() With { _
                .ID = 1, _
                .Name = "Dave", _
                .Orders = New List(Of Order)(New Order() { _
                    New Order() With { _
                        .ID = "0", _
                        .Quantity = 5 _
                    }, _
                    New Order() With { _
                        .ID = "1", _
                        .Quantity = 199 _
                    }})} _
        }
    End Function

    Public Class Customer
        Public ID As Integer
        Public Property Name() As String
            Get

            End Get
            Set(ByVal value As String)

            End Set
        End Property

        Public Property Orders() As List(Of Order)
            Get

            End Get
            Set(ByVal value As List(Of Order))

            End Set
        End Property

        Public Sub New()
            Orders = New List(Of Order)()
        End Sub
    End Class

    Public Class Order
        Public Property ID() As String
            Get
            End Get
            Set(ByVal value As String)
            End Set
        End Property

        Public Property Quantity() As Integer
            Get
            End Get
            Set(ByVal value As Integer)
            End Set
        End Property

        Public Property OrderInfo() As OrderInfo
            Get
            End Get
            Set(ByVal value As OrderInfo)
            End Set
        End Property
        Public Sub New()
            OrderInfo = New OrderInfo()
        End Sub
    End Class

    Public Class OrderInfo
        Public Property TrackingNumber() As Integer
            Get

            End Get
            Set(ByVal value As Integer)

            End Set
        End Property
    End Class
End Module
