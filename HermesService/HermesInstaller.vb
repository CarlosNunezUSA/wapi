Imports System.Configuration.Install
Imports System.ServiceProcess

Public Class HermesInstaller

    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    Protected Sub ServiceInstaller_Committed(sender As Object, e As InstallEventArgs) Handles Me.Committed
        Using sc As New ServiceController(ServiceInstal.ServiceName)
            sc.Start()
        End Using
    End Sub

    Protected Sub ServiceInstaller_OnUninstall() Handles Me.BeforeUninstall
        Using sc As New ServiceController(ServiceInstal.ServiceName)
            If sc.Status <> ServiceControllerStatus.Stopped AndAlso sc.Status <> ServiceControllerStatus.StopPending Then
                sc.Stop()
            End If
        End Using
    End Sub

End Class
