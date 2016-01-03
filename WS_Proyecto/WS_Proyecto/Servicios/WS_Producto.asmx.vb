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

        Dim ds As DataSet = New DataSet()
        ds = Buscar(Nothing, Nothing)

        Return "Hello World"
    End Function

    Public Function Insert(ByVal strNombre As String, ByVal intDisponible As Integer,
                           ByVal strImg As String, ByVal dblPrecio As Double) As Integer
        Dim dsResultado As DataSet = New DataSet
        Dim intResultado As Integer = -1
        'corre el stored procedure y regresa < 0 si ingresó los datos correctamente.
        'regresa 0 si no pudo ingresar los datos
        dsResultado = Conexion.AccesoDatos.ExecuteDataSet("Producto_Insert",
                                            "@nombre", strNombre, "@disponible", intDisponible,
                                            "@img", strImg, "@precio", dblPrecio)
        intResultado = CInt(dsResultado.Tables(0).Rows(0).Item("id"))
        Return intResultado
    End Function

    Public Function Update(ByVal intId As Integer, ByVal strNombre As String,
                           ByVal intDisponible As Integer, ByVal strImg As String,
                           ByVal dblPrecio As Double, ByVal bytEstado As Byte) As Integer
        Dim intResultado As Integer
        'corre el stored procedure y regresa < 0 si ingresó los datos correctamente.
        'regresa 0 si no pudo ingresar los datos
        intResultado = Conexion.AccesoDatos.ExecuteNonQuery("Producto_Update",
                                                            "@id", intId,
                                                            "@nombre", IIf(strNombre = Nothing, DBNull.Value, strNombre),
                                                            "@disponible", IIf(intDisponible = Nothing, DBNull.Value, intDisponible),
                                                            "@img", IIf(strImg = Nothing, DBNull.Value, strImg),
                                                            "@precio", IIf(dblPrecio = Nothing, DBNull.Value, dblPrecio),
                                                            "@estado", IIf(bytEstado = Nothing, DBNull.Value, bytEstado))
        Return intResultado
    End Function

    Public Function Delete(ByVal intId As Integer) As Integer
        Dim intResultado As Integer
        'corre el stored procedure y regresa < 0 si ingresó los datos correctamente.
        'regresa 0 si no pudo ingresar los datos
        intResultado = Conexion.AccesoDatos.ExecuteNonQuery("Producto_Delete",
                                                            "@id", intId)
        Return intResultado
    End Function

    Public Function Buscar(ByVal intId As Integer, ByVal strNombre As String) As DataSet
        Dim dsResultado As DataSet = New DataSet
        dsResultado = Conexion.AccesoDatos.ExecuteDataSet("Producto_Buscar",
                                                          "@id", IIf(intId = Nothing, DBNull.Value, intId),
                                                          "@nombre", IIf(strNombre = Nothing, DBNull.Value, strNombre))
        Return dsResultado
    End Function

End Class