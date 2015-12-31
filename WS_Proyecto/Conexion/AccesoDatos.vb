Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports Microsoft.SqlServer.Server

Public Class AccesoDatos
    Shared strConexion As String = "Server=localhost;Database=SA_Proyecto;Trusted_Connection=Yes;"
    Public Shared Function ExecuteDataSet(ByVal strStoredProcedure As String, _
                                          ParamArray ArrayParametros As Object()) As DataSet
        Dim dsRespuesta As DataSet = New DataSet
        Dim dtRespuesta As New DataTable()
        
        If ArrayParametros.Length Mod 2 = 0 Then ' check si vienen los datos pares
            Using con As New SqlConnection(strConexion)
                con.Open()
                Using cmd As New SqlCommand(strStoredProcedure, con)
                    cmd.CommandType = CommandType.StoredProcedure
                    For i As Integer = 0 To ArrayParametros.Length - 1
                        cmd.Parameters.AddWithValue(ArrayParametros(i).ToString(), ArrayParametros(i + 1))
                        i = i + 1
                    Next
                    Using drRespuesta As SqlDataReader = cmd.ExecuteReader()
                        dtRespuesta.Load(drRespuesta)
                        dsRespuesta.Tables.Add(dtRespuesta)
                    End Using
                End Using
            End Using
            Return dsRespuesta
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function ExecuteNonQuery(ByVal strStoredProcedure As String, _
                                          ParamArray ArrayParametros As Object()) As Integer
        Dim intRespuesta As Integer
        If ArrayParametros.Length Mod 2 = 0 Then ' check si vienen los datos pares
            Using con As New SqlConnection(strConexion)
                Using cmd As New SqlCommand(strStoredProcedure, con)
                    cmd.CommandType = CommandType.StoredProcedure
                    For i As Integer = 0 To ArrayParametros.Length - 1
                        cmd.Parameters.AddWithValue(ArrayParametros(i).ToString(), ArrayParametros(i + 1))
                        i = i + 1
                    Next
                    intRespuesta = cmd.ExecuteNonQuery()
                End Using
            End Using
            Return intRespuesta
        Else
            Return -1 ' Da porque no se mandaron los parametros como son 
        End If
        Return Nothing
    End Function

End Class
