Option Explicit

Private ArcHead(10) As String       'API????? (Unlha~)
Private ArcAPI(4) As Long           'API???????
Private DllEnabled(10) As Boolean   'DLL?????????
Private DllActive As Long           '??????????????

Private Sub Combo1_Click()
    Dim ArcHead2 As String
    ArcHead2 = ArcHead(Combo1.ListIndex)
        If DllEnabled(Combo1.ListIndex) = False Then
            'DLL????????????
            Combo1.ListIndex = DllActive
            MsgBox Combo1.List(Combo1.ListIndex) & "?????????", _
					vbExclamation, "???"
            Exit Sub
        End If
    With Combo2
        '??????????????
        .Clear
        '????????API?????
        .AddItem ArcHead2 & "GetRunning"
        .AddItem ArcHead2 & "CheckArchive"
        .AddItem ArcHead2 & "FindFirst"
        .AddItem ArcHead2 & "FindNext"
        .ListIndex = 1
    End With
    DllActive = Combo1.ListIndex
End Sub

Private Sub Command1_Click()
    Dim m As Long
    Dim ret As Long
    'API??????????
    m = ArcAPI(Combo2.ListIndex)
    
    'DLL?????API???
    Select Case Combo1.ListIndex
        Case 0
            ret = UnlhaQueryFunctionList(m)
        Case 1
            ret = ZipQueryFunctionList(m)
        Case 2
            ret = UnZipQueryFunctionList(m)
        Case 3
            ret = CabQueryFunctionList(m)
        Case 4
            ret = TarQueryFunctionList(m)
        Case 5
            ret = UnarjQueryFunctionList(m)
        Case 6
            ret = UnrarQueryFunctionList(m)
        Case 7
            ret = BgaQueryFunctionList(m)
        Case 8
            ret = Yz1QueryFunctionList(m)
        Case 9
            ret = UnGCAQueryFunctionList(m)
    End Select
    
    If ret = 1 Then
        MsgBox Combo2.List(Combo2.ListIndex) & "????????", _
			vbInformation, Combo1.List(Combo1.ListIndex)
    Else
        MsgBox Combo2.List(Combo2.ListIndex) & "?????????", _
			vbInformation, Combo1.List(Combo1.ListIndex)
    End If
End Sub

Private Sub Form_Load()
    Dim i As Long
    Dim strBuf As String * 255
    Dim strBuf2 As String * 255
    
    'Unlha32.dll?????????????
    DllActive = -1
    
    'API?????????????
    ArcHead(0) = "Unlha"
    ArcHead(1) = "Zip"
    ArcHead(2) = "UnZip"
    ArcHead(3) = "Cab"
    ArcHead(4) = "Tar"
    ArcHead(5) = "Unarj"
    ArcHead(6) = "Unrar"
    ArcHead(7) = "Bga"
    ArcHead(8) = "Yz1"
    ArcHead(9) = "UnGCA"
    
    'DLL????????????
    With Combo1
        .AddItem "Unlha32.dll", 0
        .AddItem "Zip32j.dll"
        .AddItem "UnZip32.dll"
        .AddItem "Cab32.dll"
        .AddItem "Tar32.dll"
        .AddItem "Unarj32j.dll"
        .AddItem "Unrar32.dll"
        .AddItem "Bga32.dll"
        .AddItem "YZ1.dll"
        .AddItem "UnGCA32.dll"
    End With
    
    '??????????????????????
    ArcAPI(0) = 8
    ArcAPI(1) = 16
    ArcAPI(2) = 25
    ArcAPI(3) = 26
    For i = 0 To 10
        '???DLL??????????????
        If SearchPath(vbNullString, Combo1.List(i), _
                    vbNullString, 255, strBuf, strBuf2) <> 0 Then
            DllEnabled(i) = True
        Else
            DllEnabled(i) = False
        End If
    Next i
    Combo1.ListIndex = 0
End Sub
