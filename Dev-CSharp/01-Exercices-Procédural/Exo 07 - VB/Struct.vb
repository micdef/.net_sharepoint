Module Struct

    Public Structure Celsius
        Public temperature As Double
        Public Sub ConvertToCelsius(tempF As Fahrenheit)
            Me.temperature = (tempF.temperature - 32) * 5.0 / 9.0
        End Sub

    End Structure

    Public Structure Fahrenheit
        Public temperature As Double

        Public Sub ConvertToFahrenheit(tempC As Celsius)
            Me.temperature = tempC.temperature * 9.0 / 5.0 + 32
        End Sub
    End Structure

    Public Structure SeconDegre
        Public a As Double
        Public b As Double
        Public c As Double

        Public Function Resoudre(ByRef x1? As Double, ByRef x2? As Double) As Boolean
            Dim ro As Double = Math.Pow(Me.b, 2) + (4 * Me.a * Me.c)
            If ro < 0 Then
                x1 = Nothing
                x2 = Nothing
                Return False
            ElseIf ro = 0 Then
                x1 = -(Me.b / (2 * Me.a))
                x2 = x1
                Return True
            Else
                x1 = (-Me.b - Math.Sqrt(ro)) / (2 * Me.a)
                x2 = (-Me.b + Math.Sqrt(ro)) / (2 * Me.a)
                Return True
            End If
        End Function
    End Structure

End Module
