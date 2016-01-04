Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class WS_Tienda
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function Insert(ByVal strNombre As String, ByVal strTelefono As String,
                           ByVal strDireccion As String, ByVal bytEsnuestra As Byte,
                           ByVal intIdEmpleado As Integer) As Integer

        Dim dsResultado As DataSet = New DataSet
        Dim intResultado As Integer = 0
        Dim intResultadoBitacora As Integer
        Dim strMensajeBitacora = "(Sin mensaje)"

        dsResultado = Conexion.AccesoDatos.ExecuteDataSet("Tienda_Insert",
                                            "@nombre", strNombre, "@telefono", strTelefono,
                                            "@direccion", strDireccion, "@esnuestra", bytEsnuestra)

        If Not (Conexion.AccesoDatos.DatasetVacio(dsResultado)) Then
            intResultado = CInt(dsResultado.Tables(0).Rows(0).Item("id"))
        End If

        ''BITACORA
        strMensajeBitacora = "Se ingresó una TIENDA de nombre: " &
            strNombre & ". El resutado fue " &
            IIf(intResultado = 0, "FALLIDO", "eeeeexitoso.")

        intResultadoBitacora = Conexion.AccesoDatos.ExecuteNonQuery("Bitacora_Insert",
                                                                    "@descripcion", strMensajeBitacora,
                                                                    "@servicio", "WS_Tienda.Insert()",
                                                                    "@id_empleado", intIdEmpleado)

        Return intResultado
    End Function

    <WebMethod()> _
    Public Function Buscar(ByVal intIdEmpleado As Integer) As List(Of Tienda)
        Dim dsResultado As DataSet
        Dim listaTiendas As List(Of Tienda) = New List(Of Tienda)
        Dim strMensajeBitacora As String
        Dim intResultadoBitacora As Integer
        dsResultado = Conexion.AccesoDatos.ExecuteDataSet("Tienda_Buscar",
                                                          "@id", DBNull.Value)

        If Not Conexion.AccesoDatos.DatasetVacio(dsResultado) Then
            For i As Integer = 0 To dsResultado.Tables(0).Rows.Count - 1
                Dim tie As Tienda = New Tienda
                tie.bytEsNuestro = dsResultado.Tables(0).Rows(i).Item("esNuestra")
                tie.bytEstado = dsResultado.Tables(0).Rows(i).Item("estado")
                tie.intId = dsResultado.Tables(0).Rows(i).Item("id_tienda")
                tie.strDireccion = IIf(dsResultado.Tables(0).Rows(i).Item("direccion_tienda") Is DBNull.Value, "(Sin nombre)", dsResultado.Tables(0).Rows(i).Item("direccion_tienda"))
                tie.strNombre = dsResultado.Tables(0).Rows(i).Item("nombre_tienda")
                tie.strTelefono = IIf(dsResultado.Tables(0).Rows(i).Item("telefono_tienda") Is DBNull.Value, "(Sin telefono)", dsResultado.Tables(0).Rows(i).Item("telefono_tienda"))
                listaTiendas.Add(tie)
            Next
        End If

        ''BITACORA
        strMensajeBitacora = "Se realizó una busqueda de TIENDAS" &
            ". El resutado fue " & IIf(Conexion.AccesoDatos.DatasetVacio(dsResultado), "fallido/sin datos.", "eeeeexitoso.")

        intResultadoBitacora = Conexion.AccesoDatos.ExecuteNonQuery("Bitacora_Insert",
                                                                    "@descripcion", strMensajeBitacora,
                                                                    "@servicio", "WS_Tienda.Buscar()",
                                                                    "@id_empleado", intIdEmpleado)

        Return listaTiendas
    End Function

End Class