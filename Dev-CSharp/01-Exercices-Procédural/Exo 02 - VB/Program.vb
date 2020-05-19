Imports System

Module Program
    Sub Main(args As String())

        modulo()
        Console.ReadKey()

    End Sub

    Sub Modulo()

        Dim nb1 As Integer
        Dim quit As Boolean

        Do
            Console.Write("Entrez le nombre :")
            If (Not Integer.TryParse(Console.ReadLine(), nb1)) Then
                Console.WriteLine("Ce que vous avez entré pas de type entier.")
            Else
                quit = True
            End If
        Loop While (Not quit)

        'Mode ton exo
        Console.WriteLine("Exo [Version Cours]")
        Console.WriteLine("-------------------")
        Console.WriteLine()
        Console.WriteLine(IIf((nb1 / 2) * 2 = nb1, "Le nombre est pair", "le nombre est impair"))

        ' Mode modulo
        Console.WriteLine()
        Console.WriteLine()
        Console.WriteLine("Exo [Version Modulo]")
        Console.WriteLine("--------------------")
        Console.WriteLine()
        Console.WriteLine(IIf(nb1 Mod 2 = 0, "Le nombre est pair", "Le nombre est impair"))

    End Sub

End Module
