<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class OSBVolume
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.OpacityTimer = New System.Windows.Forms.Timer(Me.components)
        Me.VolumeLabel = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CurrentVolume = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'OpacityTimer
        '
        Me.OpacityTimer.Interval = 2000
        '
        'VolumeLabel
        '
        Me.VolumeLabel.AutoSize = True
        Me.VolumeLabel.ForeColor = System.Drawing.Color.White
        Me.VolumeLabel.Location = New System.Drawing.Point(30, 147)
        Me.VolumeLabel.Name = "VolumeLabel"
        Me.VolumeLabel.Size = New System.Drawing.Size(13, 13)
        Me.VolumeLabel.TabIndex = 0
        Me.VolumeLabel.Text = "0"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Gray
        Me.Panel1.Controls.Add(Me.CurrentVolume)
        Me.Panel1.Location = New System.Drawing.Point(28, 27)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(20, 100)
        Me.Panel1.TabIndex = 1
        '
        'CurrentVolume
        '
        Me.CurrentVolume.BackColor = System.Drawing.SystemColors.HotTrack
        Me.CurrentVolume.Location = New System.Drawing.Point(0, 0)
        Me.CurrentVolume.Name = "CurrentVolume"
        Me.CurrentVolume.Size = New System.Drawing.Size(20, 100)
        Me.CurrentVolume.TabIndex = 2
        '
        'OSBVolume
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(76, 174)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.VolumeLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximumSize = New System.Drawing.Size(76, 174)
        Me.MinimumSize = New System.Drawing.Size(76, 174)
        Me.Name = "OSBVolume"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "mainPanel"
        Me.TopMost = True
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OpacityTimer As Timer
    Friend WithEvents VolumeLabel As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents CurrentVolume As Panel
End Class
