Imports System.Configuration.Assemblies

Module Module1

    Sub Main()
        Fibonacci()
        Console.WriteLine()
        Console.WriteLine()
        Factorielle()
        Console.WriteLine()
        Console.WriteLine()
        NombresPremier()
        Console.WriteLine()
        Console.WriteLine()
        TablesMultiplications()
        Console.WriteLine()
        Console.WriteLine()
        Comptage()
        Console.WriteLine()
        Console.WriteLine()
        RacineCarree()
        Console.WriteLine()
        Console.WriteLine()
        Console.ReadKey()
    End Sub

    Function GetInteger(textAffiche As String) As Integer
        Dim nb As Integer, quit As Boolean
        quit = False
        Do
            Console.Write(textAffiche)
            If Not Integer.TryParse(Console.ReadLine(), nb) Then
                Console.WriteLine("Le nombre entré n'est pas valide.")
            Else
                quit = True
            End If
        Loop While (Not quit)
        Return nb
    End Function

    Sub Fibonacci()
        Try
            Dim qte As Integer, nb1 As Decimal = 0, nb2 As Decimal = 1, nb3 As Decimal = nb1 + nb2, msg As String = vbNullString 'Avec le décimal possibilité de faire 141 itérations"
            qte = GetInteger("Veuillez entrer le nombre d'itération de Fibonacci souhaitée : ")
            If qte > 141 Then Throw New Exception("Le quantité entrée amène a des nombres ne pouvant être contenus dans les variables classiques. Le maximum d'itérations possible est de 141")
            msg = $"0, {nb1}, {nb2}, {nb3}"
            For i As Integer = 5 To qte
                nb1 = nb2
                nb2 = nb3
                nb3 = nb1 + nb2
                msg = msg & $", {nb3}"
            Next
            Console.WriteLine(msg)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Sub Factorielle()
        Try
            Dim nb As Integer, result As Decimal = 1
            nb = GetInteger("Veuillez entrer le nombe pour effectuer sa factorielle : ")
            For i As Integer = nb To 2 Step -1
                result = result * i
            Next i
            Console.WriteLine($"{nb}! = {result}")
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Sub NombresPremier()
        Try
            Dim x As Integer, estPremier As Boolean, msg As String = vbNullString
            x = GetInteger("Veuillez entre jusqu'à quel nombre chercher les nombres premiers : ")
            For i As Integer = 0 To x
                estPremier = True
                For j As Integer = 2 To i - 1
                    If i Mod j = 0 Then estPremier = False
                Next j
                If estPremier Then msg = msg & ", " & i
            Next i
            Console.WriteLine(msg.Substring(5))
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Sub TablesMultiplications()
        For i As Integer = 1 To 20
            For j As Integer = 1 To 5
                Console.Write(Format(j, "00") & "x" & Format(i, "00") & "=" & Format(j * i, "000") & vbTab)
            Next j
            Console.WriteLine()
        Next i
    End Sub

    Sub Comptage()
        For i As Double = 0.0 To 20.0 Step 0.1
            Console.WriteLine(i)
        Next i
    End Sub

    Sub RacineCarree()
        Dim a As Double = 1.0, b As Integer, fa As Double, e As Double = 1.0
        b = GetInteger("Veuillez entrer le nombre a calculer sa racine : ")
        While e > 0.001
            fa = (a + CDbl(b) / a) / 2
            If (fa > a) Then
                e = fa - a
            Else
                e = a - fa
            End If
            a = fa
        End While
        Console.WriteLine($"Valeur obtenue : {a}, son carré est {Math.Pow(a, 2)}")
    End Sub

End Module
