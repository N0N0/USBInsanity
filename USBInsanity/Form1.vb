' USBInsanity v0.85b
' Copyright©2007-2011 by A. Terwedow
'
' Das langfristige Ziel dieser Anwendung ist die Ablösung der nutzlosen
' HD-Copier USBExtreme & USBInsane für die PS2-HDD-Loader
' USBAdvance/-Extreme
'
'Entwicklunsstand (85% Einsatzbereit)
' Primärfunktionen:
'   CRC Berechnung                                    x
'   Suchen und einlesen der ul.cfg                    x
'   Anlegen neuer Datensätze bzw. neuer ul.cfg        x
'   Automatisches einlesen der System.cnf von LW      x
'   Kopieren/Splitten von .mdf/.iso auf USB-HD        -
'   Löschen von Datensätzen aus ul.cfg                x
' Sekundärfunktionen:
'   Sortieren der ul.cfg                              x
'   Fragmentierungsüberprüfung der Spieldaten auf USB -
'
' Weiterführende Hintergrundinfos zu Funktionsweise der o.g. Bugsammlungen
' sind in den Projekt-Memos im Quellcode Verzeichnis enthalten.
'
'Rechtliches:
' ///////////////////////////////////////////////////////////////////////////
' ///////////////////////////////////////////////////////////////////////////
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

Public Class Form1


    Dim ulcfg As String = "ul.cfg"
    Dim is_ulcfg As String

    Dim usbadv As New clUsbadvance()
    Dim str As New clStrings()
    Dim file As New clFileAccess()

    Dim installedGames As New List(Of clGame)

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim Drives() As String
        Dim i As Integer

        ' Ermittel alle Laufwerke
        Drives = Directory.GetLogicalDrives()
        For i = 0 To Drives.Length - 1

            ' Hol Laufwerksinformationen ein
            Dim drive_info As New DriveInfo(Drives(i))

            ' Trage jew. Massenspeicher und CDVD-ROMs in die ComboBoxen
            If drive_info.DriveType() = DriveType.Fixed Then
                ComboHDD.Items.Add(Drives(i))
            ElseIf drive_info.DriveType() = DriveType.CDRom Then
                ComboOptical.Items.Add(Drives(i))
            End If

        Next i

        ' Mache den jew. 1. Eintrag der ComboBoxen sichtbar
        ComboHDD.SelectedIndex = 0
        ComboOptical.SelectedIndex = 0

    End Sub

    Private Sub ComboHDD_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboHDD.SelectedIndexChanged

        ListBoxGames.Items.Clear()  ' Lisbox löschen
        ButtonSort.Enabled = False
        ButtonDelete.Enabled = False

        installedGames = usbadv.ReadUlcfg(ComboHDD.SelectedItem) ' Liste der installierten Spiele abrufen

        If installedGames.Count > 0 Then ' Wenn Spiele installiert wurden, Listbox aufbauen
            For i As Integer = 0 To installedGames.Count - 1
                Dim test As Integer = 32 - installedGames(i).getTitle().Length
                ListBoxGames.Items.Add(installedGames(i).getTitle() & New String(" ", 34 - installedGames(i).getTitle().Length) & _
                                   installedGames(i).getElf() & New String(" ", 14 - installedGames(i).getElf().Length) & _
                                   installedGames(i).getParts() & New String(" ", 4) & _
                                   installedGames(i).getDiscTypeS())
            Next i
        End If

        Label_Installed.Text = "Installed Games: " & installedGames.Count
        If installedGames.Count > 0 Then ButtonSort.Enabled = True

    End Sub

    Private Sub ListBox1_SelectedItemChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBoxGames.SelectedIndexChanged

        Dim i As Integer = ListBoxGames.SelectedIndex

        If i < 0 Then Exit Sub

        ButtonDelete.Enabled = True
        TextBoxTitle.Text = installedGames(i).getTitle
        TextBoxFilename.Text = "ul." & TextBoxHash.Text & "." & _
                         installedGames(i).getElf & ".XX" ' Dateiname des ISOs

    End Sub

    Private Sub TextBoxTitle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxTitle.TextChanged
        TextBoxFilename.Clear()

        ' Führe funcCRCBerechnung für den Inhalt der TextBox1 aus
        TextBoxHash.Text = usbadv.CreateHash(TextBoxTitle.Text)
    End Sub
    Private Sub TextBoxTitle_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBoxTitle.KeyDown
        ' Stellt sicher, dass der Delete-Button deaktiviert wird, sobald etwas ins Title-Feld eingetragen wird
        ButtonDelete.Enabled = False
        ListBoxGames.SelectedItem = -1
    End Sub

    Private Sub ButtonAddGame_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAddGame.Click

        On Error Resume Next
        ' Hol den Dateinamen aus der System.CNF von CDVD
        Dim cdvd As New clFileAccess()
        Dim elfFile As String
        elfFile = file.ReadSystemCNF(ComboOptical.SelectedItem)

        ' If no guilty System.CNF had been found leave this sub
        If elfFile = "" Or elfFile Is Nothing Then
            Exit Sub
        End If

        Dim nGame As clGame = usbadv.NewGame(TextBoxTitle.Text, _
                                             elfFile, _
                                             ComboOptical.Text, _
                                             ComboMedia.Text)

        If nGame Is Nothing Then
            Exit Sub ' Wenn kein neues Spiel erstellt wurde, beenden
        End If

        ' Schreibe den Eintrag in die ul.cfg
        usbadv.WriteUlcfg(nGame, ComboHDD.SelectedItem)

        ' Ausgabe des benötigten Dateinamens
        TextBoxFilename.Text = "ul." & TextBoxHash.Text & "." & nGame.getElf() & ".XX"

        Call ComboHDD_SelectedIndexChanged(Me, New System.EventArgs) '' Aktualisiere Spieleliste

        'TODO Notlösung bis zur Implementierung der Split/Copy-Funktion
        'MsgBox("Please name your ISO-Parts like this:" & vbCrLf & vbCrLf & vbTab & DateinamenMaske & ".0x" & vbTab)
        ' Called die Funktion ImageDump --> Alphastatus --> Übergabewerte entschlacken bzw. aufräumen!
        ' Call ImageDumpExtern(mediasize, Teile, DateinamenMaske)

        Exit Sub

    End Sub

    Private Sub ButtonSort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSort.Click
        On Error Resume Next
        Dim i, j As Integer

        ' Sortieren der Liste absteigend nach Titel
        Dim sortedList As New List(Of clGame)

        ' Erstelle 1-Dimensionales Array mit den Titeln der installedGamesList
        Dim tempArray(installedGames.Count - 1) As String

        ' Das Array erhält das Format:   title, trennzeichen(*), index in der liste
        For i = 0 To installedGames.Count - 1
            tempArray(i) = installedGames(i).getTitle() & "*" & i
        Next

        ' Sortiert tempArray nach Bezeichnung
        Array.Sort(tempArray)

        ' Erstellt aus dem sortierten Liste ein sortiertes Array
        For i = 0 To installedGames.Count - 1
            Dim indexS As String
            Dim cutIndex, length, index As Integer

            ' ermittelt die Position des Trennzeichens
            cutIndex = tempArray(i).IndexOf("*") + 1
            length = tempArray(i).Length
            ' ermittelt den angehängten index in der original liste
            indexS = tempArray(i).Substring(cutIndex, length - cutIndex)
            index = Integer.Parse(indexS)
            ' fügt das element an seiner neuen position ein
            sortedList.Add(installedGames(index))
        Next

        ' Schreibt die neue ul.cfg
        usbadv.WriteNewUlcfg(sortedList, ComboHDD.SelectedItem)

        ' Aktualisiere Spieleliste
        Call ComboHDD_SelectedIndexChanged(Me, New System.EventArgs)


    End Sub

    Private Sub ButtonDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDelete.Click
        Dim i, parts As Byte
        Dim fileseek As Boolean
        Dim path, elf As String

        parts = installedGames(ListBoxGames.SelectedIndex).getParts()
        elf = installedGames(ListBoxGames.SelectedIndex).getElf()

        For i = 0 To parts - 1 ' (two parts = 0 & 1)

            path = ComboHDD.SelectedItem & "ul." & TextBoxHash.Text & "." & elf & ".0" & i

            fileseek = System.IO.File.Exists(path) ' Check if files beloning to the title do exist

            If fileseek = False Then ' if one file do not exists, ask the user what to do
                i = MessageBox.Show("One or more files belonging to this game have not been found!" & _
                                               vbCrLf & vbCrLf & "File in question: " & path & _
                                               vbCrLf & vbCrLf & "Proceed with deleting just the games entry from ul.cfg?", _
                                               "File not found", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
                If i = 2 Then
                    Exit Sub ' If Cancel has been clicked, cancel this function
                Else
                    i = parts ' If OK has been clicked go on with deleting only Entry from ul.cfg without files
                End If

            End If
        Next

        ' if everyfile has been found just delete them
        For i = 0 To parts - 1 ' (two parts = 0 & 1)
            path = ComboHDD.SelectedItem & "ul." & TextBoxHash.Text & "." & elf & ".0" & i
            System.IO.File.Delete(path)
        Next

        ' Lösche den ausgewählten Titel aus der Liste installedGames
        TextBoxTitle.Clear()
        installedGames.RemoveAt(ListBoxGames.SelectedIndex)

        ' Erstelle ul.cfg neu
        usbadv.WriteNewUlcfg(installedGames, ComboHDD.SelectedItem)

        ' Aktualisiere Spieleliste
        Call ComboHDD_SelectedIndexChanged(Me, New System.EventArgs)
    End Sub

    Private Sub ImageDumpExtern(ByVal MediaSize As Long, ByVal Teile As Byte, ByVal DateinamenMaske As String)

        ' Alpha der Dump Mechanik über externe Tools
        On Error Resume Next

        Dim i As Integer

        ' Image von CDVD dumpen
        'Speicherplatz auf Ziellaufwerk überprüfen
        If FileIO.FileSystem.GetDriveInfo(ComboHDD.Text).AvailableFreeSpace < MediaSize * 2 Then
            MsgBox("Nicht genügend freier Speicher auf Laufwerk " & ComboHDD.Text)
            Exit Sub
        End If

        ' ComboOpt.SelectIndex bestimmt das zu benutzende LW, funzt aber nur,
        ' solange die Buchstaben der LW's in richtiger Reihenfolge liegen

        Shell("C:\miso\miso.exe " & ComboHDD.Text & DateinamenMaske & " -i " & ComboOptical.SelectedIndex + 1, AppWinStyle.NormalFocus, True)


        ' Image splitten, falls nötig

        If Teile > 1 Then
            Shell("C:\miso\splits.exe " & ComboHDD.Text & DateinamenMaske & " 1048576", AppWinStyle.NormalFocus, True)
            FileIO.FileSystem.DeleteFile(ComboHDD.Text & DateinamenMaske)
        End If


        If Teile > 1 Then
            ' Umbenennen von gesplitteten ISOs
            For i = 1 To Teile
                Rename(ComboHDD.Text & DateinamenMaske & ".00" & i, ComboHDD.Text & DateinamenMaske & ".0" & i - 1)
            Next
        Else
            ' Umbenennen von kleinen ISOs
            Rename(ComboHDD.Text & DateinamenMaske, ComboHDD.Text & DateinamenMaske & ".00")
        End If

    End Sub
    Private Sub ImageDumpIntern()
        On Error Resume Next
    End Sub

End Class
