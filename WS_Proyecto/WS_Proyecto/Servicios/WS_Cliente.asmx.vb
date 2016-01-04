Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class WS_Cliente
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function Insert(ByVal strNombre As String, ByVal strApellido As String,
                           ByVal strTelefono As String, ByVal strNit As String,
                           ByVal intIdEmpleado As Integer) As Integer
        Dim dsResultado As DataSet = New DataSet
        Dim intResultado As Integer = -1
        Dim intResultadoBitacora As Integer
        Dim strMensajeBitacora
        'corre el stored procedure y regresa < 0 si ingresó los datos correctamente.
        'regresa 0 si no pudo ingresar los datos
        dsResultado = Conexion.AccesoDatos.ExecuteDataSet("Cliente_Insert",
                                            "@nombre", strNombre, "@apellido", strApellido,
                                            "@telefono", strTelefono, "@nit", strNit)
        intResultado = CInt(dsResultado.Tables(0).Rows(0).Item("id"))

        ''BITACORA
        strMensajeBitacora = "Se ingresó un cliente de nombre: " &
            strNombre & " " & strApellido & ". El resutado fue " &
            IIf(intResultado = 0, "fallido", "eeeeexitoso.")

        intResultadoBitacora = Conexion.AccesoDatos.ExecuteNonQuery("Bitacora_Insert",
                                                                    "@descripcion", strMensajeBitacora,
                                                                    "@servicio", "WS_Cliente.Insert()",
                                                                    "@id_empleado", intIdEmpleado)
        Return intResultado
    End Function

    <WebMethod()> _
    Public Function Update(ByVal intId As Integer, ByVal strNombre As String,
                           ByVal strApellido As String, ByVal strTelefono As String,
                           ByVal intIdEmpleado As Integer) As Integer
        Dim intResultado As Integer
        Dim strMensajeBitacora As String
        Dim intResultadoBitacora As Integer
        'corre el stored procedure y regresa < 0 si ingresó los datos correctamente.
        'regresa 0 si no pudo ingresar los datos
        intResultado = Conexion.AccesoDatos.ExecuteNonQuery("Cliente_Update",
                                                            "@id", intId,
                                                            "@nombre", IIf(strNombre = Nothing, DBNull.Value, strNombre),
                                                            "@apellido", IIf(strApellido = Nothing, DBNull.Value, strApellido),
                                                            "@telefono", IIf(strTelefono = Nothing, DBNull.Value, strTelefono))

        ''BITACORA
        strMensajeBitacora = "Se actualizó el cliente de id: " & intId &
            ". El resutado fue " & IIf(intResultado = 0, "fallido", "eeeeexitoso.")
        '" y datos(" & strNombre & ", " & intDisponible & ", " & strImg & " , " & dblPrecio & " , , )=: " &

        intResultadoBitacora = Conexion.AccesoDatos.ExecuteNonQuery("Bitacora_Insert",
                                                                    "@descripcion", strMensajeBitacora,
                                                                    "@servicio", "WS_Cliente.Update()",
                                                                    "@id_empleado", intIdEmpleado)

        Return intResultado
    End Function

    <WebMethod()> _
    Public Function Buscar(ByVal strNit As String, ByVal intIdEmpleado As Integer) As Cliente
        Dim dsResultado As DataSet
        Dim cli As Cliente = New Cliente
        Dim strMensajeBitacora As String
        Dim intResultadoBitacora As Integer
        dsResultado = Conexion.AccesoDatos.ExecuteDataSet("Cliente_Buscar",
                                                          "@nit", IIf(strNit = Nothing, DBNull.Value, strNit))

        If Not Conexion.AccesoDatos.DatasetVacio(dsResultado) Then
            cli.intId = dsResultado.Tables(0).Rows(0).Item("id_cliente")
            cli.bytEstado = dsResultado.Tables(0).Rows(0).Item("estado")
            cli.strApellido = dsResultado.Tables(0).Rows(0).Item("apellido_cliente")
            cli.strNombre = dsResultado.Tables(0).Rows(0).Item("nombre_cliente")
            cli.strTelefono = dsResultado.Tables(0).Rows(0).Item("telefono_cliente")
        End If

        ''BITACORA
        strMensajeBitacora = "Se realizó una busqueda de clientes con el NIT: " & strNit &
            ". El resutado fue " & IIf(Conexion.AccesoDatos.DatasetVacio(dsResultado), "fallido/sin datos.", "eeeeexitoso.")
        
        intResultadoBitacora = Conexion.AccesoDatos.ExecuteNonQuery("Bitacora_Insert",
                                                                    "@descripcion", strMensajeBitacora,
                                                                    "@servicio", "WS_Cliente.Buscar()",
                                                                    "@id_empleado", intIdEmpleado)

        Return cli
    End Function

End Class