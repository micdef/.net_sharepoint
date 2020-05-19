Module Module1

    Sub Main()
        GestionPoints()
        Console.ReadKey()
    End Sub

    Sub GestionPoints()

        'Déclaration des variables
        Dim tabPoint?(5, 5) As Point, p As Point

        'Remplissage des valeurs
        For i As Integer = 0 To 4
            For j As Integer = 0 To 4
                If i = j Then
                    p.x = i
                    p.y = j
                    tabPoint(i, j) = p
                End If
            Next j
        Next i

        'Affichage des valeurs
        For i As Integer = 0 To 4
            For j As Integer = 0 To 4
                If tabPoint(i, j).HasValue Then
                    Console.Write($"X:{tabPoint(i, j).Value.x + 1} - Y:{tabPoint(i, j).Value.y + 1}")
                Else
                    Console.Write(vbTab)
                End If
            Next
            Console.WriteLine()
        Next i
    End Sub

End Module
