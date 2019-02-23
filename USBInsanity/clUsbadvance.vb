' clUsbadvance.vb
' Copyright©2007-2011 by A. Terwedow
'
' Diese Klasse ist Teil des Programmes USBInsanity.
' Sie bietet Funktionen für USBAdvance.
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

Option Strict Off
Option Explicit On
Imports System.IO

' Diese Klasse besitzt keinen Konstruktor, die einzige Schnittstelle zum Datenaustausch bietet
' die Methode funcCRCBerechnung
Public Class clUsbadvance

    Dim ulcfg As String = "ul.cfg"
    Dim is_ulcfg As Double

    Dim str As New clStrings()
    Dim file As New clFileAccess()

    ' Memorystream und BinaryReader für den Zugriff auf die Hashtable
    Dim mem As New IO.MemoryStream(My.Resources.USBInsanity)
    Dim xr As New BinaryReader(mem)

    Public Function NewGame(ByRef title As String, ByRef Dateiname As String, ByRef srcDrv As String, ByRef mediatype As String) As clGame
        ' Hol die Anzahl der Teile ein
        Dim mediasize As Long
        Dim drive_info As New DriveInfo(srcDrv)
        mediasize = drive_info.TotalSize
        Return NewGame(title, Dateiname, mediasize, mediatype)
    End Function


    Public Function NewGame(ByRef title As String, ByRef Dateiname As String, ByRef mediaSize As Long, ByRef mediatype As String) As clGame
        On Error Resume Next
        Dim DateinameBack As String
        Dim Media, Teile As Byte

        ' Prüfe den übergebenen Titel
        If title = "" Then
            MsgBox("You have to define a title for the game!", MsgBoxStyle.Critical, "Error")
            Return Nothing
        End If

        ' Prüfe den übergebenen Dateinamen
        If Dateiname = "" Then
            Return Nothing
        End If

        ' Hol den MedienTyp ein und bring ihn in Byteform :>
        If mediatype = "CD" Then
            Media = clConstants.DISC_TYPE_CD
        ElseIf mediatype = "DVD" Then
            Media = clConstants.DISC_TYPE_DVD
        Else
            MsgBox("Unsupported MediaType(?)")
            Return Nothing
        End If

        ' Hol die Anzahl der Teile ein
        Dim media_antiround As Long

        ' Wenn MedienTyp "CD" prüfe, ob es wirklich eine CD ist
        If Media = clConstants.DISC_TYPE_CD And mediasize > 734003200 Then
            If MsgBox("The Gamedata seems to be too much for a CD (" & Convert.ToInt32(mediaSize / 1048576) & " MegaByte)." & vbCrLf & "Should the Mediatype be set to DVD instead?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes Then
                Media = clConstants.DISC_TYPE_DVD
            End If
        End If

        ' Prüfe obs Medium zu klein für eine DVD ist und schlage den Wechsel des Medientyps vor
        If Media = clConstants.DISC_TYPE_DVD And mediasize <= 734003200 Then
            If MsgBox("The Gamedata seems to be too few Data for a DVD (" & Convert.ToInt32(mediaSize / 1048576) & " MegaByte)." & vbCrLf & "Should the Mediatype be set to CD instead?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes Then
                Media = clConstants.DISC_TYPE_CD
            End If
        End If

        ' Prüfe auf DVD9 -> veraltet, OpenPSLoader und PS2ESDL unterstützen DVD9
        '    If mediasize > 4700000000 Then
        ' MsgBox("DVD9 is neither supported by USBExtreme, nor is it by USBInsanity!", MsgBoxStyle.Critical, "Sorry")
        ' Return Nothing
        ' End If

        ' Errechne die Teilstücke (je 1024 MegaByte)
        media_antiround = mediasize / 1048576
        While media_antiround > 1024
            Teile = Teile + 1
            media_antiround = media_antiround - 1024
        End While
        ' Wenn ein Rest übrig ist, muß dieser in ein Extra Teilstück
        If media_antiround <> 0 Then
            Teile = Teile + 1
        End If

        Return New clGame(title, Dateiname, Teile, Media)

    End Function

    Private Function ReadHashblock(ByVal sOffset As String) As String
        ' Liest einen 4Byte Integer aus der Hashtable "USBInsanity.bin"
        mem.Seek(sOffset, IO.SeekOrigin.Begin)
        Return xr.ReadUInt32
        mem.Close()
        xr.Close()
    End Function
    Public Function CreateHash(ByVal sTitle As String) As String
        On Error Resume Next

        Dim Laenge As SByte = sTitle.Length  ' sByte reicht, da sTitle.length maximal 0b31 sein kann
        Dim EDI As Integer = &H349398
        Dim DL As Integer

        Dim EDX, EBX As Long
        Dim EAX, nEAX, nEBX, nEDX As String

        ' Start der Berechnung
        EAX = ""


        While Laenge >= 0  ' Je Zeichen des Titels 1 Durchgang + 1 Extradurchgang

            ' mov dl, byte ptr [ecx]
            If sTitle <> "" Then
                DL = Asc(str.funcLeft(sTitle, 1))
            Else
                DL = 0 ' Für den letzten Durchgang wenn kein Zeichen mehr vorhanden ist
            End If

            ' mov ebx, eax
            EBX = EAX

            ' and edx, 0FF
            EDX = DL

            ' shr ebx, 18
            nEBX = Hex(EBX)

            ' Notlösung für seltene HEX-Werte die mit 0 beginnen,
            ' was von VB einfach abgeschnittent wird)
            While (Len(nEBX) < 8)
                nEBX = "0" & nEBX
            End While

            nEBX = str.funcLeft(nEBX, 2)

            EBX = CInt("&H" & nEBX)

            ' xor edx, ebx
            EDX = EDX Xor EBX

            ' inc ecx
            sTitle = str.funcRight(sTitle, (sTitle.Length - 1))

            ' shl eax, 8
            If EAX <> 0 Then
                nEAX = Hex(EAX)
                nEAX = str.funcRight(nEAX, 6)
                nEAX = nEAX & "00"
                EAX = CLng("&H" & nEAX)
            End If

            ' mov edx, dword ptr [edi+edx*4]
            ' Errechnet aus dem ASCII-Wert in Hex(DL * 4) und einer Variablen in EDI,
            ' das Offset eines 4Bit Integers in einem Hashblock, welcher sich nun
            ' glücklicherweise in My.Resources.USBInsanity.bin befindet.
            '
            ' In Klartext heißt das, daß hier noch mit EDX * 4 das Offset
            ' bestimmt und der Integer damit aus USBInsanity.bin gelesen wird.
            '-----------------------------------------------------------------------
            EDX = ReadHashblock("&H" & Hex(EDX * 4))

            ' xor eax, edx
            EAX = EAX Xor EDX

            'dec esi
            Laenge -= 1

        End While

        EAX = Hex(EAX)

        While Len(EAX) < 8
            EAX = "0" & EAX
        End While

        Return (EAX)

    End Function

    Public Function ReadUlcfg(ByVal srcDrive As String) As List(Of clGame)

        On Error Resume Next
        Dim i As Integer
        Dim title, dateiname, puffer As String
        Dim teile, mediaNum As Byte

        Dim tempList As New List(Of clGame)

        ' Lösche is_ulcfg vor dem Lesen (FileLen gibt bei Fehler einen
        ' Nullstring zurück der einen "alten" Wert von is_ulcfg nicht ersetzt
        is_ulcfg = 0

        is_ulcfg = FileLen(srcDrive & ulcfg)

        ' Liest die Datei in Blöcken zu 64Bit ein und filtert innerhalb
        ' der Schleife alle benötigten Daten heraus
        For i = 0 To is_ulcfg / 64 - 1

            Dim fs As New FileStream(srcDrive & ulcfg, FileMode.Open, FileAccess.Read)

            ' Text.Encoding.ASCII   = 128Bit Ur-ASCII (verursacht Probleme mit Umlauten und Sonderzeichen
            ' Text.Encoding.Default = 255Bit Standard-ASCII
            Dim r As BinaryReader = New BinaryReader(fs, System.Text.Encoding.Default)

            title = "" ' Lösche ggf. vorhandenen Titel

            ' Bestimme den Spieltitel
            r.BaseStream.Seek(i * 64, SeekOrigin.Begin)
            title = (r.ReadChars(32))         ' Maximallänge des Namens sind 32 Chars
            ' schneide die 0'en ab. Wenn ich direkt auf "0"
            ' verglichen habe, lief die Funktion spätestens nach
            ' dem 2. Datenblock aus der Sub herraus
            puffer = str.funcRight(title, 1)
            While Asc(puffer) < 32 Or Asc(puffer) > 255
                title = str.funcLeft(title, Len(title) - 1)
                puffer = str.funcRight(title, 1)
            End While
            ' Lösche puffer für den nächsten Sub-Abschnitt
            puffer = ""

            ' Bestimme den Dateinamen der Startdatei
            r.BaseStream.Seek(i * 64 + 32, SeekOrigin.Begin)
            dateiname = (r.ReadChars(14))
            ' Schneide die 0'en ab / wenn ich direkt auf "0"
            ' verglichen habe, lief die Funktion spätestens nach
            ' dem 2. Datenblock aus der Sub heraus.
            puffer = str.funcRight(dateiname, 1)
            While Asc(puffer) < 32 Or Asc(puffer) > 255
                dateiname = str.funcLeft(dateiname, Len(dateiname) - 1)
                puffer = str.funcRight(dateiname, 1)
            End While
            ' lösche "ul." vor dem dateinamen
            dateiname = str.funcRight(dateiname, Len(dateiname) - 3)
            ' Lösche den Puffer für den nächsten Sub-Abschnitt
            puffer = ""

            ' Bestimme die Anzahl der Teilstücke
            r.BaseStream.Seek(i * 64 + 47, SeekOrigin.Begin)
            teile = (r.ReadByte)

            ' Bestimme den Medientyp
            r.BaseStream.Seek(i * 64 + 48, SeekOrigin.Begin)
            mediaNum = (r.ReadByte)

            'Stream Schließen
            fs.Close()

            ' Füge Spiel der Spieleliste hinzu
            tempList.Add(New clGame(title, dateiname, teile, mediaNum))

        Next i

        Return tempList

    End Function

    ' Rückgabe 0 bei Erfolg, 1 bei
    Public Function ClearUlcfg(ByVal destDrv As String) As Byte
        On Error GoTo fehler

        My.Computer.FileSystem.DeleteFile(destDrv & ulcfg)

        Return 0
fehler:
        Return 1

    End Function
    Public Function WriteUlcfg(ByVal nGame As clGame, ByVal destDrv As String) As Byte

        ' Schreibe alle Daten als neuen Block in die ul.cfg
        On Error GoTo fehler

        Dim title As String = nGame.getTitle()
        Dim elf As String = nGame.getElf()
        Dim Teile As Byte = nGame.getParts()
        Dim Media As Byte = nGame.getDiscType()

        ' neue attribute
        Dim elf2writeS As String = ""
        Dim title2writeS As String = ""
        Dim data2writeS As String = ""
        Dim data2writeStmp As String = ""
        Dim data2writeUL As ULong
        Dim path2write As String
        Dim fileLen As Double
        Dim j As Integer

TitelSchreiben:
        ' FileLen gibt bei nichtgefundener Datei -1 zurück
        is_ulcfg = getFileLen(destDrv & ulcfg)

neuerTitle:
        title2writeS = ""
        path2write = destDrv & ulcfg
        ' Title-Block in HEX-ASCII-String umwandeln
        For j = 0 To Len(title) - 1
            title2writeS = Hex(Asc(str.funcLeft(title, 1))) & title2writeS
            title = str.funcRight(title, Len(title) - 1)
        Next j
        ' Title-Block auf 32 Zeichen erweitern
        While title2writeS.Length < 64
            title2writeS = "0" & title2writeS
        End While

neuerDateiname:
        elf2writeS = "2E6C75" ' ==> ".lu" ==> wird später zu "ul."
        For j = 0 To Len(elf) - 1
            elf2writeS = Hex(Asc(str.funcLeft(elf, 1))) & elf2writeS
            elf = str.funcRight(elf, Len(elf) - 1)
        Next j

        While Len(elf2writeS) < 30  ' Block auf 15 Zeichen erweitern
            elf2writeS = "0" & elf2writeS
        End While

neueTeile:
        ' Mediatype & "0" & Nr.Image-Parts & elf-name
        elf2writeS = Hex(nGame.getDiscType) & "0" & nGame.getParts & elf2writeS

        ' ELF-Block auf 32 Zeichen erweitern
        While elf2writeS.Length < 64
            elf2writeS = "0" & elf2writeS
        End While

        data2writeS = elf2writeS & title2writeS

datenSchreiben:
        ' data2write in Blöcken a 8 Byte schreiben
        For j = 0 To 7 ' 8 Durchgänge
            data2writeStmp = data2writeS.Substring(Len(data2writeS) - 16, 16)
            data2writeS = str.funcLeft(data2writeS, Len(data2writeS) - 16)
            data2writeStmp = "&H" & data2writeStmp
            data2writeUL = data2writeStmp
            ' ermittle länge von ul.cfg
            fileLen = getFileLen(destDrv & ulcfg)
            If fileLen < 0 Then fileLen = 0 ' wenn die ul.cfg nicht vorhanden ist, setze länge auf 0
            file.BinaryWrite(path2write, fileLen, data2writeUL)
        Next j

        Return 0

fehler:
        Return 1

    End Function
    Public Function WriteNewUlcfg(ByVal listofgames As List(Of clGame), ByVal destDrive As String) As Byte
        On Error GoTo fehler

        ' Diese Methode erstellt aus einer
        Dim i As Integer

        ' Schreibe komplette Liste neu
        ClearUlcfg(destDrive)

        For i = 0 To listofgames.Count - 1
            WriteUlcfg(listofgames(i), destDrive)
        Next i

        Return 0

fehler:
        Return 1

    End Function

    Private Function getFileLen(ByVal path As String) As Double
        ' Gibt die Länge einer Datei als double zurück, ist als eigene Function realisiert um ihr
        ' eine eigene Fehlerbehandlung geben zu können
        Try
            Return FileLen(path)
        Catch
            Return 0
        End Try
    End Function

End Class
