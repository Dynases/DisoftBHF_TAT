Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports ENTITY
Imports Janus.Windows.GridEX
Imports LOGIC
Imports UTILITIES
Imports Logica.AccesoLogica




Public Class frmBillingDispatch

    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem

    Private _cargaCompleta = False

#Region "Eventos"
    Private Sub frmBillingDispatch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Init()
    End Sub

    Private Sub cbChoferes_ValueChanged(sender As Object, e As EventArgs) Handles cbChoferes.ValueChanged
        Try
            If (_cargaCompleta) Then
                CargarPedidos()
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub btFacturar_Click(sender As Object, e As EventArgs) Handles btFacturar.Click
        Try
            Dim idChofer = Me.cbChoferes.Value
            Dim idDespacho = dgjPedido.GetValue("Id")
            If (Not IsNumeric(idChofer)) Then
                Throw New Exception("Debe seleccionar un chofer.")
            End If
            If (Convert.ToInt32(idChofer) = ENCombo.ID_SELECCIONAR) Then
                Throw New Exception("Debe seleccionar un chofer.")
            End If

            Dim checks = Me.dgjPedido.GetCheckedRows()
            Dim listIdPedido = checks.Select(Function(a) Convert.ToInt32(a.Cells("Id").Value)).ToList()
            For Each idPedido As Integer In listIdPedido
                Dim listResult = New LPedido().ListarDespachoXNotaVentaDeChofer(idChofer, idPedido)
                If (listResult.Count = 0) Then
                    Throw New Exception("No registros para generar el reporte.")
                End If
                If Not IsNothing(P_Global.Visualizador) Then
                    P_Global.Visualizador.Close()
                End If
                Dim _Hora As String = Now.Hour.ToString + ":" + Now.Minute.ToString
                Dim _Total As Decimal = listResult.Item(0).Total
                Dim _Decimal As Decimal = _Total - Math.Truncate(_Total)
                Dim _TotalDeciaml As String = CDbl(_Decimal) * 100

                Dim Literal As String = UConvertirLiteral.A_fnConvertirLiteral(CDbl(_Total) - CDbl(_Decimal)) + "  " + IIf(_TotalDeciaml.Equals("0"), "00", _TotalDeciaml) + "/100 Bolivianos"

                P_Global.Visualizador = New Visualizador
                Dim objrep As New DespachoNotaVentaXCliente

                objrep.SetDataSource(listResult)
                objrep.SetParameterValue("Hora", _Hora)
                objrep.SetParameterValue("Literal1", Literal)
                objrep.SetParameterValue("Usuario", P_Global.gs_user)

                objrep.PrintOptions.PrinterName = "EPSON TM-T2011 Receipt"
                objrep.PrintToPrinter(1, False, 1, 1)

                'P_Global.Visualizador.CRV1.ReportSource = objrep
                'P_Global.Visualizador.Show()
                'P_Global.Visualizador.BringToFront()
            Next
            'Dim listResult = New LPedido().ListarDespachoXNotaVenta(idChofer, listIdPedido)

        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub btReporteDespachoCliente_Click(sender As Object, e As EventArgs) Handles btReporteDespachoCliente.Click
        Try
            Dim idChofer = Me.cbChoferes.Value
            If (Not IsNumeric(idChofer)) Then
                Throw New Exception("Debe seleccionar un chofer.")
            End If
            If (Convert.ToInt32(idChofer) = ENCombo.ID_SELECCIONAR) Then
                Throw New Exception("Debe seleccionar un chofer.")
            End If

            Dim checks = Me.dgjPedido.GetCheckedRows()
            Dim listIdPedido = checks.Select(Function(a) Convert.ToInt32(a.Cells("Id").Value)).ToList()

            If (listIdPedido.Count = 0) Then
                ToastNotification.Show(Me, "No Existe ningun dato a Facturar!!".ToUpper,
                                    My.Resources.OK,
                                    5 * 1000,
                                    eToastGlowColor.Red,
                                    eToastPosition.MiddleCenter)
                Return
            End If
            Dim listResult = New LPedido().ListarDespachoXClienteDeChofer(idChofer)
            Dim listFiltrada = New LPedido().ListarDespachoXClienteDeChofer(idChofer)
            listFiltrada.Clear()

            Dim list2 As List(Of VPedido_BillingDispatch) = CType(dgjPedido.DataSource, List(Of VPedido_BillingDispatch))
            Dim list1 As List(Of VPedido_BillingDispatch) = New List(Of VPedido_BillingDispatch)
            ''Dim dtContenido As DataTable = CType(dgjPedido.DataSource, DataTable)
            ''dt.Rows.Clear()


            For i As Integer = 0 To list2.Count - 1 Step 1

                For j As Integer = 0 To listIdPedido.Count - 1 Step 1
                    If (list2(i).Id = listIdPedido(j)) Then
                        list1.Add(list2(i))
                    End If
                Next
            Next

            For i As Integer = 0 To listResult.Count - 1 Step 1

                For j As Integer = 0 To list1.Count - 1 Step 1

                    If (listResult(i).ccdesc.Equals(list1(j).NombreCliente)) Then
                        listFiltrada.Add(listResult(i))
                    End If
                Next
            Next


            If (listResult.Count = 0) Then
                Throw New Exception("No registros para generar el reporte.")
            End If

            If Not IsNothing(P_Global.Visualizador) Then
                P_Global.Visualizador.Close()
            End If

            P_Global.Visualizador = New Visualizador
            Dim objrep As New DespachoXCliente

            objrep.SetDataSource(listFiltrada)
            objrep.SetParameterValue("nroDespacho", String.Empty)
            objrep.SetParameterValue("nombreDistribuidor", cbChoferes.Text)
            objrep.SetParameterValue("nombreUsuario", P_Global.gs_user)

            P_Global.Visualizador.CRV1.ReportSource = objrep
            P_Global.Visualizador.Show()
            P_Global.Visualizador.BringToFront()
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub btReporteDespachoLinea_Click(sender As Object, e As EventArgs) Handles btReporteDespachoLinea.Click
        Try
            Dim idChofer = Me.cbChoferes.Value
            If (Not IsNumeric(idChofer)) Then
                Throw New Exception("Debe seleccionar un chofer.")
            End If
            If (Convert.ToInt32(idChofer) = ENCombo.ID_SELECCIONAR) Then
                Throw New Exception("Debe seleccionar un chofer.")
            End If



            Dim checks = Me.dgjPedido.GetCheckedRows()
            Dim listIdPedido = checks.Select(Function(a) Convert.ToInt32(a.Cells("Id").Value)).ToList()



            Dim dtList As DataTable = L_prTypeId()

            For j As Integer = 0 To listIdPedido.Count - 1 Step 1

                dtList.Rows.Add(listIdPedido(j))
            Next

            Dim dtResult As DataTable = L_prObtenerListado(dtList, idChofer)

            If (dtResult.Rows.Count = 0) Then
                Throw New Exception("No registros para generar el reporte.")
            End If

            If Not IsNothing(P_Global.Visualizador) Then
                P_Global.Visualizador.Close()
            End If

            P_Global.Visualizador = New Visualizador
            Dim objrep As New DespachoXProducto

            objrep.SetDataSource(dtResult)
            objrep.SetParameterValue("nroDespacho", String.Empty)
            objrep.SetParameterValue("nombreDistribuidor", cbChoferes.Text)
            objrep.SetParameterValue("nombreUsuario", P_Global.gs_user)

            P_Global.Visualizador.CRV1.ReportSource = objrep
            P_Global.Visualizador.Show()
            P_Global.Visualizador.BringToFront()
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub dgjPedido_SelectionChanged(sender As Object, e As EventArgs) Handles dgjPedido.SelectionChanged
        Try
            Dim idPedido = 0
            If (dgjPedido.GetRows().Count > 0) Then
                idPedido = Convert.ToInt32(dgjPedido.CurrentRow.Cells("Id").Value)
            End If

            CargarProductos(idPedido)
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
#End Region

#Region "Privado, metodos y funciones"
    Private Sub Init()
        Try

            ''L_prAbrirConexion()

            ConfigForm()
            CargarChoferes()
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub ConfigForm()
        Try
            Me.Text = "FACTURACIÓN/DESPACHO"
            Me.WindowState = FormWindowState.Maximized
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub CargarChoferes()
        Try
            Dim listResult As List(Of VCombo) = New LPersonal().ListarRepatidorCombo()

            With cbChoferes.DropDownList
                .Columns.Clear()

                .Columns.Add("Id").Width = 30
                .Columns("Id").Caption = "Id"
                .Columns("Id").Visible = True

                .Columns.Add("Descripcion").Width = 180
                .Columns("Descripcion").Caption = "Nombre repartidor"
                .Columns("Descripcion").Visible = True

                .ValueMember = "Id"
                .DisplayMember = "Descripcion"
                .DataSource = listResult

                .AlternatingColors = True
                .AllowColumnDrag = False
                .AutomaticSort = False
                .Refresh()
            End With
            cbChoferes.VisualStyle = VisualStyle.Office2007

            cbChoferes.SelectedIndex = 0
            _cargaCompleta = True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub CargarPedidos()
        Try
            Dim idChofer = Me.cbChoferes.Value
            If (Not IsNumeric(idChofer)) Then
                Throw New Exception("Debe seleccionar un chofer.")
            End If
            If (Convert.ToInt32(idChofer) = ENCombo.ID_SELECCIONAR) Then
                Throw New Exception("Debe seleccionar un chofer.")
            End If

            Dim listResult = New LPedido().ListarPedidoAsignadoAChofer(idChofer)

            dgjPedido.BoundMode = Janus.Data.BoundMode.Bound
            dgjPedido.DataSource = listResult
            dgjPedido.RetrieveStructure()

            With dgjPedido.RootTable.Columns("Fecha")
                .Caption = "Fecha"
                .Width = 80
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = True
                .Position = 0
            End With

            With dgjPedido.RootTable.Columns("NombreCliente")
                .Caption = "Cliente"
                .Width = 400
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
                .Position = 1
            End With

            With dgjPedido.RootTable.Columns("Id")
                .Caption = "Pedido"
                .Width = 60
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = True
                .Position = 2
            End With

            With dgjPedido.RootTable.Columns("NombreVendedor")
                .Caption = "Vendedor"
                .Width = 400
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
                .Position = 3
            End With

            dgjPedido.RootTable.Columns.Add(New GridEXColumn("Check"))
            With dgjPedido.RootTable.Columns("Check")
                .Caption = "N.Venta"
                .Width = 80
                .ShowRowSelector = True
                .UseHeaderSelector = True
                .FilterEditType = FilterEditType.NoEdit
                .Position = 4
            End With

            With dgjPedido.RootTable.Columns("NroFactura")
                .Caption = "Número"
                .Width = 80
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = True
                .Position = 5
            End With

            With dgjPedido
                .GroupByBoxVisible = False
                .DefaultFilterRowComparison = FilterConditionOperator.Contains
                .FilterMode = FilterMode.Automatic
                .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
                .VisualStyle = VisualStyle.Office2007
                .SelectionMode = SelectionMode.MultipleSelection
                .AlternatingColors = True
                .AllowEdit = InheritableBoolean.False
                .AllowColumnDrag = False
                .AutomaticSort = False
                '.ColumnHeaders = InheritableBoolean.False
            End With
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub CargarProductos(idPedido As Integer)
        Try
            Dim listResult = New LProducto().ListarProductoXPedido(idPedido)

            dgjProducto.BoundMode = Janus.Data.BoundMode.Bound
            dgjProducto.DataSource = listResult
            dgjProducto.RetrieveStructure()

            With dgjProducto.RootTable.Columns("Id")
                .Visible = False
            End With

            With dgjProducto.RootTable.Columns("NombreProducto")
                .Caption = "Producto"
                .Width = 250
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
            End With

            With dgjProducto.RootTable.Columns("Cantidad")
                .Caption = "Cantidad"
                .Width = 80
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = True
                .FormatString = "0"
            End With

            With dgjProducto.RootTable.Columns("Precio")
                .Caption = "Precio"
                .Width = 80
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = True
                .FormatString = "0.00"
            End With

            With dgjProducto.RootTable.Columns("Total")
                .Caption = "Total"
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = True
                .FormatString = "0.00"
            End With

            With dgjProducto
                .GroupByBoxVisible = False
                .DefaultFilterRowComparison = FilterConditionOperator.Contains
                '.FilterMode = FilterMode.Automatic
                .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
                .VisualStyle = VisualStyle.Office2007
                .SelectionMode = SelectionMode.MultipleSelection
                .AlternatingColors = True
                .AllowEdit = InheritableBoolean.False
                .AllowColumnDrag = False
                .AutomaticSort = False
                '.ColumnHeaders = InheritableBoolean.False
            End With
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub MostrarMensajeError(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.WARNING,
                               ENMensaje.MEDIANO,
                               eToastGlowColor.Red,
                               eToastPosition.TopCenter)
    End Sub
#End Region

#Region "Publico, metodos y funciones"
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click

    End Sub
#End Region
End Class