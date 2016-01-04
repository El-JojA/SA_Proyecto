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
    Public Function HelloWorld() As List(Of Producto)

        Dim ds As List(Of Producto)
        ds = Buscar(Nothing, Nothing, 1)

        Return ds
    End Function

    <WebMethod()> _
    Public Function Insert(ByVal strNombre As String, ByVal intDisponible As Integer,
                           ByVal strImg As String, ByVal dblPrecio As Double,
                           ByVal strDetalle As String, ByVal intIdEmpleado As Integer) As Integer
        Dim dsResultado As DataSet = New DataSet
        Dim intResultado As Integer = -1
        Dim intResultadoBitacora As Integer
        Dim strMensajeBitacora
        'corre el stored procedure y regresa < 0 si ingresó los datos correctamente.
        'regresa 0 si no pudo ingresar los datos
        dsResultado = Conexion.AccesoDatos.ExecuteDataSet("Producto_Insert",
                                            "@nombre", strNombre, "@disponible", intDisponible,
                                            "@img", strImg, "@precio", dblPrecio,
                                            "@detalle", strDetalle)
        intResultado = CInt(dsResultado.Tables(0).Rows(0).Item("id"))

        ''BITACORA
        strMensajeBitacora = "Se ingresó un producto de nombre: " &
            strNombre & " y descripción: " & strDetalle & ". El resutado fue " &
            IIf(intResultado = 0, "fallido", "eeeeexitoso.")

        intResultadoBitacora = Conexion.AccesoDatos.ExecuteNonQuery("Bitacora_Insert",
                                                                    "@descripcion", strMensajeBitacora,
                                                                    "@servicio", "WS_Producto.Insert()",
                                                                    "@id_empleado", intIdEmpleado)
        Return intResultado
    End Function

    <WebMethod()> _
    Public Function Update(ByVal intId As Integer, ByVal strNombre As String,
                           ByVal intDisponible As Integer, ByVal strImg As String,
                           ByVal dblPrecio As Double, ByVal bytEstado As Byte,
                           ByVal strDetalle As String, ByVal intIdEmpleado As Integer) As Integer
        Dim intResultado As Integer
        Dim strMensajeBitacora As String
        Dim intResultadoBitacora As Integer
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

        ''BITACORA
        strMensajeBitacora = "Se ingresó actualizó el producto de id: " & intId &
            ". El resutado fue " & IIf(intResultado = 0, "fallido", "eeeeexitoso.")
        '" y datos(" & strNombre & ", " & intDisponible & ", " & strImg & " , " & dblPrecio & " , , )=: " &

        intResultadoBitacora = Conexion.AccesoDatos.ExecuteNonQuery("Bitacora_Insert",
                                                                    "@descripcion", strMensajeBitacora,
                                                                    "@servicio", "WS_Producto.Update()",
                                                                    "@id_empleado", intIdEmpleado)

        Return intResultado
    End Function

    <WebMethod()> _
    Public Function Delete(ByVal intId As Integer, ByVal intIdEmpleado As Integer) As Integer
        Dim intResultado As Integer
        Dim strMensajeBitacora As String
        Dim intResultadoBitacora As Integer
        'corre el stored procedure y regresa < 0 si ingresó los datos correctamente.
        'regresa 0 si no pudo ingresar los datos
        intResultado = Conexion.AccesoDatos.ExecuteNonQuery("Producto_Delete",
                                                            "@id", intId)
        ''BITACORA
        strMensajeBitacora = "Se ingresó eliminó el producto de id: " & intId &
            ". El resutado fue " & IIf(intResultado = 0, "fallido", "eeeeexitoso.")
        '" y datos(" & strNombre & ", " & intDisponible & ", " & strImg & " , " & dblPrecio & " , , )=: " &

        intResultadoBitacora = Conexion.AccesoDatos.ExecuteNonQuery("Bitacora_Insert",
                                                                    "@descripcion", strMensajeBitacora,
                                                                    "@servicio", "WS_Producto.Delete()",
                                                                    "@id_empleado", intIdEmpleado)
        Return intResultado
    End Function

    <WebMethod()> _
    Public Function Buscar(ByVal intId As Integer, ByVal strNombre As String, ByVal intIdEmpleado As Integer) As List(Of Producto)
        Dim dsResultado As DataSet = New DataSet("Lista_Productos")
        Dim listaProductos As List(Of Producto) = New List(Of Producto)
        Dim strMensajeBitacora As String
        Dim intResultadoBitacora As Integer
        dsResultado = Conexion.AccesoDatos.ExecuteDataSet("Producto_Buscar",
                                                          "@id", IIf(intId = Nothing, DBNull.Value, intId),
                                                          "@nombre", IIf(strNombre = Nothing, DBNull.Value, strNombre))

        If Not Conexion.AccesoDatos.DatasetVacio(dsResultado) Then
            Dim dtResultado As DataTable = dsResultado.Tables(0)
            For i As Integer = 0 To dtResultado.Rows.Count - 1
                Dim pro As Producto = New Producto
                pro.bytEstado = dtResultado.Rows(i).Item("estado")
                pro.dblPrecio = dtResultado.Rows(i).Item("precio_producto")
                pro.intDisponible = dtResultado.Rows(i).Item("disponible_producto")
                pro.intId = dtResultado.Rows(i).Item("id_producto")
                pro.strDetalle = dtResultado.Rows(i).Item("detalle_producto")
                pro.strNombre = IIf(dtResultado.Rows(i).Item("nombre_producto") Is DBNull.Value, "(Sin nombre)", dtResultado.Rows(i).Item("nombre_producto"))
                listaProductos.Add(pro)
            Next
        End If

        ''BITACORA
        strMensajeBitacora = "Se realizó una busqueda de productos con los datos: " &
            "(" & intId & ", " & strNombre & ") " &
            ". El resutado fue " & IIf(Conexion.AccesoDatos.DatasetVacio(dsResultado), "fallido", "eeeeexitoso.")
        '" y datos(" & strNombre & ", " & intDisponible & ", " & strImg & " , " & dblPrecio & " , , )=: " &

        intResultadoBitacora = Conexion.AccesoDatos.ExecuteNonQuery("Bitacora_Insert",
                                                                    "@descripcion", strMensajeBitacora,
                                                                    "@servicio", "WS_Producto.Buscar()",
                                                                    "@id_empleado", intIdEmpleado)

        Return listaProductos
    End Function

End Class