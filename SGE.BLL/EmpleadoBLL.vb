Imports SGE.DAL
Imports SGE.Entities
Imports System.Linq

Public Class EmpleadoBLL

    Private ReadOnly _dal As IEmpleadoDAL

    Public Sub New()
        _dal = New EmpleadoDAL()
    End Sub

    Public Function ObtenerEmpleados() As List(Of Empleado)
        Return _dal.ObtenerTodosLosEmpleados()
    End Function

    Public Function Buscar(criterio As String) As List(Of Empleado)
        If String.IsNullOrWhiteSpace(criterio) Then
            Return _dal.ObtenerTodosLosEmpleados()
        Else
            Return _dal.BuscarEmpleados(criterio)
        End If
    End Function

    Public Sub Crear(empleado As Empleado)
        If _dal.ExisteEmpleado(empleado.IdEmpleado) Then
            Throw New ValidacionException("Ya existe un empleado con el ID: " & empleado.IdEmpleado)
        End If
        If String.IsNullOrWhiteSpace(empleado.NombreCompleto) Then
            Throw New ValidacionException("El nombre del empleado no puede estar vacío.")
        End If
        If String.IsNullOrWhiteSpace(empleado.Cargo) Then
            Throw New ValidacionException("El cargo del empleado no puede estar vacío.")
        End If
        If empleado.FechaContratacion < Date.Today Then
            Throw New ValidacionException("La fecha de contratación no puede ser anterior al día actual.")
        End If
        If empleado.Salario <= 0 Then
            Throw New ValidacionException("El salario debe ser un valor positivo.")
        End If
        If empleado.IdDepartamento <= 0 Then
            Throw New ValidacionException("El ID del departamento debe ser un número positivo.")
        End If

        _dal.CrearEmpleado(empleado)
    End Sub

    Public Sub Actualizar(empleado As Empleado)
        If Not _dal.ExisteEmpleado(empleado.IdEmpleado) Then
            Throw New ValidacionException("El empleado con el ID " & empleado.IdEmpleado & " no existe para ser actualizado.")
        End If
        If empleado.IdEmpleado <= 0 Then
            Throw New ValidacionException("El ID del empleado es inválido para la actualización.")
        End If
        If String.IsNullOrWhiteSpace(empleado.NombreCompleto) Then
            Throw New ValidacionException("El nombre del empleado no puede estar vacío.")
        End If
        If String.IsNullOrWhiteSpace(empleado.Cargo) Then
            Throw New ValidacionException("El cargo del empleado no puede estar vacío.")
        End If
        If empleado.Salario <= 0 Then
            Throw New ValidacionException("El salario debe ser un valor positivo.")
        End If
        If empleado.IdDepartamento <= 0 Then
            Throw New ValidacionException("El ID del departamento debe ser un número positivo.")
        End If

        _dal.ActualizarEmpleado(empleado)
    End Sub

    Public Sub Eliminar(idEmpleado As Integer)
        If idEmpleado <= 0 Then
            Throw New ValidacionException("El ID del empleado es inválido para la eliminación.")
        End If
        If Not _dal.ExisteEmpleado(idEmpleado) Then
            Throw New ValidacionException("El empleado con el ID " & idEmpleado & " no existe para ser eliminado.")
        End If

        _dal.EliminarEmpleado(idEmpleado)
    End Sub
End Class