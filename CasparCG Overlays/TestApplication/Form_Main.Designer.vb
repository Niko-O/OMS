<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_Main
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
        Me.Button_Connect = New System.Windows.Forms.Button()
        Me.Button_DoStuff1 = New System.Windows.Forms.Button()
        Me.Button_DoStuff2 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Button_Connect
        '
        Me.Button_Connect.Location = New System.Drawing.Point(12, 12)
        Me.Button_Connect.Name = "Button_Connect"
        Me.Button_Connect.Size = New System.Drawing.Size(100, 36)
        Me.Button_Connect.TabIndex = 0
        Me.Button_Connect.Text = "Connect"
        Me.Button_Connect.UseVisualStyleBackColor = True
        '
        'Button_DoStuff1
        '
        Me.Button_DoStuff1.Location = New System.Drawing.Point(12, 54)
        Me.Button_DoStuff1.Name = "Button_DoStuff1"
        Me.Button_DoStuff1.Size = New System.Drawing.Size(100, 36)
        Me.Button_DoStuff1.TabIndex = 0
        Me.Button_DoStuff1.Text = "DoStuff1"
        Me.Button_DoStuff1.UseVisualStyleBackColor = True
        '
        'Button_DoStuff2
        '
        Me.Button_DoStuff2.Location = New System.Drawing.Point(12, 96)
        Me.Button_DoStuff2.Name = "Button_DoStuff2"
        Me.Button_DoStuff2.Size = New System.Drawing.Size(100, 36)
        Me.Button_DoStuff2.TabIndex = 0
        Me.Button_DoStuff2.Text = "DoStuff2"
        Me.Button_DoStuff2.UseVisualStyleBackColor = True
        '
        'Form_Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.Button_DoStuff2)
        Me.Controls.Add(Me.Button_DoStuff1)
        Me.Controls.Add(Me.Button_Connect)
        Me.Name = "Form_Main"
        Me.Text = "Form_Main"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button_Connect As System.Windows.Forms.Button
    Friend WithEvents Button_DoStuff1 As System.Windows.Forms.Button
    Friend WithEvents Button_DoStuff2 As System.Windows.Forms.Button

End Class
