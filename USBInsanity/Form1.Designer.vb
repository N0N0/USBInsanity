<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.TextBoxTitle = New System.Windows.Forms.TextBox()
        Me.TextBoxHash = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboHDD = New System.Windows.Forms.ComboBox()
        Me.ComboOptical = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ComboMedia = New System.Windows.Forms.ComboBox()
        Me.ListBoxGames = New System.Windows.Forms.ListBox()
        Me.Label_Installed = New System.Windows.Forms.Label()
        Me.ButtonAddDisc = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxFilename = New System.Windows.Forms.TextBox()
        Me.ButtonSort = New System.Windows.Forms.Button()
        Me.ButtonDelete = New System.Windows.Forms.Button()
        Me.ButtonAddImage = New System.Windows.Forms.Button()
        Me.ButtonRename = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TextBoxTitle
        '
        Me.TextBoxTitle.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxTitle.Location = New System.Drawing.Point(79, 65)
        Me.TextBoxTitle.MaxLength = 32
        Me.TextBoxTitle.Name = "TextBoxTitle"
        Me.TextBoxTitle.Size = New System.Drawing.Size(275, 20)
        Me.TextBoxTitle.TabIndex = 0
        Me.TextBoxTitle.TabStop = False
        '
        'TextBoxHash
        '
        Me.TextBoxHash.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.TextBoxHash.Location = New System.Drawing.Point(376, 65)
        Me.TextBoxHash.MaxLength = 8
        Me.TextBoxHash.Name = "TextBoxHash"
        Me.TextBoxHash.ReadOnly = True
        Me.TextBoxHash.Size = New System.Drawing.Size(70, 20)
        Me.TextBoxHash.TabIndex = 1
        Me.TextBoxHash.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Game Title:"
        '
        'ComboHDD
        '
        Me.ComboHDD.FormattingEnabled = True
        Me.ComboHDD.Location = New System.Drawing.Point(404, 25)
        Me.ComboHDD.MaxLength = 3
        Me.ComboHDD.Name = "ComboHDD"
        Me.ComboHDD.Size = New System.Drawing.Size(42, 21)
        Me.ComboHDD.TabIndex = 5
        '
        'ComboOptical
        '
        Me.ComboOptical.FormattingEnabled = True
        Me.ComboOptical.Location = New System.Drawing.Point(12, 25)
        Me.ComboOptical.MaxLength = 3
        Me.ComboOptical.Name = "ComboOptical"
        Me.ComboOptical.Size = New System.Drawing.Size(42, 21)
        Me.ComboOptical.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(401, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(31, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "HDD"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "CDVD"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(206, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Mediatype"
        '
        'ComboMedia
        '
        Me.ComboMedia.FormattingEnabled = True
        Me.ComboMedia.Items.AddRange(New Object() {"CD", "DVD"})
        Me.ComboMedia.Location = New System.Drawing.Point(209, 25)
        Me.ComboMedia.MaxDropDownItems = 2
        Me.ComboMedia.MaxLength = 3
        Me.ComboMedia.Name = "ComboMedia"
        Me.ComboMedia.Size = New System.Drawing.Size(50, 21)
        Me.ComboMedia.Sorted = True
        Me.ComboMedia.TabIndex = 11
        Me.ComboMedia.Text = "DVD"
        '
        'ListBoxGames
        '
        Me.ListBoxGames.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBoxGames.FormattingEnabled = True
        Me.ListBoxGames.ItemHeight = 14
        Me.ListBoxGames.Location = New System.Drawing.Point(15, 162)
        Me.ListBoxGames.Name = "ListBoxGames"
        Me.ListBoxGames.Size = New System.Drawing.Size(431, 158)
        Me.ListBoxGames.TabIndex = 12
        Me.ListBoxGames.UseTabStops = False
        '
        'Label_Installed
        '
        Me.Label_Installed.AutoSize = True
        Me.Label_Installed.Location = New System.Drawing.Point(12, 139)
        Me.Label_Installed.Name = "Label_Installed"
        Me.Label_Installed.Size = New System.Drawing.Size(85, 13)
        Me.Label_Installed.TabIndex = 13
        Me.Label_Installed.Text = "Installed Games:"
        '
        'ButtonAddDisc
        '
        Me.ButtonAddDisc.Enabled = False
        Me.ButtonAddDisc.Location = New System.Drawing.Point(162, 113)
        Me.ButtonAddDisc.Name = "ButtonAddDisc"
        Me.ButtonAddDisc.Size = New System.Drawing.Size(88, 22)
        Me.ButtonAddDisc.TabIndex = 14
        Me.ButtonAddDisc.Text = "Add Disc"
        Me.ButtonAddDisc.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 98)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Filename:"
        '
        'TextBoxFilename
        '
        Me.TextBoxFilename.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxFilename.Location = New System.Drawing.Point(79, 91)
        Me.TextBoxFilename.MaxLength = 32
        Me.TextBoxFilename.Name = "TextBoxFilename"
        Me.TextBoxFilename.ReadOnly = True
        Me.TextBoxFilename.Size = New System.Drawing.Size(367, 20)
        Me.TextBoxFilename.TabIndex = 16
        Me.TextBoxFilename.TabStop = False
        '
        'ButtonSort
        '
        Me.ButtonSort.Enabled = False
        Me.ButtonSort.Location = New System.Drawing.Point(256, 136)
        Me.ButtonSort.Name = "ButtonSort"
        Me.ButtonSort.Size = New System.Drawing.Size(92, 22)
        Me.ButtonSort.TabIndex = 17
        Me.ButtonSort.Text = "Sort List"
        Me.ButtonSort.UseVisualStyleBackColor = True
        '
        'ButtonDelete
        '
        Me.ButtonDelete.Enabled = False
        Me.ButtonDelete.Location = New System.Drawing.Point(354, 136)
        Me.ButtonDelete.Name = "ButtonDelete"
        Me.ButtonDelete.Size = New System.Drawing.Size(92, 22)
        Me.ButtonDelete.TabIndex = 18
        Me.ButtonDelete.Text = "Delete Game"
        Me.ButtonDelete.UseVisualStyleBackColor = True
        '
        'ButtonAddImage
        '
        Me.ButtonAddImage.Enabled = False
        Me.ButtonAddImage.Location = New System.Drawing.Point(162, 136)
        Me.ButtonAddImage.Name = "ButtonAddImage"
        Me.ButtonAddImage.Size = New System.Drawing.Size(88, 22)
        Me.ButtonAddImage.TabIndex = 19
        Me.ButtonAddImage.Text = "Add Image"
        Me.ButtonAddImage.UseVisualStyleBackColor = True
        '
        'ButtonRename
        '
        Me.ButtonRename.Enabled = False
        Me.ButtonRename.Location = New System.Drawing.Point(354, 113)
        Me.ButtonRename.Name = "ButtonRename"
        Me.ButtonRename.Size = New System.Drawing.Size(92, 22)
        Me.ButtonRename.TabIndex = 20
        Me.ButtonRename.Text = "Rename Game"
        Me.ButtonRename.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(454, 332)
        Me.Controls.Add(Me.ButtonRename)
        Me.Controls.Add(Me.ButtonAddImage)
        Me.Controls.Add(Me.ButtonDelete)
        Me.Controls.Add(Me.ButtonSort)
        Me.Controls.Add(Me.TextBoxFilename)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.ButtonAddDisc)
        Me.Controls.Add(Me.Label_Installed)
        Me.Controls.Add(Me.ListBoxGames)
        Me.Controls.Add(Me.ComboMedia)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ComboOptical)
        Me.Controls.Add(Me.ComboHDD)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxHash)
        Me.Controls.Add(Me.TextBoxTitle)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(470, 370)
        Me.MinimumSize = New System.Drawing.Size(470, 370)
        Me.Name = "Form1"
        Me.Text = "USBInsanity 0.86.1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxTitle As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxHash As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboHDD As System.Windows.Forms.ComboBox
    Friend WithEvents ComboOptical As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ListBoxGames As System.Windows.Forms.ListBox
    Friend WithEvents Label_Installed As System.Windows.Forms.Label
    Friend WithEvents ButtonAddDisc As System.Windows.Forms.Button
    Public WithEvents ComboMedia As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxFilename As System.Windows.Forms.TextBox
    Friend WithEvents ButtonSort As System.Windows.Forms.Button
    Friend WithEvents ButtonDelete As System.Windows.Forms.Button
    Friend WithEvents ButtonAddImage As System.Windows.Forms.Button
    Friend WithEvents ButtonRename As System.Windows.Forms.Button

End Class
