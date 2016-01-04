Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class WS_Factura
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function insert(ByVal intIdCliente As Integer,
                           ByVal intIdFarmacia As Integer, ByVal intIdEmpleado As Integer) As Integer

        Dim dsResultado As DataSet = New DataSet
        Dim intResultado As Integer = 0
        Dim intResultadoBitacora As Integer
        Dim strMensajeBitacora = "(Sin mensaje)"

        dsResultado = Conexion.AccesoDatos.ExecuteDataSet("Factura_Insert",
                                                          "@id_cliente", intIdCliente,
                                                          "@id_farmacia", intIdFarmacia,
                                                          "@id_empleado", intIdEmpleado)

        If Not (Conexion.AccesoDatos.DatasetVacio(dsResultado)) Then
            intResultado = CInt(dsResultado.Tables(0).Rows(0).Item("id"))
        End If

        ''BITACORA
        strMensajeBitacora = "Se ingresó una FACTTURA con el identificador: " & intResultado &
        ". El resutado fue " &
            IIf(intResultado = 0, "FALLIDO", "ééééééééxitoso.")

        intResultadoBitacora = Conexion.AccesoDatos.ExecuteNonQuery("Bitacora_Insert",
                                                                    "@descripcion", strMensajeBitacora,
                                                                    "@servicio", "WS_Factura.Insert()",
                                                                    "@id_empleado", intIdEmpleado)
        Return intResultado

    End Function



    <WebMethod()> _
    Public Function delete(ByVal intIdFactura As Integer, ByVal intIdEmpleado As Integer) As Integer
        ''
        Dim intResultado As Integer
        Dim strMensajeBitacora As String = "(Sin mensaje)"
        Dim intResultadoBitacora As Integer

        intResultado = Conexion.AccesoDatos.ExecuteNonQuery("Factura_Delete", "@id", intIdFactura)

        ''BITACORA
        strMensajeBitacora = "Se canceló una el pedido número: " & intIdFactura &
        ". El resutado fue " &
            IIf(intResultado = 0, "FALLIDO", "ééééééééxitoso.")

        intResultadoBitacora = Conexion.AccesoDatos.ExecuteNonQuery("Bitacora_Insert",
                                                                    "@descripcion", strMensajeBitacora,
                                                                    "@servicio", "WS_Factura.delete()",
                                                                    "@id_empleado", intIdEmpleado)

        Return intResultado
    End Function

    <WebMethod()> _
    Public Function InsertDetalleFactura(ByVal intIdFactura As Integer,
                                         ByVal intIdEmpleado As Integer) As Integer
        Return 0
    End Function


End Class