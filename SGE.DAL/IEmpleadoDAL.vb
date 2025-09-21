Imports SGE.Entities

Public Interface IEmpleadoDAL
    Function ObtenerTodosLosEmpleados() As List(Of Empleado)
    Function BuscarEmpleados(criterio As String) As List(Of Empleado)
    Sub CrearEmpleado(empleado As Empleado)
    Sub ActualizarEmpleado(empleado As Empleado)
    Sub EliminarEmpleado(idEmpleado As Integer)
    Function ExisteEmpleado(idEmpleado As Integer) As Boolean
End Interface