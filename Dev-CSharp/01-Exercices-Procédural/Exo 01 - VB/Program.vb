Imports System

Module Program
    Sub Main(args As String())

        AdditionParse()
        AdditionTryParse()
        Console.ReadKey()

    End Sub

    Sub AdditionParse()

        Dim nb1, nb2 As Integer
        Console.Write("Entrez le premier nombre : ")
        nb1 = Integer.Parse(Console.ReadLine())
        Console.Write("Entrez le second nombre :")
        nb2 = Integer.Parse(Console.ReadLine())
        Console.WriteLine($"L'addition de {nb1} et {nb2} donne {nb1 + nb2}")

    End Sub

    Sub AdditionTryParse()

        Dim nb1, nb2 As Integer
        Dim quit As Boolean = False

        Do
            Console.Write("Entrez le premier nombre :")
            If (Not Integer.TryParse(Console.ReadLine(), nb1)) Then
                Console.WriteLine("Ce que vous avez entré pas de type entier.")
            Else
                quit = True
            End If
        Loop While (Not quit)

        Do
            Console.Write("Entrez le second nombre :")
            If (Not Integer.TryParse(Console.ReadLine(), nb2)) Then
                Console.WriteLine("Ce que vous avez entré pas de type entier.")
            Else
                quit = True
            End If
        Loop While (Not quit)

        Console.WriteLine($"L'addition de {nb1} et {nb2} donne {nb1 + nb2}")

    End Sub
End Module
