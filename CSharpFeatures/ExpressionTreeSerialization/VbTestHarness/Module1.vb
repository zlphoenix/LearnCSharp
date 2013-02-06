
Option Strict On

Module Module1
    Sub Main()
        BasicExpressionSerialization()
        ComplexExpressionSerializationSamples()
        DLinqQuerySerializationSamples()
        AcrossTheWireSerializationSamples()

        Console.WriteLine("Press enter to run Unit Tests...")
        Console.Read()

        Console.WriteLine("***RUNNING UNIT TESTS***")
        UnitTests.Test()

        Test()
    End Sub
End Module
