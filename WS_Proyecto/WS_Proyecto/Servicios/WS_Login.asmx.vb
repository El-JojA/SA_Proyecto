Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class WS_Login
    Inherits System.Web.Services.WebService

    '0 = No loguea
    '1 = Loguea como empleado de farmacias
    '2 = Loguea como empleado de callcenter
    <WebMethod()> _
    Public Function Logueo(ByVal strUsuario As String, ByVal strPass As String) As Login
        Dim dsResultado As DataSet = New DataSet
        Dim log As Login = New Login
        dsResultado = Conexion.AccesoDatos.ExecuteDataSet("Login_Emp",
                                                          "@usuario", strUsuario,
                                                          "@pass", strPass)
        If Conexion.AccesoDatos.DatasetVacio(dsResultado) Then
            log.intId = "0"
            log.intResultado = "0"
            log.intTipoEmpleado = "0"
            log.strApellido = ""
            log.strNombre = ""
            Return log
        Else
            log.intId = dsResultado.Tables(0).Rows(0).Item("id_empleado")
            log.intResultado = dsResultado.Tables(0).Rows(0).Item("tipo_empleado")
            log.intTipoEmpleado = dsResultado.Tables(0).Rows(0).Item("tipo_empleado")
            log.strApellido = dsResultado.Tables(0).Rows(0).Item("apellido_empleado").ToString
            log.strNombre = IIf(dsResultado.Tables(0).Rows(0).Item("nombre_empleado") Is DBNull.Value, "(Sin nombre)", dsResultado.Tables(0).Rows(0).Item("nombre_empleado").ToString)
            Return log
        End If
    End Function

End Class