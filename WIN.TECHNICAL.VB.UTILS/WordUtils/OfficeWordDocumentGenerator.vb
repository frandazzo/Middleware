Public Class OfficeWordDocumentGenerator


   Public Shared Function CalculateWordFileExtensionLowLevel(ByVal nomeFile As String) As String
      Dim ext As String = ""
      If My.Computer.FileSystem.GetFileInfo(nomeFile).Extension = ".dot" Then
         ext = ".doc"
      End If
      If My.Computer.FileSystem.GetFileInfo(nomeFile).Extension = ".dotx" Then
         ext = ".docx"
      End If
      Return ext
   End Function

   Public Shared Function BuildWordFileNameLowLevel(ByVal modelName As String, ByVal destination As String, ByVal suggestedFileNameWithoutWxtension As String) As String
      Dim outputFile As String = ""
      Dim i As Int32 = 1

      If Not My.Computer.FileSystem.FileExists(modelName) Then
         Throw New ArgumentException("Nome modello non corretto")
      End If
      If Not My.Computer.FileSystem.DirectoryExists(destination) Then
         Throw New ArgumentException("Nome percorso di destinazione non inesistente")
      End If

      Dim ext As String = CalculateWordFileExtensionLowLevel(modelName)

      If ext = "" Then
         Throw New ArgumentException("Estensione file non valida")
      End If
      Dim name As String = IO.Path.GetFileNameWithoutExtension(modelName)

      name = suggestedFileNameWithoutWxtension + ext

      Dim path As String

      path = destination

      'outputFile = My.Computer.FileSystem.CombinePath(path, name)
      'Do While My.Computer.FileSystem.FileExists(outputFile)
      '   Dim temp As String = i.ToString & "-" & name
      '   outputFile = My.Computer.FileSystem.CombinePath(path, temp)
      '   i = i + 1
      '   temp = ""
      'Loop
      'Return outputFile
      Return SimpleFileSystemManager.GenerateConsistentFileName(path, name)
   End Function
   Public Shared Function GenerateAndSaveDocumentLowLevel(ByVal nomeFileModello As String, ByVal TagTable As Hashtable, ByVal DirectoryDestinazione As String, ByVal nomeFileSenzaEstensione As String) As String
      'Dim tagValues As Hashtable = CreateTagValuesMap(IdUtente, TagTable) '(Current.Utente.Id, TagTable)

      Dim wordHandler As New OfficeWordHandler

      Dim documento As String = ""
      wordHandler.OpenWordApplication(True)
      wordHandler.AddDocumentToWord(nomeFileModello)


      Dim enumerator As IDictionaryEnumerator = TagTable.GetEnumerator
      Do While enumerator.MoveNext
         wordHandler.FindReplace(enumerator.Key, enumerator.Value)
      Loop


      'lo salvo
      documento = BuildWordFileNameLowLevel(nomeFileModello, DirectoryDestinazione, nomeFileSenzaEstensione)
      wordHandler.SaveAs(documento)


      wordHandler.DocumentClose(False)
      wordHandler.QuitApplication()

      Return documento
   End Function

   Public Shared Sub GenerateAndActivateTempDocumentLowLevel(ByVal modello As String, ByVal TagTable As Hashtable)
      Dim wordHandler As New OfficeWordHandler
      wordHandler.OpenWordApplication(True)
      wordHandler.AddDocumentToWord(modello)

      Dim enumerator As IDictionaryEnumerator = TagTable.GetEnumerator
      Do While enumerator.MoveNext
         wordHandler.FindReplace(enumerator.Key, enumerator.Value)
      Loop
        wordHandler.ActivateDocument()
        wordHandler.Visible = True
   End Sub



   Public Shared Function GenerateDocumentAndPrintOnBackGroundLowLevel(ByVal nomeFileModello As String, ByVal TagTable As Hashtable, ByVal Salva As Boolean, ByVal DirectoryDestinazione As String, ByVal nomeFileSenzaEstensione As String) As String
      'Dim tagValues As Hashtable = CreateTagValuesMap(IdUtente, TagTable) '(Current.Utente.Id, TagTable)

      Dim wordHandler As New OfficeWordHandler

      Dim documento As String = ""
      wordHandler.OpenWordApplication(True)
      wordHandler.AddDocumentToWord(nomeFileModello)


      Dim enumerator As IDictionaryEnumerator = TagTable.GetEnumerator
      Do While enumerator.MoveNext
         wordHandler.FindReplace(enumerator.Key, enumerator.Value)
      Loop

      If Salva Then
         documento = BuildWordFileNameLowLevel(nomeFileModello, DirectoryDestinazione, nomeFileSenzaEstensione)
         wordHandler.SaveAs(documento)
      End If
      'lo salvo


        wordHandler.Print()
        ' wordHandler.KillAllWordProcesses()
        wordHandler.DocumentClose(False)
        wordHandler.QuitApplication()

      Return documento
   End Function


End Class
