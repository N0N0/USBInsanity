' clFileAccess.vb
' Copyright© 2007-2011 by A. Terwedow
'
' Diese Klasse ist Teil des Programmes USBInsanity.
' Sie bietet Funktionen für die Arbeit auf Laufwerke und Dateien.
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

Imports System.IO

Public Class clFileAccess

    ' Sucht und liest Informationen aus der System.cnf der gewählten DVD
    Public Function ReadSystemCNF(ByVal srcDrive As String) As String

        On Error Resume Next
        Dim CNF_Laenge As Integer
        Dim bootelf As Boolean = False
        Dim i As Integer
        Dim CNF_Inhalt, CNF_Kurz, CNF_Backup As String
        Dim dateiname As String = ""
        Dim str As New clStrings()

        ' Suche System.CNF
        CNF_Laenge = FileLen(srcDrive & "SYSTEM.CNF")

        ' Wurde keine System.cnf gefunden suche eine BOOT.ELF
        If CNF_Laenge = 0 Then
            bootelf = File.Exists(srcDrive & "BOOT.ELF")
        End If

        If bootelf = True Then Return "BOOT.ELF" ' wurde eine BOOT.ELF gefunden gib Ihren namen zurück

        If CNF_Laenge = 0 And bootelf = 0 Then ' wurde nichts gefunden sag bescheid und beende
            MsgBox("Please insert Playstation 2 CD/DVD in Drive " & srcDrive & " .", MsgBoxStyle.Critical, "Error")
            Return ""
            Exit Function
        End If

        ' Lese System.CNF
        ' Datei als Stream mit Leserechten öffnen
        Dim fs As New FileStream(srcDrive & "SYSTEM.CNF", FileMode.Open, FileAccess.Read)
        ' Stream binär öffnen
        Dim r As BinaryReader = New BinaryReader(fs)
        ' Pointer setzen
        r.BaseStream.Seek(0, SeekOrigin.Begin)
        ' Eingelesenen Wert zurückgeben
        CNF_Inhalt = (r.ReadChars(CNF_Laenge))
        ' Stream wieder schließen
        fs.Close()

        ' Müll, um den Dateinamen abschneiden
        ' links
        While str.funcLeft(CNF_Inhalt, 1) <> "\"
            CNF_Inhalt = str.funcRight(CNF_Inhalt, Len(CNF_Inhalt) - 1)
        End While
        CNF_Inhalt = str.funcRight(CNF_Inhalt, Len(CNF_Inhalt) - 1)
        ' und rechts
        CNF_Inhalt = str.funcLeft(CNF_Inhalt, 12)
        ' prüfe ob der Dateiname kürzer als 8.3 ist
        While str.funcRight(CNF_Inhalt, 1) <> ";"
            CNF_Inhalt = str.funcLeft(CNF_Inhalt, Len(CNF_Inhalt) - 1)
        End While
        ' schneide das ";" ab
        CNF_Inhalt = str.funcLeft(CNF_Inhalt, Len(CNF_Inhalt) - 1)

        Return CNF_Inhalt
    End Function

    ' Schreibt einen Binärstring
    Public Function funcBinaryWrite(ByVal sFile As String, ByVal sOffset As String, ByVal sData As ULong) As Byte
        On Error GoTo errors
        ' Datei als stream mit Schreibzugriff öffnen
        Dim fs As New FileStream(sFile, FileMode.OpenOrCreate, FileAccess.ReadWrite)
        ' Stream binär öffnen
        Dim r As BinaryWriter = New BinaryWriter(fs)
        ' Pointer setzen)
        r.BaseStream.Seek(sOffset, SeekOrigin.Begin)
        ' Wert schreiben
        r.Write(sData)
        GoTo good

errors:
        ' Bei Fehlern wird 1 zurückgegeben
        fs.Close()
        Return (1)
good:
        ' Bei Erfolg wird 0 zurückgegeben
        fs.Close()
        Return (0)
    End Function

End Class
