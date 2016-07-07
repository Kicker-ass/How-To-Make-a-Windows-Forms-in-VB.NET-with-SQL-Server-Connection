Imports System.Data.SqlClient

Public Class frm_Persona

    Dim conexion As Class_Conexion = New Class_Conexion()
    Dim generoPersona As Integer
    Dim adaptador As SqlDataAdapter
    Dim tabla As DataTable

    Public Sub llenarCombo()
        For Each fila As DataRow In tabla.Rows
            cmb_genero.Items.Add(fila("Genero"))
        Next
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conexion.Conectar()
        MostrarDatos()
        adaptador = New SqlDataAdapter("Select * from Genero", conexion.conexion)
        tabla = New DataTable
        adaptador.Fill(tabla)
        llenarCombo()

        For i = 1 To 120
            cmb_edad.Items.Add(i.ToString)
        Next

    End Sub

    Public Sub Busqueda()
        Dim ID As String = "Ingrese ID"
        ID = InputBox("Ingresa el ID de la persona que desea buscar", "Búsqueda por ID", ID)
        If ID = "" Then
            MessageBox.Show("Casilla vacía", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Try
                conexion.Consulta("sp_Busca_Persona " + ID.ToString, "Persona")
                dgv_datos.DataSource = conexion.ds.Tables("Persona")
            Catch ex As Exception
                MostrarDatos()
            End Try
        End If
    End Sub

    Public Sub MostrarDatos()
        conexion.Consulta("sp_Consulta_Persona", "Persona")
        dgv_datos.DataSource = conexion.ds.Tables("Persona")
    End Sub

    Private Sub IngresarNuevoRegistroToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles btnIngresar.Click
        If cmb_genero.SelectedItem = "MASCULINO" Then
            generoPersona = 1
        ElseIf cmb_genero.SelectedItem = "FEMENINO" Then
            generoPersona = 2
        End If

        Dim insertar As String = "sp_Ingresa_Persona " + txt_id.Text +
            ",'" + txt_nombre.Text + "'," +
            "" + cmb_edad.Text + "," +
            "" + generoPersona.ToString + ""

        If conexion.Insertar(insertar) Then
            MessageBox.Show("Datos agregados correctamente", "Éxito", MessageBoxButtons.OK)
            MostrarDatos()
            conexion.conexion.Close()

        Else
            MessageBox.Show("Error al agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            conexion.conexion.Close()
        End If

    End Sub

    Private Sub SalirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalirToolStripMenuItem.Click
        Dim R As Integer
        Dim Cancel As Integer
        R = MsgBox("¿Estás seguro de que deseas salir? ", vbExclamation + vbYesNo, "Salir")
        If R = vbNo Then
            Cancel = 1
        Else
            Me.Close()
        End If
    End Sub

    Private Sub BuscarPersonaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BuscarPersonaToolStripMenuItem.Click
        Busqueda()
    End Sub

    Private Sub EliminarPersonaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EliminarPersonaToolStripMenuItem.Click
        Dim ID As String = "Ingrese ID"
        ID = InputBox("Ingresa el ID de la persona que deseas eliminar", "Eliminación por ID", ID)
        If ID = "" Then
            MessageBox.Show("Casilla vacía", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else

            Dim eliminar As String = "sp_Elimina_Persona " + ID.ToString
            If conexion.Insertar(eliminar) Then
                MessageBox.Show("Registro eliminado correctamente", "Éxito", MessageBoxButtons.OK)
                MostrarDatos()
                conexion.conexion.Close()
            Else
                MessageBox.Show("Error al eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                conexion.conexion.Close()
            End If
        End If

    End Sub

    Private Sub ActualizarNombreToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActualizarNombreToolStripMenuItem.Click
        Dim nombre_sp As String = "sp_Actualiza_Nombre_Persona "
        updateData(nombre_sp)
    End Sub

    Private Sub ActualizarEdadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActualizarEdadToolStripMenuItem.Click
        Dim nombre_sp As String = "Sp_Actualiza_Edad_Persona "
        updateData(nombre_sp)
    End Sub

    Private Sub ActualizarGeneroToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActualizarGeneroToolStripMenuItem.Click
        Dim nombre_sp As String = "Sp_Actualiza_Genero_Persona "
        updateGender(nombre_sp)
    End Sub

    Private Sub ConsultarTodosLosDatosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConsultarTodosLosDatosToolStripMenuItem.Click
        MostrarDatos()
    End Sub

    Private Sub updateData(ByVal name_sp As String)
        Dim ID As String = "Ingrese ID"
        Dim Dato As String = "Por favor, ingresa el nuevo dato."

inicio:
        ID = InputBox("Ingresa el ID de la persona que deseas actualizar", "Actualización por ID", ID)
        If ID = "" Then
            MessageBox.Show("Casilla vacía", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            GoTo fin
        End If

        Dato = InputBox("Ingresa el dato de la persona", "Actualización", Dato)
        If Dato = "" Then
            MessageBox.Show("Casilla vacía", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else

            Dim actualizaDato As String = name_sp + ID.ToString + "," + Dato.ToString
            If conexion.Insertar(actualizaDato) Then
                MessageBox.Show("Registro actualizado correctamente", "Éxito", MessageBoxButtons.OK)
                MostrarDatos()
                conexion.conexion.Close()
            Else
                MessageBox.Show("Error al actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                conexion.conexion.Close()
            End If
        End If
fin:
    End Sub

    Private Sub updateGender(ByVal name_sp As String)
        Dim ID As String = "Ingrese ID"
        Dim Dato As String = "Por favor, ingresa el nuevo dato."

inicio:
        ID = InputBox("Ingresa el ID de la persona que deseas actualizar", "Actualización por ID", ID)
        If ID = "" Then
            MessageBox.Show("Casilla vacía", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            GoTo fin
        End If

        Dato = InputBox("Ingresa el dato de la persona", "Actualización", Dato)
        If Dato = "" Then
            MessageBox.Show("Casilla vacía", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else

            If Dato = "masculino" Or Dato = "Masculino" Or Dato = "MASCULINO" Then
                Dato = 1
            ElseIf Dato = "femenino" Or Dato = "Femenino" Or Dato = "FEMENINO" Then
                Dato = 2
            End If

            Dim actualizaDato As String = name_sp + ID.ToString + "," + Dato.ToString
            If conexion.Insertar(actualizaDato) Then
                MessageBox.Show("Registro actualizado correctamente", "Éxito", MessageBoxButtons.OK)
                MostrarDatos()
                conexion.conexion.Close()
            Else
                MessageBox.Show("Error al actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                conexion.conexion.Close()
            End If
        End If
fin:
    End Sub

End Class