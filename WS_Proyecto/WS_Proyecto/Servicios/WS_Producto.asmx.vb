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

        Dim ds As String
        ds = Buscar(Nothing, Nothing)

        Return ds
    End Function

    <WebMethod()> _
    Public Function Insert(ByVal strNombre As String, ByVal intDisponible As Integer,
                           ByVal strImg As String, ByVal dblPrecio As Double,
                           ByVal strDetalle As String) As Integer
        Dim dsResultado As DataSet = New DataSet
        Dim intResultado As Integer = -1
        'corre el stored procedure y regresa < 0 si ingresó los datos correctamente.
        'regresa 0 si no pudo ingresar los datos
        dsResultado = Conexion.AccesoDatos.ExecuteDataSet("Producto_Insert",
                                            "@nombre", strNombre, "@disponible", intDisponible,
                                            "@img", strImg, "@precio", dblPrecio,
                                            "@detalle", strDetalle)
        intResultado = CInt(dsResultado.Tables(0).Rows(0).Item("id"))
        Return intResultado
    End Function

    <WebMethod()> _
    Public Function Update(ByVal intId As Integer, ByVal strNombre As String,
                           ByVal intDisponible As Integer, ByVal strImg As String,
                           ByVal dblPrecio As Double, ByVal bytEstado As Byte,
                           ByVal strDetalle As String) As Integer
        Dim intResultado As Integer
        'corre el stored procedure y regresa < 0 si ingresó los datos correctamente.
        'regresa 0 si no pudo ingresar los datos
        intResultado = Conexion.AccesoDatos.ExecuteNonQuery("Producto_Update",
                                                            "@id", intId,
                                                            "@nombre", IIf(strNombre = Nothing, DBNull.Value, strNombre),
                                                            "@disponible", IIf(intDisponible = Nothing, DBNull.Value, intDisponible),
                                                            "@img", IIf(strImg = Nothing, DBNull.Value, strImg),
                                                            "@precio", IIf(dblPrecio = Nothing, DBNull.Value, dblPrecio),
                                                            "@estado", IIf(bytEstado = Nothing, DBNull.Value, bytEstado),
                                                            "@detalle", IIf(strDetalle = Nothing, DBNull.Value, strDetalle))
        Return intResultado
    End Function

    <WebMethod()> _
    Public Function Delete(ByVal intId As Integer) As Integer
        Dim intResultado As Integer
        'corre el stored procedure y regresa < 0 si ingresó los datos correctamente.
        'regresa 0 si no pudo ingresar los datos
        intResultado = Conexion.AccesoDatos.ExecuteNonQuery("Producto_Delete",
                                                            "@id", intId)
        Return intResultado
    End Function

    <WebMethod()> _
    Public Function Buscar(ByVal intId As Integer, ByVal strNombre As String) As String
        Dim dsResultado As DataSet = New DataSet("Lista_Productos")
        dsResultado = Conexion.AccesoDatos.ExecuteDataSet("Producto_Buscar",
                                                          "@id", IIf(intId = Nothing, DBNull.Value, intId),
                                                          "@nombre", IIf(strNombre = Nothing, DBNull.Value, strNombre))
        dsResultado.DataSetName = "Lista_Productos"
        If Not Conexion.AccesoDatos.DatasetVacio(dsResultado) Then
            dsResultado.Tables(0).TableName = "Producto"
        End If
        Return "<BuscarProducto>" & dsResultado.GetXml & "</BuscarProducto>"
    End Function

End Class