﻿Public Class RDespachoNotaVentaXCliente
    Private _oanumi As Integer
    Public Property oanumi() As Integer
        Get
            Return _oanumi
        End Get
        Set(ByVal value As Integer)
            _oanumi = value
        End Set
    End Property

    Private _oafdoc As DateTime
    Public Property oafdoc() As DateTime
        Get
            Return _oafdoc
        End Get
        Set(ByVal value As DateTime)
            _oafdoc = value
        End Set
    End Property

    Private _ccdesc As String
    Public Property ccdesc() As String
        Get
            Return _ccdesc
        End Get
        Set(ByVal value As String)
            _ccdesc = value
        End Set
    End Property

    Private _cbdesc As String
    Public Property cbdesc() As String
        Get
            Return _cbdesc
        End Get
        Set(ByVal value As String)
            _cbdesc = value
        End Set
    End Property

    Private _canumi As Integer
    Public Property canumi() As Integer
        Get
            Return _canumi
        End Get
        Set(ByVal value As Integer)
            _canumi = value
        End Set
    End Property

    Private _cadesc As String
    Public Property cadesc() As String
        Get
            Return _cadesc
        End Get
        Set(ByVal value As String)
            _cadesc = value
        End Set
    End Property


    Private _obpcant As Decimal
    Public Property obpcant() As Decimal
        Get
            Return _obpcant
        End Get
        Set(ByVal value As Decimal)
            _obpcant = value
        End Set
    End Property


    Private _obpbase As Decimal
    Public Property obpbase() As Decimal
        Get
            Return _obpbase
        End Get
        Set(ByVal value As Decimal)
            _obpbase = value
        End Set
    End Property

    Private _obptot As Decimal
    Public Property obptot() As Decimal
        Get
            Return _obptot
        End Get
        Set(ByVal value As Decimal)
            _obptot = value
        End Set
    End Property
    Private _Total As Decimal
    Public Property Total() As Decimal
        Get
            Return _Total
        End Get
        Set(ByVal value As Decimal)
            _Total = value
        End Set
    End Property


    Private _TotalLiteral As String
    Public Property TotalLiteral() As String
        Get
            Return _TotalLiteral
        End Get
        Set(ByVal value As String)
            _TotalLiteral = value
        End Set
    End Property

    Private _oaobs As String
    Public Property oaobs() As String
        Get
            Return _oaobs
        End Get
        Set(ByVal value As String)
            _oaobs = value
        End Set
    End Property

End Class
