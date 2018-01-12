Public Class DateUtils
    Public Shared Function FormatItalianDateString(ByVal DateValue As DateTime, ByVal dateonly As Boolean, ByVal db As DB.DBType) As String

        If dateonly Then


            Select Case db

                Case PERSISTENCE.DB.DBType.Access
                    Return Format(DateValue, "MM/dd/yyyy")
                Case PERSISTENCE.DB.DBType.SqlServer
                    Return Format(DateValue, "dd/MM/yyyy")
                Case PERSISTENCE.DB.DBType.MySql
                    Return Format(DateValue, "yyyy-MM-dd")
                Case Else
                    Return ""
            End Select

        Else

            Select Case db
                Case PERSISTENCE.DB.DBType.Access
                    Dim s As String = Format(DateValue, "MM/dd/yyyy HH:mm:ss")
                    Return s.Replace(".", ":")
                Case PERSISTENCE.DB.DBType.SqlServer
                    Dim s As String = Format(DateValue, "dd/MM/yyyy HH:mm:ss")
                    Return s.Replace(".", ":")
                Case PERSISTENCE.DB.DBType.MySql
                    Dim s As String = Format(DateValue, "yyyy-MM-dd HH:mm:ss")
                    Return s.Replace(".", ":")
                Case Else
                    Return ""
            End Select


        End If


    End Function



    Public Shared Function OverlapWithDBCharacter(ByVal datevalue As String, ByVal db As DB.DBType) As String

        If db = PERSISTENCE.DB.DBType.Access Then
            Return "#" & datevalue & "#"
        End If

        Return "'" & datevalue & "'"

    End Function

End Class
