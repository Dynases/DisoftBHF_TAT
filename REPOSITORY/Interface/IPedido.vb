﻿Imports ENTITY

Public Interface IPedido
    Function ListarPedidoDistribucion(listIdZona As List(Of Integer)) As List(Of VPedido_Dispatch)
    Function ListarPedidoAsignadoAChofer(idChofer As Integer) As List(Of VPedido_BillingDispatch)
    Function GuardarPedidoDeChofer(listIdPedido As List(Of Integer), idChofer As Integer) As Boolean
    Function ListarDespachoXClienteDeChofer(idChofer As Integer) As List(Of RDespachoxCliente)
    Function ListarDespachoXProductoDeChofer(idChofer As Integer) As List(Of RDespachoXProducto)
    Function ListarDespachoXProductoDeChoferSalida(idChofer As Integer) As List(Of RDespachoXProducto)
    Function ListarDespachXoNotaVentaDeChofer(idChofer As Integer, idDespacho As Integer) As List(Of RDespachoNotaVentaXCliente)
End Interface
