Public Class Utilidades
    Public Shared Function DatasetVacio(ByVal ds As DataSet) As Boolean
        If ds Is Nothing Then
            Return True
        End If
        If ds.Tables.Count = 0 Then
            Return True
        End If
        If ds.Tables(0).Rows.Count = 0 Then
            Return True
        End If
        Return False
    End Function

End Class
