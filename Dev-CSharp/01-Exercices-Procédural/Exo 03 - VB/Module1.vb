Module Module1

    Sub Main()
        CalculDivisionEntiere()
        Console.WriteLine()
        Console.WriteLine()
        VérificationBBAN()
        Console.WriteLine()
        Console.WriteLine()
        ConvertirBBANtoIBAN()
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

    Function GetBBANNumber() As String
        Dim numCompte As String, quit As Boolean, tmp As Integer
        Do
            Console.WriteLine("Veuillez entrer le numéro de compte à vérifier (au format 123-1234567-12) : ")
            numCompte = Console.ReadLine()

            quit = False
            If numCompte.Length = 14 Then
                For i As Integer = 0 To 13
                    Select Case i
                        Case 3, 11
                            quit = numCompte.Substring(i, 1).Equals("-")
                            i = IIf(Not quit, 14, i)
                        Case Else
                            quit = Integer.TryParse(numCompte.Substring(i, 1), tmp)
                            i = IIf(Not quit, 14, i)
                    End Select
                Next i
            End If
            If Not quit Then Console.WriteLine("Le numéro de compte entré n'est pas au bon format, veuillez recommencer svp.")
        Loop While (Not quit)

        Return numCompte
    End Function

    Sub CalculDivisionEntiere()
        Dim dividende As Integer, diviseur As Integer
        dividende = GetInteger("Veuillez entrer le nombre dividende : ")
        diviseur = GetInteger("Veuillez entrer le nombre diviseur : ")
        Console.WriteLine($"Le résultat de la division entière de {dividende} par {diviseur} vaut : {dividende / diviseur}")
        Console.WriteLine($"Le reste (modulo) de la division entière de {dividende} par {diviseur} vaut : {dividende Mod diviseur}")
        Console.WriteLine($"Le résultat de la division réelle de {dividende} par {diviseur} vaut : {CLng(dividende) / diviseur}")
    End Sub

    Sub VérificationBBAN()
        Dim numCompte As String, baseCompte As String, moduloCompte As String
        numCompte = GetBBANNumber()
        baseCompte = Left(numCompte, numCompte.Length - 3)
        baseCompte = baseCompte.Replace("-", "")
        moduloCompte = Right(numCompte, 2)
        Console.WriteLine(IIf(IIf(Long.Parse(baseCompte) Mod 97 = 0, 97, Long.Parse(baseCompte) Mod 97) = Integer.Parse(moduloCompte), "OK", "KO"))
    End Sub

    Sub ConvertirBBANtoIBAN()
        Dim numCompte As String, dirt As String, bankCode As String
        numCompte = GetBBANNumber()
        dirt = Right(numCompte, 2) & Right(numCompte, 2) & "111400"
        bankCode = (98 - Integer.Parse(Long.Parse(dirt) Mod 97)).ToString()
        Console.WriteLine($"BE{bankCode}{numCompte.Replace("-", "")}")
    End Sub

End Module
