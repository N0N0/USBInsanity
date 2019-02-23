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
Imports DiscUtils
Imports DiscUtils.Iso9660
Imports DiscUtils.raw

Public Class clFileAccess

    Public Function ReadSystemCNF(ByRef srcFile As System.IO.Stream) As String
        Dim reader = Nothing
        Try
            reader = New CDReader(srcFile, True)
        Catch
            MsgBox("Your Image seems to be a RAW/BIN Image file, please try a tool like bin2iso to convert it into a standard ISO9660 image.", MsgBoxStyle.Critical, "Error")
            Return ""
        End Try

        Dim dateiname As String = ""

        If (reader.FileExists("\BOOT.ELF")) Then
            srcFile.Close()
            Return "BOOT.ELF"
        ElseIf (reader.FileExists("\SYSTEM.CNF")) Then ' read the file
            Dim exeName As String = getExecutableName(reader.OpenFile("\SYSTEM.CNF", FileMode.Open))
            srcFile.Close()
            Return exeName
        Else
            srcFile.Close()
            MsgBox("Could not determine game executable.", MsgBoxStyle.Critical, "Error")
            Return ""
        End If

    End Function

    ' Sucht und liest Informationen aus der System.cnf der gewählten DVD
    Public Function ReadSystemCNF(ByRef srcDrive As String) As String

        On Error Resume Next
        ' suche BOOT.ELF
        If (File.Exists(srcDrive & "BOOT.ELF")) Then
            Return "BOOT.ELF"
        ElseIf (File.Exists(srcDrive & "SYSTEM.CNF")) Then ' kein BOOT.ELF ? > suche System.CNF
            Dim fs As New FileStream(srcDrive & "SYSTEM.CNF", FileMode.Open, FileAccess.Read)
            Return getExecutableName(fs)
        Else ' weder BOOT.ELF noch System.CNF ? > fehler
            MsgBox("Please insert Playstation 2 CD/DVD in Drive " & srcDrive & " .", MsgBoxStyle.Critical, "Error")
            Return ""
        End If

    End Function

    Private Function getExecutableName(ByRef fs As Stream) As String
        Dim CNF_Laenge As Integer = fs.Length
        Dim CNF_Inhalt As String
        Dim str As New clStrings()
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
        While Str.funcLeft(CNF_Inhalt, 1) <> "\"
            CNF_Inhalt = Str.funcRight(CNF_Inhalt, Len(CNF_Inhalt) - 1)
        End While
        CNF_Inhalt = Str.funcRight(CNF_Inhalt, Len(CNF_Inhalt) - 1)
        ' und rechts
        CNF_Inhalt = Str.funcLeft(CNF_Inhalt, 12)
        ' prüfe ob der Dateiname kürzer als 8.3 ist
        While Str.funcRight(CNF_Inhalt, 1) <> ";"
            CNF_Inhalt = Str.funcLeft(CNF_Inhalt, Len(CNF_Inhalt) - 1)
        End While
        ' schneide das ";" ab
        CNF_Inhalt = Str.funcLeft(CNF_Inhalt, Len(CNF_Inhalt) - 1)

        Return CNF_Inhalt
    End Function

    ' Schreibt einen Binärstring
    Public Function BinaryWrite(ByVal sFile As String, ByVal sOffset As String, ByVal sData As ULong) As Byte
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
