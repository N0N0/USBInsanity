' clHelpfulThings.vb
' Copyright© 2007-2011 by A. Terwedow
'
' Diese Klasse ist Teil des Programmes USBInsanity.
' Sie bietet Funktionen für die Umformung und den Zugriff auf Strings.
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

Public Class clStrings

    ''' <summary>Schneidet einen String von  Links nach Rechts und liefert den abgetrennten Teil zurück.</summary>
    ''' <param name="sText">Ausgangsstring</param>
    ''' <param name="nLen">Anzahl zu schneidender Zeichen</param>
    Public Function funcLeft(ByVal sText As String, ByVal nLen As Integer) As String
        If nLen > sText.Length Then nLen = sText.Length
        Return (sText.Substring(0, nLen))
    End Function


    ''' <summary>Schneidet einen String von Rechts nach Links und liefert den abgetrennten Teil zurück.</summary>
    ''' <param name="sText">Ausgangsstring</param>
    ''' <param name="nLen">Anzahl zu schneidender Zeichen</param>
    Public Function funcRight(ByVal sText As String, ByVal nLen As Integer) As String
        If nLen > sText.Length Then nLen = sText.Length
        Return (sText.Substring(sText.Length - nLen))
    End Function

End Class
