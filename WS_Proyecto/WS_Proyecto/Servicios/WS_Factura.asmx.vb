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
    Public Function insert(ByVal strFecha As String, ByVal intIdCliente As Integer,
                           ByVal intIdFarmacia As Integer, ByVal intIdEmpleado As Integer) As DataSet

        Dim rs As DataSet = New DataSet

        rs = Conexion.AccesoDatos.ExecuteDataSet("Factura_Insert", "@fecha_factura", Convert.ToDateTime(strFecha), "@id_cliente", intIdCliente,
                                                 "@id_farmacia", intIdFarmacia, "@id_empleado", intIdEmpleado)
        Return rs
    End Function

    <WebMethod()> _
    Public Function delete(ByVal intIdFactura As Integer) As Integer
        Dim rs As Integer

        rs = Conexion.AccesoDatos.ExecuteNonQuery("Factura_Delete", "@id", intIdFactura)

        Return rs
    End Function

End Class