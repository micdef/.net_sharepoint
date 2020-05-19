Module Module1

    Sub Main()
        ConvertCelsiusToFahrenheit()
        Console.WriteLine()
        Console.WriteLine()
        ConvertFahreneitToCelsius()
        Console.WriteLine()
        Console.WriteLine()
        ResolutionSecondDegre()
        Console.ReadKey()
    End Sub

    Function GetDouble(textAffiche As String) As Integer
        Dim nb As Double, quit As Boolean
        quit = False
        Do
            Console.Write(textAffiche)
            If Not Double.TryParse(Console.ReadLine(), nb) Then
                Console.WriteLine("Le nombre entré n'est pas valide.")
            Else
                quit = True
            End If
        Loop While (Not quit)
        Return nb
    End Function

    Sub ConvertCelsiusToFahrenheit()
        Dim c As Celsius, f As Fahrenheit
        c.temperature = GetDouble("Veuillez entre votre températeur en °C : ")
        f.ConvertToFahrenheit(c)
        Console.WriteLine($"{c.temperature}°C = {f.temperature}°F")
    End Sub

    Sub ConvertFahreneitToCelsius()
        Dim c As Celsius, f As Fahrenheit
        f.temperature = GetDouble("Veuillez entre votre températeur en °F : ")
        c.ConvertToCelsius(f)
        Console.WriteLine($"{f.temperature}°F = {c.temperature}°C")
    End Sub

    Sub ResolutionSecondDegre()
        Dim sd As SeconDegre, msg As String = vbNullString, x1? As Double, x2? As Double
        sd.a = GetDouble("Veuillez entrer la valeur ""a"" de ax²+bx+c : ")
        sd.b = GetDouble("Veuillez entrer la valeur ""b"" de ax²+bx+c : ")
        sd.c = GetDouble("Veuillez entrer la valeur ""c"" de ax²+bx+c : ")
        msg = "Solution pour l'équation : " &
              IIf(sd.a.Equals(0), "", sd.a & "x²") &
              IIf(sd.b > 0, "+" & sd.b & "x", IIf(sd.b < 0, sd.b & "x", "")) &
              IIf(sd.c > 0, "+" & sd.c, IIf(sd.c < 0, sd.c, "")) & " : " & vbNewLine & vbNewLine
        If sd.Resoudre(x1, x2) Then
            If x1 = x2 Then
                msg = msg & $"Une seule solution réelle : x1 = x2 = {x1}"
            Else
                msg = msg & $"Deux solution réelles : x1 = {x1}, x2 = {x2}"
            End If
        Else
            msg = msg & "Aucune solution réelle"
        End If
        Console.WriteLine(msg)
    End Sub

End Module
