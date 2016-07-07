Public Class frm_Persona

    Dim conexion As Class_Conexion = New Class_Conexion()
    Dim generoPersona As String


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conexion.Conectar()
        MostrarDatos()

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
            conexion.Consulta("sp_Busca_Persona " + ID.ToString, "Persona")
            dgv_datos.DataSource = conexion.ds.Tables("Persona")
        End If
    End Sub

    Public Sub MostrarDatos()
        conexion.Consulta("sp_Consulta_Persona", "Persona")
        dgv_datos.DataSource = conexion.ds.Tables("Persona")
    End Sub

    Private Sub IngresarNuevoRegistroToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles btnIngresar.Click
        Dim insertar As String = "sp_Ingresa_Persona " + txt_id.Text +
            ",'" + txt_nombre.Text + "'," +
            "" + cmb_edad.Text + "," +
            "'" + generoPersona.ToString + "'"

        If conexion.Insertar(insertar) Then
            MessageBox.Show("Datos agregados correctamente","Éxito",MessageBoxButtons.OK,MessageBoxIcon.Hand)
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
        Dim ID As String = "Ingrese ID"
        Dim Nombre As String = "Ingrese Nombre"

        ID = InputBox("Ingresa el ID de la persona que deseas actualizar el nombre", "Actualización por ID", ID)
        If ID = "" Then
            MessageBox.Show("Casilla vacía", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If


        Nombre = InputBox("Ingresa el nombre de la persona", "Actualización de nombre", Nombre)
        If Nombre = "" Then
            MessageBox.Show("Casilla vacía", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else

            Dim actualizarNombre As String = "sp_Actualiza_Nombre_Persona " + ID.ToString + "," + Nombre.ToString
            If conexion.Insertar(actualizarNombre) Then
                MessageBox.Show("Registro actualizado correctamente", "Éxito", MessageBoxButtons.OK)
                MostrarDatos()
                conexion.conexion.Close()
            Else
                MessageBox.Show("Error al actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                conexion.conexion.Close()
            End If
        End If
    End Sub

    Private Sub ActualizarEdadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActualizarEdadToolStripMenuItem.Click
        Dim ID As String = "Ingrese ID"
        Dim edad As String = "Ingrese Nombre"

        ID = InputBox("Ingresa el ID de la persona que deseas actualizar la edad", "Actualización por ID", ID)
        If ID = "" Then
            MessageBox.Show("Casilla vacía", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If


        edad = InputBox("Ingresa la edad de la persona", "Actualización de edad", edad)
        If edad = "" Then
            MessageBox.Show("Casilla vacía", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else

            Dim ActualizarEdad As String = "sp_Actualiza_edad_Persona " + ID.ToString + "," + edad.ToString
            If conexion.Insertar(ActualizarEdad) Then
                MessageBox.Show("Registro actualizado correctamente", "Éxito", MessageBoxButtons.OK)
                MostrarDatos()
                conexion.conexion.Close()
            Else
                MessageBox.Show("Error al actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                conexion.conexion.Close()
            End If
        End If
    End Sub

    Private Sub ActualizarGeneroToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActualizarGeneroToolStripMenuItem.Click
        Dim ID As String = "Ingrese ID"
        Dim Genero As String = "Ingrese genero"

        ID = InputBox("Ingresa el ID de la persona que deseas actualizar el genero", "Actualización por ID", ID)
        If ID = "" Then
            MessageBox.Show("Casilla vacía", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If


        Genero = InputBox("Ingresa el genero de la persona", "Actualización de edad", Genero)
        If Genero = "" Then
            MessageBox.Show("Casilla vacía", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else

            Dim actualizarGenero As String = "sp_Actualiza_genero_Persona " + ID.ToString + "," + Genero.ToString
            If conexion.Insertar(actualizarGenero) Then
                MessageBox.Show("Registro actualizado correctamente", "Éxito", MessageBoxButtons.OK)
                MostrarDatos()
                conexion.conexion.Close()
            Else
                MessageBox.Show("Error al actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                conexion.conexion.Close()
            End If
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked Then
            generoPersona = "Masculino"
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked Then
            generoPersona = "Femenino"
        End If
    End Sub

    Private Sub cmb_edad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_edad.SelectedIndexChanged
        
    End Sub

    Private Sub ConsultarTodosLosDatosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConsultarTodosLosDatosToolStripMenuItem.Click
        MostrarDatos()
    End Sub
End Class
