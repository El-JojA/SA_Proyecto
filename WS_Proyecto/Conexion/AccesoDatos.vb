Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports Microsoft.SqlServer.Server

Public Class AccesoDatos
    'Shared strConexion As String = "Server=localhost;Database=SA_Proyecto;Trusted_Connection=Yes;"
    Shared strConexion As String = "Server=25.149.166.182;Database=SA_Proyecto;User Id=admin;Password=123;"


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
                con.Open()
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

    Public Shared Function InsertarBitacora(ByVal strMensajeBitacora As String,
                                            ByVal strServicio As String,
                                            ByVal intIdEmpleado As Integer) As Integer
        ''BITACORA
        Return Conexion.AccesoDatos.ExecuteNonQuery("Bitacora_Insert",
                                                    "@descripcion", strMensajeBitacora,
                                                    "@servicio", strServicio,
                                                    "@id_empleado", intIdEmpleado)
    End Function

End Class
