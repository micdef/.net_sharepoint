Module Module1

    Sub Main()
        PaquetCartesCours()
        Console.ReadKey()
        PaquetCartes()
        Console.ReadKey()
    End Sub

    Sub PaquetCartesCours()
        Dim paquet(52) As Carte, c As Carte, i As Integer = 0
        For Each val As Valeurs In System.Enum.GetValues(GetType(Valeurs))
            For Each col As Couleurs In System.Enum.GetValues(GetType(Couleurs))
                c.couleur = col
                c.valeur = val
                paquet(i) = c
                i += 1
            Next col
        Next val
        AffichePaquetCartes(paquet)
    End Sub

    Sub ConstruirePaquetCartes(ByRef paquet As Carte())
        Dim c As Carte, k As Integer = 0
        For i As Integer = 2 To 14
            For j As Integer = 1 To 4
                c.valeur = i
                c.couleur = j
                paquet(k) = c
                k += 1
            Next
        Next
    End Sub

    Sub AffichePaquetCartes(ByRef paquet As Carte())
        For Each c As Carte In paquet
            Console.WriteLine($"{c.valeur} de {c.couleur}")
        Next
    End Sub

    Sub PaquetCartes()
        Dim paquet(52) As Carte
        ConstruirePaquetCartes(paquet)
        AffichePaquetCartes(paquet)
    End Sub

End Module
