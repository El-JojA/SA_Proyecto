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

    <WebMethod()> _
    Public Function HelloWorld() As String
       Return "Hello World"
    End Function

    '0 = No loguea
    '1 = Loguea como empleado de farmacias
    '2 = Loguea como empleado de callcenter
    <WebMethod()> _
    Public Function Login(ByVal strUsuario As String, ByVal strPass As String) As String
        Dim dsResultado As DataSet = New DataSet
        Dim strTipoEmpleado As String
        dsResultado = Conexion.AccesoDatos.ExecuteDataSet("Login_Emp",
                                                          "@usuario", strUsuario,
                                                          "@pass", strPass)
        If Conexion.AccesoDatos.DatasetVacio(dsResultado) Then
            Return "0"
        Else
            strTipoEmpleado = dsResultado.Tables(0).Rows(0).Item("tipo_empleado").ToString
            Return strTipoEmpleado
        End If
    End Function

End Class