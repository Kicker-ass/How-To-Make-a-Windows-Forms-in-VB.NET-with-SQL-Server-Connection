Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms

Public Class Class_Conexion
    Public conexion As SqlConnection = New SqlConnection("Data Source=HP\SQLSERVER2012;Initial Catalog=Examen;Integrated Security=True")
    Private cmb As SqlCommandBuilder
    Public ds As DataSet = New DataSet
    Public da As SqlDataAdapter
    Public cmd As SqlCommand

    Public Sub Conectar()
        Try
            conexion.Open()
            MessageBox.Show("Conexión exitosa","Conexión",MessageBoxButtons.OK,MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error al conectar","Conexión",MessageBoxButtons.OK,MessageBoxIcon.Error)
        Finally
            conexion.Close()
        End Try
    End Sub

    Public Sub Consulta(ByVal sql As String, ByVal Tabla As String)
        Try
            ds.Tables.Clear()
            da = New SqlDataAdapter(sql, conexion)
            cmb = New SqlCommandBuilder(da)
            da.Fill(ds, Tabla)
        Catch ex As Exception
            MessageBox.Show("Error al mostrar datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Function Insertar(ByVal sql)
        Try
            conexion.Open()
            cmd = New SqlCommand(sql, conexion)
            Dim i As Integer = cmd.ExecuteNonQuery()
            If (i > 0) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

End Class