Imports CoreAudioApi
Imports System.IO
Imports System.Runtime.InteropServices


Public Class OSBVolume
    Dim Svol As Integer = 0
    Dim IncreaseKey As String = GetKEY("osdConfig.ini", 2, 12)
    Dim DecreaseKey As String = GetKEY("osdConfig.ini", 3, 12)
    'FUNCTION GET KEYS
    Public Shared Function GetKEY(path As String, count As Integer, substring As Integer) As String
        Dim allLines As String() = File.ReadAllLines(path)
        Dim randomLine As String = allLines(count)
        Return randomLine.Substring(substring)
    End Function

    'FUNCTION SET VOLUME
    Private Function SetVol() As Integer
        Dim DevEnum As New MMDeviceEnumerator
        Dim device As MMDevice = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia)
        device.AudioEndpointVolume.MasterVolumeLevelScalar = Svol / 100.0F
    End Function

    'FUNCTION GET VOLUME
    Private Function GetVol() As Integer
        Dim MasterMin As Integer = 0
        Dim DevEnum As New MMDeviceEnumerator
        Dim device As MMDevice = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia)
        Dim Vol As Integer = 0

        With device.AudioEndpointVolume
            Vol = CInt(.MasterVolumeLevelScalar * 100)
            If Vol < MasterMin Then
                Vol = MasterMin / 100
            End If
        End With
        Return Vol
    End Function


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).SetValue(Application.ProductName, Application.ExecutablePath)
        'My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).DeleteValue(Application.ProductName)
        Me.Opacity = 0
        Me.TopMost = True
        Me.Location = New Point(Screen.PrimaryScreen.Bounds.Right - (Me.Width + 30), (My.Computer.Screen.Bounds.Size.Height - Me.Height - 80))
        VolumeLabel.Text = GetVol()
        CurrentVolume.Size = New System.Drawing.Size(20, GetVol())

        If GetVol() < 100 And GetVol() > 10 Then
            Me.VolumeLabel.Location = New System.Drawing.Point(27, 147)
        ElseIf GetVol() < 10 Then
            Me.VolumeLabel.Location = New System.Drawing.Point(30, 147)
        End If
    End Sub



    Private Sub OSBVolume_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp

        If (e.KeyCode And Not Keys.Modifiers) = Keys.OemOpenBrackets AndAlso e.Modifiers = Keys.Control Then
            OpacityTimer.Start()
        End If

        If (e.KeyCode And Not Keys.Modifiers) = Keys.OemCloseBrackets AndAlso e.Modifiers = Keys.Control Then
            OpacityTimer.Start()
        End If

    End Sub

    Private Sub OpacityTimer_Tick(sender As Object, e As EventArgs) Handles OpacityTimer.Tick
        Me.Opacity = 0
    End Sub

    Private WithEvents KbHook As New KeyboardHook
    Private Sub kbHook_KeyDown(ByVal e As System.Windows.Forms.Keys) Handles KbHook.KeyDown
        Debug.WriteLine(e)
        Console.WriteLine(GetKEY("osdConfig.ini", 3, 12))
        If (e.ToString() = DecreaseKey) Then
            OpacityTimer.Stop()
            Me.Opacity = 100
            Try
                Svol = GetVol() - 10
                SetVol()
                If GetVol() < 100 And GetVol() > 10 Then
                    Me.VolumeLabel.Location = New System.Drawing.Point(28, 147)
                ElseIf GetVol() < 10 Then
                    Me.VolumeLabel.Location = New System.Drawing.Point(30, 147)
                ElseIf GetVol() = 100 Then
                    Me.VolumeLabel.Location = New System.Drawing.Point(25, 147)
                End If
                VolumeLabel.Text = GetVol()

                CurrentVolume.Size = New System.Drawing.Size(20, GetVol())

            Catch ex As Exception

            End Try
        End If

        If (e.ToString() = IncreaseKey) Then
            OpacityTimer.Stop()
            Me.Opacity = 100
            Try
                Svol = GetVol() + 10
                SetVol()
                If GetVol() < 100 And GetVol() > 10 Then
                    Me.VolumeLabel.Location = New System.Drawing.Point(28, 147)
                ElseIf GetVol() < 10 Then
                    Me.VolumeLabel.Location = New System.Drawing.Point(30, 147)
                ElseIf GetVol() = 100 Then
                    Me.VolumeLabel.Location = New System.Drawing.Point(25, 147)
                End If
                VolumeLabel.Text = GetVol()

                CurrentVolume.Size = New System.Drawing.Size(20, GetVol())
            Catch ex As Exception

            End Try
        End If



    End Sub

    Private Sub kbHook_KeyUp(ByVal e As System.Windows.Forms.Keys) Handles KbHook.KeyUp
        Debug.WriteLine(e.ToString)
        If (e.ToString() = DecreaseKey) Then
            OpacityTimer.Start()
        End If

        If (e.ToString() = IncreaseKey) Then
            OpacityTimer.Start()
        End If
    End Sub
End Class


Public Class KeyboardHook

    <DllImport("User32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall)>
    Private Overloads Shared Function SetWindowsHookEx(ByVal idHook As Integer, ByVal HookProc As KBDLLHookProc, ByVal hInstance As IntPtr, ByVal wParam As Integer) As Integer
    End Function
    <DllImport("User32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall)>
    Private Overloads Shared Function CallNextHookEx(ByVal idHook As Integer, ByVal nCode As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Integer
    End Function
    <DllImport("User32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall)>
    Private Overloads Shared Function UnhookWindowsHookEx(ByVal idHook As Integer) As Boolean
    End Function

    <StructLayout(LayoutKind.Sequential)>
    Private Structure KBDLLHOOKSTRUCT
        Public vkCode As UInt32
        Public scanCode As UInt32
        Public flags As KBDLLHOOKSTRUCTFlags
        Public time As UInt32
        Public dwExtraInfo As UIntPtr
    End Structure

    <Flags()>
    Private Enum KBDLLHOOKSTRUCTFlags As UInt32
        LLKHF_EXTENDED = &H1
        LLKHF_INJECTED = &H10
        LLKHF_ALTDOWN = &H20
        LLKHF_UP = &H80
    End Enum

    Public Shared Event KeyDown(ByVal Key As Keys)
    Public Shared Event KeyUp(ByVal Key As Keys)

    Private Const WH_KEYBOARD_LL As Integer = 13
    Private Const HC_ACTION As Integer = 0
    Private Const WM_KEYDOWN = &H100
    Private Const WM_KEYUP = &H101
    Private Const WM_SYSKEYDOWN = &H104
    Private Const WM_SYSKEYUP = &H105

    Private Delegate Function KBDLLHookProc(ByVal nCode As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Integer

    Private KBDLLHookProcDelegate As KBDLLHookProc = New KBDLLHookProc(AddressOf KeyboardProc)
    Private HHookID As IntPtr = IntPtr.Zero

    Private Function KeyboardProc(ByVal nCode As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Integer
        If (nCode = HC_ACTION) Then
            Dim struct As KBDLLHOOKSTRUCT
            Select Case wParam
                Case WM_KEYDOWN, WM_SYSKEYDOWN
                    RaiseEvent KeyDown(CType(CType(Marshal.PtrToStructure(lParam, struct.GetType()), KBDLLHOOKSTRUCT).vkCode, Keys))
                Case WM_KEYUP, WM_SYSKEYUP
                    RaiseEvent KeyUp(CType(CType(Marshal.PtrToStructure(lParam, struct.GetType()), KBDLLHOOKSTRUCT).vkCode, Keys))
            End Select
        End If
        Dim ret As Integer = CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam)
        Return ret
    End Function

    Public Sub New()
        HHookID = SetWindowsHookEx(WH_KEYBOARD_LL, KBDLLHookProcDelegate, System.Runtime.InteropServices.Marshal.GetHINSTANCE(System.Reflection.Assembly.GetExecutingAssembly.GetModules()(0)).ToInt32, 0)
        If HHookID = IntPtr.Zero Then
            Throw New Exception("Error!")
        End If
    End Sub

    Protected Overrides Sub Finalize()
        If Not HHookID = IntPtr.Zero Then
            UnhookWindowsHookEx(HHookID)
        End If
        MyBase.Finalize()
    End Sub
End Class

