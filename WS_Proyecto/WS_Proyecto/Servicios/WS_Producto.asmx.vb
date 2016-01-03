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
    Public Function HelloWorld() As String
       Return "Hello World"
    End Function

    Public Function Insert(ByVal intId As Integer, ByVal strNombre As String,
                           ByVal intDisponible As Integer, ByVal strImg As String,
                           ByVal dblPrecio As Double, ByVal intEstado As Integer) As Integer
        'llmar a stored procedure para que haga el insert
        Return -1
    End Function

End Class