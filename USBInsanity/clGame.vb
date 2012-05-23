' clGame.vb
' Copyright© 2007-2011 by A. Terwedow
'
' Diese Klasse ist Teil des Programmes USBInsanity.
' Sie stellt Objekte des Typs clGame dar.
'
'
' Lizenzhinweis für USBInsanity, den USBInsanity Quellcode und alle damit
' verknüpften Module, Klassen und Begleitmaterialien:
'
' This program is free software; you can redistribute it and/or
' modify it under the terms of the GNU General Public License version 2
' as published by the Free Software Foundation.
'
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with this program; if not, write to the Free Software
' Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
' ///////////////////////////////////////////////////////////////////////////
' ///////////////////////////////////////////////////////////////////////////

Public Class clGame

    ' Attribute aus ul.cfg
    Private _title As String
    Private _elf As String
    Private _parts As Byte
    Private _discType As Byte

    ' Getter & Setter
    Public Function getTitle() As String
        Return _title
    End Function
    Public Function getElf() As String
        Return _elf
    End Function
    Public Function getParts() As Byte
        Return _parts
    End Function
    Public Function getDiscType() As Byte ' Gibt den HEX-Wert zurück
        Return _discType
    End Function
    Public Function getDiscTypeS() As String ' Gibt 'CD' oder 'DVD' zurück
        If _discType = &H14 Then
            Return "DVD"
        ElseIf _discType = &H12 Then
            Return "CD"
        Else
            Return "N/A"
        End If
    End Function

    Public Sub setDiscType(ByVal discType As Byte)
        _discType = discType
    End Sub
    Public Sub setDiscTypeS(ByVal disctype As String)
        If disctype = "DVD" Then
            _discType = &H14
        ElseIf disctype = "CD" Then
            _discType = &H12
        End If
    End Sub


    ' Konstruktor
    Public Sub New(ByVal title As String, ByVal elf As String, ByVal parts As Byte, ByVal discType As Byte)
        _title = title
        _elf = elf
        _parts = parts
        _discType = discType
    End Sub


End Class
