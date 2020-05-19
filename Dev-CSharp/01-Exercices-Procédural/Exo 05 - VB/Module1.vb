Imports System.Text.RegularExpressions

Module Module1

    Sub Main()
        NombresPremier()
        Console.WriteLine()
        Console.WriteLine()
        AdditionCharToChar()
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

    Function IsPremier(nb As Integer) As Boolean
        Dim estPremier As Boolean = True
        For i As Integer = 2 To nb - 1
            If nb Mod i = 0 Then estPremier = False
        Next i
        Return estPremier
    End Function

    Sub NombresPremier()
        Try
            'Déclaration des variables
            Dim x As Integer, msg As String = vbNullString
            Dim nbPremiers2 As ArrayList = New ArrayList()
            Dim nbPremiers As List(Of Integer) = New List(Of Integer)
            x = GetInteger("Veuillez entre jusqu'à quel nombre chercher les nombres premiers : ")

            'Ecriture Exercices sans les boucles for 
            Dim i As Integer = 0
            While i <= x
                If IsPremier(i) Then nbPremiers2.Add(i)
                i += 1
            End While

            'Affichage avec une for-each
            For Each n As Integer In nbPremiers2
                msg = msg & $"{n}, "
            Next
            Console.WriteLine(Left(msg, msg.Length - 2))

            'Ecriture en boucle for
            For k As Integer = 0 To x
                If IsPremier(k) Then nbPremiers.Add(k)
            Next k

            'Affichage avec une for-each
            For Each n As Integer In nbPremiers
                msg = msg & $"{n}, "
            Next
            Console.WriteLine(Left(msg, msg.Length - 2))
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Sub AdditionCharToChar()
        Try
            'Déclaration des variables
            Dim nb1 As String, nb2 As String, result As Decimal = 0, i As Integer
            Dim tab1 As Char(), tab2 As Char()

            'Récupération des valeurs
            nb1 = GetInteger("Veuillez entrer le premier nombre svp : ").ToString()
            nb2 = GetInteger("Veulilez entrer le second nombre svp : ").ToString()

            'Récupération et inversion des tableaux
            tab1 = nb1.ToCharArray()
            Array.Reverse(tab1)
            tab2 = nb2.ToCharArray()
            Array.Reverse(tab2)

            'Addition Caractère/Caractère
            i = 0
            If tab1.Length > tab2.Length Then
                For Each c As Char In tab1
                    If i < tab2.Length Then
                        result += Integer.Parse(c) * Math.Pow(10, i) + Integer.Parse(tab2(i)) * Math.Pow(10, i)
                    Else
                        result += Integer.Parse(c) * Math.Pow(10, i)
                    End If
                    i += 1
                Next
            Else
                For Each c As Char In tab2
                    If i < tab1.Length Then
                        result += Integer.Parse(c) * Math.Pow(10, i) + Integer.Parse(tab1(i)) * Math.Pow(10, i)
                    Else
                        result += Integer.Parse(c) * Math.Pow(10, i)
                    End If
                    i += 1
                Next
            End If

            'Affichage du résultat
            Console.WriteLine($"{nb1} + {nb2} = {result}")

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

End Module
