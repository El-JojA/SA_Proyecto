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
    Public Function Insert(ByVal intIdCliente As Integer,
                           ByVal intIdFarmacia As Integer,
                           ByVal intIdEmpleado As Integer,
                           ByVal dblTotal As Double,
                           ByVal intTipoPago As Integer,
                           ByVal strDireccion As String) As Integer

        Dim dsResultado As DataSet = New DataSet
        Dim intResultado As Integer = 0
        Dim intResultadoBitacora As Integer
        Dim strMensajeBitacora = "(Sin mensaje)"

        Try
            dsResultado = Conexion.AccesoDatos.ExecuteDataSet("Factura_Insert",
                                                          "@id_cliente", intIdCliente,
                                                          "@id_farmacia", intIdFarmacia,
                                                          "@id_empleado", intIdEmpleado,
                                                          "@total", dblTotal,
                                                          "@tipo", intTipoPago,
                                                          "@direccion", strDireccion)

            If Not (Conexion.AccesoDatos.DatasetVacio(dsResultado)) Then
                intResultado = CInt(dsResultado.Tables(0).Rows(0).Item("id"))
            End If

            ''BITACORA
            strMensajeBitacora = "Se ingresó una FACTTURA con el identificador: " & intResultado &
            ". El resutado fue " & IIf(intResultado = 0, "FALLIDO", "ééééééééxitoso.")
        Catch ex As Exception
            strMensajeBitacora = ex.Message
        End Try

        'BITACORA
        intResultadoBitacora = Conexion.AccesoDatos.InsertarBitacora(strMensajeBitacora, "WS_Factura.Insert()", intIdEmpleado)
        Return intResultado

    End Function

    <WebMethod()> _
    Public Function Rollback(ByVal intIdFactura As Integer, ByVal intIdEmpleado As Integer) As Integer
        Dim intResultado As Integer
        Dim strMensajeBitacora As String = "(Sin mensaje)"
        Dim dsDetalleFactura As DataSet = New DataSet
        Dim intCantidadDetalle As Integer

        Try
            ''Consulta de detalles de factura sobre factura en cuestion
            dsDetalleFactura = Conexion.AccesoDatos.ExecuteDataSet("Detalle_Factura_Buscar",
                                                                   "@idFactura", intIdFactura)
            'Recorrido de detalles. 
            If Not Conexion.AccesoDatos.DatasetVacio(dsDetalleFactura) Then
                intCantidadDetalle = dsDetalleFactura.Tables(0).Rows(0).Item("cantidad")
                For Each rou As DataRow In dsDetalleFactura.Tables(0).Rows
                    'Consulta de producto de detalle
                    Dim intIdProducto As Integer = rou.Item("id_producto_fk")
                    Dim dsProducto As DataSet = New DataSet
                    dsProducto = Conexion.AccesoDatos.ExecuteDataSet("Producto_Buscar",
                                                                     "@id", intIdProducto,
                                                                     "@nombre", DBNull.Value)
                    If Not Conexion.AccesoDatos.DatasetVacio(dsProducto) Then
                        'Reposición en inventario de las cantidades
                        Dim intCantidadInventario As Integer = dsProducto.Tables(0).Rows(0).Item("disponible_producto")
                        Dim intResultadoReposicion As Integer = Conexion.AccesoDatos.ExecuteNonQuery("Producto_Update",
                                                                              "@Id", intIdProducto,
                                                                              "@nombre", DBNull.Value,
                                                                              "@disponible", intCantidadInventario + intCantidadDetalle,
                                                                              "@img", DBNull.Value,
                                                                              "@precio", DBNull.Value,
                                                                              "@estado", DBNull.Value,
                                                                              "@detalle", DBNull.Value)
                    Else
                        Conexion.AccesoDatos.InsertarBitacora("Error al buscar Producto.", "WS_Factura.Delete()", intIdEmpleado)
                    End If
                Next
            End If

            ''Cancelación de factura
            intResultado = Conexion.AccesoDatos.ExecuteNonQuery("Factura_Delete", "@id", intIdFactura)

            strMensajeBitacora = "Se ingresó una FACTTURA con el identificador: " & intResultado &
            ". El resutado fue " & IIf(intResultado = 0, "FALLIDO", "ééééééééxitoso.")
        Catch ex As Exception
            intResultado = 0
            strMensajeBitacora = ex.Message
        End Try

        'BITACORA
        Conexion.AccesoDatos.InsertarBitacora(strMensajeBitacora, "WS_Factura.Delete()", intIdEmpleado)
        Return intResultado

    End Function

    <WebMethod()> _
    Public Function Cancelar(ByVal intIdFactura As Integer, ByVal intIdEmpleado As Integer) As Integer
        Dim intResultado As Integer
        Dim strMensajeBitacora As String = "(Sin mensaje)"
        Dim dsDetalleFactura As DataSet = New DataSet
        Dim dsFactura As DataSet = New DataSet
        Dim intCantidadDetalle As Integer
        Dim blnSePuedeCancelar As Boolean

        Try
            dsFactura = Conexion.AccesoDatos.ExecuteDataSet("Factura_Buscar",
                                                            "@idFactura", intIdFactura)
            If Not Conexion.AccesoDatos.DatasetVacio(dsFactura) Then
                Dim datFecha As DateTime = dsFactura.Tables(0).Rows(0).Item("fecha_factura")
                Dim segundos As Integer = (datFecha - Now).Seconds
                If segundos < 3600 Then
                    blnSePuedeCancelar = True
                Else
                    blnSePuedeCancelar = False
                End If
            Else
                blnSePuedeCancelar = False
            End If

            If blnSePuedeCancelar Then ''ver la hora y fecha
                ''Consulta de detalles de factura sobre factura en cuestion
                dsDetalleFactura = Conexion.AccesoDatos.ExecuteDataSet("Detalle_Factura_Buscar",
                                                                       "@idFactura", intIdFactura)
                'Recorrido de detalles. 
                If Not Conexion.AccesoDatos.DatasetVacio(dsDetalleFactura) Then
                    intCantidadDetalle = dsDetalleFactura.Tables(0).Rows(0).Item("cantidad")
                    For Each rou As DataRow In dsDetalleFactura.Tables(0).Rows
                        'Consulta de producto de detalle
                        Dim intIdProducto As Integer = rou.Item("id_producto_fk")
                        Dim dsProducto As DataSet = New DataSet
                        dsProducto = Conexion.AccesoDatos.ExecuteDataSet("Producto_Buscar",
                                                                         "@id", intIdProducto,
                                                                         "@nombre", DBNull.Value)
                        If Not Conexion.AccesoDatos.DatasetVacio(dsProducto) Then
                            'Reposición en inventario de las cantidades
                            Dim intCantidadInventario As Integer = dsProducto.Tables(0).Rows(0).Item("disponible_producto")
                            Dim intResultadoReposicion As Integer = Conexion.AccesoDatos.ExecuteNonQuery("Producto_Update",
                                                                                  "@Id", intIdProducto,
                                                                                  "@nombre", DBNull.Value,
                                                                                  "@disponible", intCantidadInventario + intCantidadDetalle,
                                                                                  "@img", DBNull.Value,
                                                                                  "@precio", DBNull.Value,
                                                                                  "@estado", DBNull.Value,
                                                                                  "@detalle", DBNull.Value)
                        Else
                            Conexion.AccesoDatos.InsertarBitacora("Error al buscar Producto.", "WS_Factura.Delete()", intIdEmpleado)
                        End If
                    Next
                End If

                ''Cancelación de factura
                intResultado = Conexion.AccesoDatos.ExecuteNonQuery("Factura_Delete", "@id", intIdFactura)

                strMensajeBitacora = "Se ingresó una FACTTURA con el identificador: " & intResultado &
                ". El resutado fue " & IIf(intResultado = 0, "FALLIDO", "ééééééééxitoso.")
            Else
                intResultado = 0
            End If
        Catch ex As Exception
            intResultado = 0
            strMensajeBitacora = ex.Message
        End Try

        'BITACORA
        Conexion.AccesoDatos.InsertarBitacora(strMensajeBitacora, "WS_Factura.Delete()", intIdEmpleado)
        Return intResultado

    End Function

    <WebMethod()> _
    Public Function InsertDetalleFactura(ByVal intIdFactura As Integer,
                                         ByVal intIdProducto As Integer,
                                         ByVal intCantidad As Integer,
                                         ByVal intIdEmpleado As Integer) As Integer
        '' get producto y sacar el precio
        '' operar la cantidad total $
        '' crear detalle factura
        '' actualizar inventario
        Dim dblPrecio As Double
        Dim dblCantidadTotal As Double
        Dim intResultado As Integer
        Dim intResultadoUpdateProducto As Integer
        Dim intCantidadProducto As Integer
        Dim dsResultado As DataSet = Conexion.AccesoDatos.ExecuteDataSet("Producto_Buscar",
                                                                         "@id", intIdProducto)
        If Not Conexion.AccesoDatos.DatasetVacio(dsResultado) Then
            dblPrecio = dsResultado.Tables(0).Rows(0).Item("precio_producto")
            intCantidadProducto = dsResultado.Tables(0).Rows(0).Item("disponible_producto")
        Else 'Falló consulta de precio sobre producto
            Return 0
        End If
        dblCantidadTotal = dblCantidadTotal * intCantidad

        'Inventario insuficiente
        If intCantidadProducto < intCantidad Then
            Return -1
        End If

        intResultado = Conexion.AccesoDatos.ExecuteNonQuery("Detalle_Factura_Insert",
                                                            "@idProducto", intIdProducto,
                                                            "@idFactura", intIdFactura,
                                                            "@cantidad", intCantidad,
                                                            "@precioUnidad", dblPrecio,
                                                            "@cantidadTotal", dblCantidadTotal)
        If intResultado = 0 Then
            Return 0
        End If

        '' Decrementar cantidad de inventario en Producto
        intResultadoUpdateProducto = Conexion.AccesoDatos.ExecuteNonQuery("Producto_Update",
                                                                          "@Id", intIdProducto,
                                                                          "@nombre", DBNull.Value,
                                                                          "@disponible", intCantidadProducto - intCantidad,
                                                                          "@img", DBNull.Value,
                                                                          "@precio", DBNull.Value,
                                                                          "@estado", DBNull.Value,
                                                                          "@detalle", DBNull.Value)
        If intResultadoUpdateProducto = 0 Then
            Return 0
        End If

        Return intResultado
    End Function
End Class