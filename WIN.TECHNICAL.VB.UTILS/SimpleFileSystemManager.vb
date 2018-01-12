Public Class SimpleFileSystemManager


   Public Shared Function GenerateConsistentFileName(ByVal path As String, ByVal Name As String) As String
      Dim outputFile As String
      Dim i As Int32 = 1

      outputFile = My.Computer.FileSystem.CombinePath(path, Name)
      Do While My.Computer.FileSystem.FileExists(outputFile)
         Dim temp As String = i.ToString & "-" & Name
         outputFile = My.Computer.FileSystem.CombinePath(path, temp)
         i = i + 1
         temp = ""
      Loop
      Return outputFile
   End Function

   Public Shared Function GetFileNameWithoutExtension(ByVal filename) As String

      Dim i As New IO.FileInfo(filename)

      If Not i.Exists Then
         Return ""
      End If

      Dim ext As String = i.Extension

      Dim name As String = i.Name


      Return name.Replace(ext, "")
   End Function

   Public Shared Sub ViewFile(ByVal FileName As String)

      If FileName = "" Then Throw New ArgumentException("Nome file vuoto")
      If Not System.IO.File.Exists(FileName) Then Throw New ArgumentException("File inesistente")
      Dim pInfo As New ProcessStartInfo()
      pInfo.FileName = FileName
      pInfo.UseShellExecute = True
      Dim p As Process = Process.Start(pInfo)

   End Sub


   Public Shared Function GetFileProperties(ByVal FileName As String) As String
      If Not My.Computer.FileSystem.FileExists(FileName) Then
         Throw New IO.FileNotFoundException
      End If
      Dim prop As New Text.StringBuilder
      Dim info As System.IO.FileInfo = My.Computer.FileSystem.GetFileInfo(FileName)

      prop.Append("Nome file: " & info.Name & vbCrLf)
      prop.Append("Nome directory: " & info.DirectoryName & vbCrLf & vbCrLf & vbCrLf)


      prop.Append("Data creazione: " & info.CreationTime & vbCrLf)
      prop.Append("Data ultimo accesso: " & info.LastAccessTime & vbCrLf)
      prop.Append("Data ultima modifica: " & info.LastWriteTime & vbCrLf)
      prop.Append("Sola lettura: " & info.IsReadOnly & vbCrLf)
      prop.Append("Spazio occupato: " & info.Length & " bytes" & vbCrLf)



      Return prop.ToString
   End Function


   Public Shared Sub ViewPath(ByVal FilePath As String)

      If FilePath = "" Then Throw New ArgumentException("Nome percorso vuoto")
      If Not My.Computer.FileSystem.DirectoryExists(FilePath) Then Throw New ArgumentException("Percorso inesistente")
      Dim pInfo As New ProcessStartInfo()
      pInfo.FileName = FilePath
      pInfo.UseShellExecute = True
      Dim p As Process = Process.Start(pInfo)

   End Sub

   Public Shared Sub RemoveFile(ByVal FileName As String)
      If My.Computer.FileSystem.FileExists(FileName) Then
         IO.File.Delete(FileName)
      End If
   End Sub

   Public Shared Sub RenameFile(ByVal oldName As String, ByVal NewName As String)
      If My.Computer.FileSystem.FileExists(oldName) Then
         My.Computer.FileSystem.RenameFile(oldName, NewName)
      Else
         Throw New IO.FileNotFoundException
      End If
   End Sub

   Public Shared Sub CopyFile(ByVal source As String, ByVal destination As String, ByVal Sovrascrivi As Boolean)
      My.Computer.FileSystem.MoveFile(source, destination, True)

   End Sub

   Public Shared Sub MoveFile(ByVal source As String, ByVal destination As String, ByVal Sovrascrivi As Boolean)
      My.Computer.FileSystem.MoveFile(source, destination, Sovrascrivi)
   End Sub
End Class
