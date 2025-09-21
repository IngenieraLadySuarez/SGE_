Imports System.Data.SqlClient
Imports SGE.Entities
Imports System.Configuration

Public Class EmpleadoDAL
    Implements IEmpleadoDAL

    Private ReadOnly _conexion As String = ConfigurationManager.ConnectionStrings("SGE.DB.Connection").ConnectionString

    Public Function ObtenerTodosLosEmpleados() As List(Of Empleado) Implements IEmpleadoDAL.ObtenerTodosLosEmpleados
        Dim lista As New List(Of Empleado)()
        Using conexion As New SqlConnection(_conexion)
            Dim comando As New SqlCommand("SELECT IdEmpleado, NombreCompleto, FechaContratacion, Cargo, Salario, IdDepartamento FROM Empleados", conexion)
            conexion.Open()
            Dim lector As SqlDataReader = comando.ExecuteReader()
            While lector.Read()
                Dim emp As New Empleado()
                emp.IdEmpleado = lector.GetInt32(0)
                emp.NombreCompleto = lector.GetString(1)
                emp.FechaContratacion = lector.GetDateTime(2)
                emp.Cargo = lector.GetString(3)
                emp.Salario = lector.GetDecimal(4)
                emp.IdDepartamento = lector.GetInt32(5)
                lista.Add(emp)
            End While
        End Using
        Return lista
    End Function

    Public Function BuscarEmpleados(criterio As String) As List(Of Empleado) Implements IEmpleadoDAL.BuscarEmpleados
        Dim lista As New List(Of Empleado)()
        Dim query As String = "SELECT IdEmpleado, NombreCompleto, FechaContratacion, Cargo, Salario, IdDepartamento FROM Empleados "

        Using conexion As New SqlConnection(_conexion)
            Dim comando As New SqlCommand()
            comando.Connection = conexion

            Dim id As Integer

            If Integer.TryParse(criterio, id) Then
                query &= "WHERE IdEmpleado = @criterioId"
                comando.CommandText = query
                comando.Parameters.AddWithValue("@criterioId", id)
            Else
                query &= "WHERE NombreCompleto COLLATE Latin1_General_CI_AI LIKE @criterioNombre"
                comando.CommandText = query
                comando.Parameters.AddWithValue("@criterioNombre", "%" & criterio & "%")
            End If

            conexion.Open()
            Dim lector As SqlDataReader = comando.ExecuteReader()
            While lector.Read()
                Dim emp As New Empleado()
                emp.IdEmpleado = lector.GetInt32(0)
                emp.NombreCompleto = lector.GetString(1)
                emp.FechaContratacion = lector.GetDateTime(2)
                emp.Cargo = lector.GetString(3)
                emp.Salario = lector.GetDecimal(4)
                emp.IdDepartamento = lector.GetInt32(5)
                lista.Add(emp)
            End While
        End Using
        Return lista
    End Function

    Public Sub CrearEmpleado(empleado As Empleado) Implements IEmpleadoDAL.CrearEmpleado
        Using conexion As New SqlConnection(_conexion)
            Dim query As String = "INSERT INTO Empleados (NombreCompleto, FechaContratacion, Cargo, Salario, IdDepartamento) VALUES (@nombre, @fecha, @cargo, @salario, @idDep)"
            Dim comando As New SqlCommand(query, conexion)
            comando.Parameters.AddWithValue("@nombre", empleado.NombreCompleto)
            comando.Parameters.AddWithValue("@fecha", empleado.FechaContratacion)
            comando.Parameters.AddWithValue("@cargo", empleado.Cargo)
            comando.Parameters.AddWithValue("@salario", empleado.Salario)
            comando.Parameters.AddWithValue("@idDep", empleado.IdDepartamento)
            conexion.Open()
            Dim filasAfectadas As Integer = comando.ExecuteNonQuery()
            If filasAfectadas = 0 Then
                Throw New Exception("No se pudo crear el empleado.")
            End If
        End Using
    End Sub

    Public Sub ActualizarEmpleado(empleado As Empleado) Implements IEmpleadoDAL.ActualizarEmpleado
        Using conexion As New SqlConnection(_conexion)
            Dim query As String = "UPDATE Empleados SET NombreCompleto = @nombre, FechaContratacion = @fecha, Cargo = @cargo, Salario = @salario, IdDepartamento = @idDep WHERE IdEmpleado = @idEmp"
            Dim comando As New SqlCommand(query, conexion)
            comando.Parameters.AddWithValue("@nombre", empleado.NombreCompleto)
            comando.Parameters.AddWithValue("@fecha", empleado.FechaContratacion)
            comando.Parameters.AddWithValue("@cargo", empleado.Cargo)
            comando.Parameters.AddWithValue("@salario", empleado.Salario)
            comando.Parameters.AddWithValue("@idDep", empleado.IdDepartamento)
            comando.Parameters.AddWithValue("@idEmp", empleado.IdEmpleado)
            conexion.Open()
            Dim filasAfectadas As Integer = comando.ExecuteNonQuery()
            If filasAfectadas = 0 Then
                Throw New Exception("No se pudo actualizar el empleado. Es posible que el ID no exista.")
            End If
        End Using
    End Sub

    Public Sub EliminarEmpleado(idEmpleado As Integer) Implements IEmpleadoDAL.EliminarEmpleado
        Using conexion As New SqlConnection(_conexion)
            Dim query As String = "DELETE FROM Empleados WHERE IdEmpleado = @idEmp"
            Dim comando As New SqlCommand(query, conexion)
            comando.Parameters.AddWithValue("@idEmp", idEmpleado)
            conexion.Open()
            Dim filasAfectadas As Integer = comando.ExecuteNonQuery()
            If filasAfectadas = 0 Then
                Throw New Exception("No se pudo eliminar el empleado. Es posible que el ID no exista.")
            End If
        End Using
    End Sub

    Public Function ExisteEmpleado(idEmpleado As Integer) As Boolean Implements IEmpleadoDAL.ExisteEmpleado
        Dim existe As Boolean = False
        Dim query As String = "SELECT COUNT(*) FROM Empleados WHERE IdEmpleado = @idEmp"
        Using conexion As New SqlConnection(_conexion)
            Dim comando As New SqlCommand(query, conexion)
            comando.Parameters.AddWithValue("@idEmp", idEmpleado)
            conexion.Open()
            Dim count As Object = comando.ExecuteScalar()
            If count IsNot Nothing AndAlso CInt(count) > 0 Then
                existe = True
            End If
        End Using
        Return existe
    End Function
End Class