<System.ComponentModel.RunInstaller(True)> Partial Class HermesInstaller
    Inherits System.Configuration.Install.Installer

    'Installer overrides dispose to clean up the component list.
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

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ProcessInstal = New System.ServiceProcess.ServiceProcessInstaller()
        Me.ServiceInstal = New System.ServiceProcess.ServiceInstaller()
        '
        'ProcessInstal
        '
        Me.ProcessInstal.Account = System.ServiceProcess.ServiceAccount.LocalService
        Me.ProcessInstal.Password = Nothing
        Me.ProcessInstal.Username = Nothing
        '
        'ServiceInstal
        '
        Me.ServiceInstal.Description = "Hermes Service"
        Me.ServiceInstal.DisplayName = "Hermes Service"
        Me.ServiceInstal.ServiceName = "Hermes Service"
        Me.ServiceInstal.StartType = System.ServiceProcess.ServiceStartMode.Automatic
        '
        'HermesInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.ProcessInstal, Me.ServiceInstal})

End Sub

    Friend WithEvents ProcessInstal As ServiceProcess.ServiceProcessInstaller
    Friend WithEvents ServiceInstal As ServiceProcess.ServiceInstaller
End Class
