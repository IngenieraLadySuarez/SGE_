Imports SGE.BLL
Imports SGE.Entities

Public Class Form1
    Private _bll As New EmpleadoBLL()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarEmpleados()
    End Sub

    Private Sub CargarEmpleados()
        Try
            Dim empleados As List(Of Empleado) = _bll.ObtenerEmpleados()
            dgvEmpleados.DataSource = empleados

            If dgvEmpleados.Columns.Contains("IdDepartamento") Then
                dgvEmpleados.Columns("IdDepartamento").Visible = True
            End If
        Catch ex As Exception
            MessageBox.Show("Error al cargar los empleados: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        Try
            Dim criterio As String = txtBuscar.Text.Trim()
            Dim empleadosFiltrados As List(Of Empleado) = _bll.Buscar(criterio)

            If empleadosFiltrados.Count > 0 Then
                dgvEmpleados.DataSource = empleadosFiltrados
            Else
                CargarEmpleados()
                MessageBox.Show("No se encontró ningún empleado con ese criterio.", "Búsqueda sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LimpiarCampos()
            End If

        Catch ex As Exception
            MessageBox.Show("Error al realizar la búsqueda: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        Try
            Dim empleadoId As Integer
            If Not Integer.TryParse(txtId.Text, empleadoId) OrElse empleadoId <= 0 Then
                MessageBox.Show("El ID del empleado debe ser un número mayor que 0.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim salario As Decimal
            If Not Decimal.TryParse(TxtSalario.Text, salario) Then
                MessageBox.Show("El salario debe ser un número válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim departamentoId As Integer
            If Not Integer.TryParse(txtDepartamento.Text, departamentoId) Then
                MessageBox.Show("El ID de departamento debe ser un número válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim nuevoEmpleado As New Empleado()
            nuevoEmpleado.IdEmpleado = empleadoId
            nuevoEmpleado.NombreCompleto = txtNombre.Text
            nuevoEmpleado.FechaContratacion = txtFecha.Value
            nuevoEmpleado.Cargo = txtCargo.Text
            nuevoEmpleado.Salario = salario
            nuevoEmpleado.IdDepartamento = departamentoId

            _bll.Crear(nuevoEmpleado)

            MessageBox.Show("Empleado guardado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)

            CargarEmpleados()
            LimpiarCampos()

        Catch ex As ValidacionException
            MessageBox.Show(ex.Message, "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As Exception
            MessageBox.Show("Ocurrió un error inesperado al guardar el empleado: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnLimpiar_Click(sender As Object, e As EventArgs) Handles BtnLimpiar.Click
        LimpiarCampos()
        CargarEmpleados()
    End Sub

    Private Sub dgvEmpleados_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEmpleados.CellClick
        If e.RowIndex >= 0 Then
            Dim fila As DataGridViewRow = dgvEmpleados.Rows(e.RowIndex)

            txtId.Text = fila.Cells("IdEmpleado").Value.ToString()
            txtNombre.Text = fila.Cells("NombreCompleto").Value.ToString()
            txtFecha.Value = CDate(fila.Cells("FechaContratacion").Value)
            txtCargo.Text = fila.Cells("Cargo").Value.ToString()
            TxtSalario.Text = fila.Cells("Salario").Value.ToString()
            txtDepartamento.Text = fila.Cells("IdDepartamento").Value.ToString()
        End If
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Try
            Dim empleadoId As Integer
            If Not Integer.TryParse(txtId.Text, empleadoId) OrElse empleadoId <= 0 Then
                MessageBox.Show("El ID del empleado es inválido para la actualización.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim salario As Decimal
            If Not Decimal.TryParse(TxtSalario.Text, salario) Then
                MessageBox.Show("El salario debe ser un número válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim departamentoId As Integer
            If Not Integer.TryParse(txtDepartamento.Text, departamentoId) Then
                MessageBox.Show("El ID de departamento debe ser un número válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim empleadoActualizado As New Empleado With {
                .IdEmpleado = empleadoId,
                .NombreCompleto = txtNombre.Text,
                .FechaContratacion = txtFecha.Value,
                .Cargo = txtCargo.Text,
                .Salario = salario,
                .IdDepartamento = departamentoId
            }

            _bll.Actualizar(empleadoActualizado)

            MessageBox.Show("Empleado actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CargarEmpleados()
            LimpiarCampos()

        Catch ex As ValidacionException
            MessageBox.Show(ex.Message, "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As Exception
            MessageBox.Show("Ocurrió un error inesperado al actualizar el empleado: ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LimpiarCampos()
        txtId.Clear()
        txtNombre.Clear()
        txtFecha.Value = Date.Today
        txtCargo.Clear()
        TxtSalario.Clear()
        txtDepartamento.Clear()
        txtBuscar.Clear()
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Try
            Dim idEmpleado As Integer
            If Not Integer.TryParse(txtId.Text, idEmpleado) OrElse idEmpleado <= 0 Then
                MessageBox.Show("Por favor, seleccione un ID de empleado válido para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If MessageBox.Show("¿Está seguro de que desea eliminar el empleado con ID: " & idEmpleado & "?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                _bll.Eliminar(idEmpleado)

                MessageBox.Show("Empleado eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                CargarEmpleados()
                LimpiarCampos()
            End If

        Catch ex As ValidacionException
            MessageBox.Show(ex.Message, "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As Exception
            MessageBox.Show("Ocurrió un error inesperado al eliminar el empleado: ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtId_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtId.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TxtSalario_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtSalario.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso e.KeyChar <> "." Then
            e.Handled = True
        End If

        If e.KeyChar = "." AndAlso DirectCast(sender, TextBox).Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtDepartamento_KeyPres(sender As Object, e As KeyPressEventArgs) Handles txtDepartamento.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
End Class