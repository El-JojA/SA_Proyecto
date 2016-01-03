Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class WS_Producto
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function Insert(ByVal strNombre As String, ByVal intDisponible As Integer,
                           ByVal strImg As String, ByVal dblPrecio As Double) As Integer
        'llmar a stored procedure para que haga el insert
        Dim intResultado As Integer
        'corre el stored procedure y regresa 0 si ingresó los datos correctamente.
        'regresa -1 si no los ingresa correctamente (creo)
        intResultado = Conexion.AccesoDatos.ExecuteNonQuery("Producto_Insert",
                                            "@nombre", strNombre, "@disponible", intDisponible,
                                            "@img", strImg, "@precio", dblPrecio)
        If intResultado = 0 Then
            Return 1
        End If
        Return 0
    End Function



End Class