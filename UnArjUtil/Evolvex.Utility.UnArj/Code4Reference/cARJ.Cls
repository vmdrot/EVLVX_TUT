VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "cARJ"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit
' ===========================
' UnARJ32J.DLL   i I???
' cARJ(cARJ.Cls)
'  heropa at abelia ocn ne jp
'             Niiyama(HEROPA)
' ===========================

Private Declare Function UnarjGetVersion Lib "UnARJ32J.DLL" () As Integer
'Private Declare Function UnarjGetSubVersion Lib "UnARJ32J.DLL" () As Integer

Private Declare Function UnarjGetRunning Lib "UnARJ32J.DLL" () As Long

Private Declare Function UnarjGetBackGroundMode Lib "UnARJ32J.DLL" () As Long
Private Declare Function UnarjSetBackGroundMode Lib "UnARJ32J.DLL" _
                                (ByVal bBackGroundMode As Long) As Long
Private Declare Function UnarjGetCursorMode Lib "UnARJ32J.DLL" () As Long
Private Declare Function UnarjSetCursorMode Lib "UnARJ32J.DLL" _
                                (ByVal bCursorMode As Long) As Long
'Private Declare Function UnarjGetCursorInterval Lib "UnARJ32J.DLL" () As Integer
'Private Declare Function UnarjSetCursorInterval Lib "UnARJ32J.DLL" _
'                                (ByVal uInterval As Integer) As Long

Private Declare Function Unarj Lib "UnARJ32J.DLL" _
                                (ByVal hWnd As Long, _
                                 ByVal szCmdLine As String, _
                                 ByVal szOutput As String, _
                                 ByVal wSize As Long) As Long

Private Declare Function UnarjCheckArchive Lib "UnARJ32J.DLL" _
                                (ByVal szFilename As String, _
                                 ByVal iMode As Long) As Long
'Private Declare Function UnarjConfigDialog Lib "UnARJ32J.DLL" _
'                                (ByVal hWnd As Long, _
'                                 ByVal lpszComBuffer As String, _
'                                 ByVal iMode As Long) As Long
'Private Declare Function UnarjGetFileCount Lib "UnARJ32J.DLL" _
'                                (ByVal szArcFile As String) As Long

Private Declare Function UnarjOpenArchive Lib "UnARJ32J.DLL" _
                                (ByVal hWnd As Long, _
                                 ByVal szFilename As String, _
                                 ByVal dwMode As Long) As Long
Private Declare Function UnarjCloseArchive Lib "UnARJ32J.DLL" _
                                (ByVal harc As Long) As Long
Private Declare Function UnarjFindFirst Lib "UnARJ32J.DLL" _
                                (ByVal harc As Long, _
                                 ByVal szWildName As String, _
                                 lpSubInfo As INDIVIDUALINFO) As Long
Private Declare Function UnarjFindNext Lib "UnARJ32J.DLL" _
                                (ByVal harc As Long, _
                                 lpSubInfo As INDIVIDUALINFO) As Long
'Private Declare Function UnarjGetArcFileName Lib "UnARJ32J.DLL" _
'                                (ByVal harc As Long, _
'                                 ByVal lpBuffer As String, _
'                                 ByVal nSize As Long) As Long
'Private Declare Function UnarjGetArcFileSize Lib "UnARJ32J.DLL" _
'                                (ByVal harc As Long) As Long
'Private Declare Function UnarjGetArcOriginalSize Lib "UnARJ32J.DLL" _
'                                (ByVal harc As Long) As Long
'Private Declare Function UnarjGetArcCompressedSize Lib "UnARJ32J.DLL" _
'                                (ByVal harc As Long) As Long
'Private Declare Function UnarjGetArcRatio Lib "UnARJ32J.DLL" _
'                                (ByVal harc As Long) As Integer
'Private Declare Function UnarjGetArcDate Lib "UnARJ32J.DLL" _
'                                (ByVal harc As Long) As Integer
'Private Declare Function UnarjGetArcTime Lib "UnARJ32J.DLL" _
'                                (ByVal harc As Long) As Integer
'Private Declare Function UnarjGetArcOSType Lib "UnARJ32J.DLL" _
'                                (ByVal harc As Long) As Long
'Private Declare Function UnarjIsSFXFile Lib "UnARJ32J.DLL" _
'                                (ByVal harc As Long) As Long
'Private Declare Function UnarjGetFileName Lib "UnARJ32J.DLL" _
'                                (ByVal harc As Long, _
'                                 ByVal lpBuffer As String, _
'                                 ByVal nSize As Long) As Long
'Private Declare Function UnarjGetMethod Lib "UnARJ32J.DLL" _
'                                (ByVal harc As Long, _
'                                 ByVal lpBuffer As String, _
'                                 ByVal nSize As Long) As Long
'Private Declare Function UnarjGetOriginalSize Lib "UnARJ32J.DLL" _
'                                (ByVal harc As Long) As Long
'Private Declare Function UnarjGetCompressedSize Lib "UnARJ32J.DLL" _
'                                (ByVal harc As Long) As Long
'Private Declare Function UnarjGetRatio Lib "UnARJ32J.DLL" _
'                                (ByVal harc As Long) As Integer
'Private Declare Function UnarjGetDate Lib "UnARJ32J.DLL" _
'                                (ByVal harc As Long) As Integer
'Private Declare Function UnarjGetTime Lib "UnARJ32J.DLL" _
'                                (ByVal harc As Long) As Integer
'Private Declare Function UnarjGetCRC Lib "UnARJ32J.DLL" _
'                                (ByVal harc As Long) As Long
'Private Declare Function UnarjGetAttribute Lib "UnARJ32J.DLL" _
'                                (ByVal harc As Long) As Long
'Private Declare Function UnarjGetOSType Lib "UnARJ32J.DLL" _
'                                (ByVal harc As Long) As Long
'
'Private Declare Function UnarjQueryFunctionList Lib "UnARJ32J.DLL" _
'                                (ByVal iFunction As Long) As Long
'
'Private Declare Function UnarjSetOwnerWindow Lib "UnARJ32J.DLL" _
'                                (ByVal hwnd As Long) As Long
'Private Declare Function UnarjClearOwnerWindow Lib "UnARJ32J.DLL" () As Long
'
'Private Const UNPACK_CONFIG_MODE = 1
'Private Const PACK_CONFIG_MODE = 2
'
'Private Const CHECKARCHIVE_RAPID = 0
'Private Const CHECKARCHIVE_BASIC = 1
'Private Const CHECKARCHIVE_FULLCRC = 2
'
'/* MODE (for UnarjOpenArchive) */
'Private Const M_INIT_FILE_USE = &H1&
'Private Const M_REGARDLESS_INIT_FILE = &H2&
'Private Const M_NOT_USE_TIME_STAMP = &H8&
'Private Const M_EXTRACT_REPLACE_FILE = &H10&
'Private Const M_EXTRACT_NEW_FILE = &H20&
'Private Const M_EXTRACT_UPDATE_FILE = &H40&
'Private Const M_CHECK_ALL_PATH = &H100&
'Private Const M_CHECK_FILENAME_ONLY = &H200&
'Private Const M_CHECK_PATH = &H4000000
'Private Const M_CHECK_DISK_SIZE = &H400&
'Private Const M_REGARDLESS_DISK_SIZE = &H800&
'Private Const M_USE_DRIVE_LETTER = &H1000&
'Private Const M_NOT_USE_DRIVE_LETTER = &H2000&
'Private Const M_INQUIRE_DIRECTORY = &H4000&
'Private Const M_NOT_INQUIRE_DIRECTORY = &H8000&
'Private Const M_INQUIRE_WRITE = &H10000
'Private Const M_NOT_INQUIRE_WRITE = &H20000
'Private Const M_CHECK_READONLY = &H40000
'Private Const M_REGARDLESS_READONLY = &H80000
'Private Const M_REGARD_E_COMMAND = &H100000
'Private Const M_REGARD_X_COMMAND = &H200000
'Private Const M_ERROR_MESSAGE_ON = &H400000
'Private Const M_ERROR_MESSAGE_OFF = &H800000
'Private Const M_BAR_WINDOW_ON = &H1000000
'Private Const M_BAR_WINDOW_OFF = &H2000000
'
'Private Const M_MAKE_INDEX_FILE = &H10000000
Private Const M_NOT_MAKE_INDEX_FILE = &H20000000
'Private Const EXTRACT_FOUND_FILE = &H40000000
'Private Const EXTRACT_NAMED_FILE = &H80000000
'
'Private Const ISARC_FUNCTION_START = 0
'Private Const ISARC = 0
'Private Const ISARC_GET_VERSION = 1
'Private Const ISARC_GET_CURSOR_INTERVAL = 2
'Private Const ISARC_SET_CURSOR_INTERVAL = 3
'Private Const ISARC_GET_BACK_GROUND_MODE = 4
'Private Const ISARC_SET_BACK_GROUND_MODE = 5
'Private Const ISARC_GET_CURSOR_MODE = 6
'Private Const ISARC_SET_CURSOR_MODE = 7
'Private Const ISARC_GET_RUNNING = 8
'
'Private Const ISARC_CHECK_ARCHIVE = 16
'Private Const ISARC_CONFIG_DIALOG = 17
'Private Const ISARC_GET_FILE_COUNT = 18
'Private Const ISARC_QUERY_FUNCTION_LIST = 19
'Private Const ISARC_HOUT = 20
'Private Const ISARC_STRUCTOUT = 21
'Private Const ISARC_GET_ARC_FILE_INFO = 22
'
'Private Const ISARC_OPEN_ARCHIVE = 23
'Private Const ISARC_CLOSE_ARCHIVE = 24
'Private Const ISARC_FIND_FIRST = 25
'Private Const ISARC_FIND_NEXT = 26
'Private Const ISARC_EXTRACT = 27
'Private Const ISARC_ADD = 28
'Private Const ISARC_MOVE = 29
'Private Const ISARC_DELETE = 30
'
'Private Const ISARC_GET_ARC_FILE_NAME = 40
'Private Const ISARC_GET_ARC_FILE_SIZE = 41
'Private Const ISARC_GET_ARC_ORIGINAL_SIZE = 42
'Private Const ISARC_GET_ARC_COMPRESSED_SIZE = 43
'Private Const ISARC_GET_ARC_RATIO = 44
'Private Const ISARC_GET_ARC_DATE = 45
'Private Const ISARC_GET_ARC_TIME = 46
'Private Const ISARC_GET_ARC_OS_TYPE = 47
'Private Const ISARC_GET_ARC_IS_SFX_FILE = 48
'Private Const ISARC_GET_FILE_NAME = 57
'Private Const ISARC_GET_ORIGINAL_SIZE = 58
'Private Const ISARC_GET_COMPRESSED_SIZE = 59
'Private Const ISARC_GET_RATIO = 60
'Private Const ISARC_GET_DATE = 61
'Private Const ISARC_GET_TIME = 62
'Private Const ISARC_GET_CRC = 63
'Private Const ISARC_GET_ATTRIBUTE = 64
'Private Const ISARC_GET_OS_TYPE = 65
'Private Const ISARC_GET_METHOD = 66
'
'Private Const ISARC_FUNCTION_END = 66
'
''/* Attribute */
'Private Const FA_RDONLY = &H1&             '/* Read only attribute */
'Private Const FA_HIDDEN = &H2&             '/* Hidden file */
'Private Const FA_SYSTEM = &H4&             '/* System file */
'Private Const FA_LABEL = &H8&             '/* Volume label */
'Private Const FA_DIREC = &H10&            '/* Directory */
'Private Const FA_ARCH = &H20&            '/* Archive */

' ----------------------------
' API O A I e ?
' ----------------------------

' ----------------------------
'  N   X a A g p · e I   I e ?
' ----------------------------
Private mstrCommandLine  As String  ' UnARJ32J.DLL I?IYA???Y
Private mstrOutput       As String  ' ?Y?°U o I
Private mlngReturnCode   As Long    ' UnARJ32J.DLL I ? e l
Private mintMaxFileCount As Integer
Private mintTestMode     As Integer
Private mlnghWnd         As Long

Private mcolArcFiles     As ArcFiles
'
'
'
Public Property Set ArcFiles(vData As ArcFiles)
    Set mcolArcFiles = vData
End Property
'
'
'
Public Property Get ArcFiles() As ArcFiles
    If mcolArcFiles Is Nothing Then
        Set mcolArcFiles = New ArcFiles
    End If
    Set ArcFiles = mcolArcFiles
End Property

'
' UnARJ32J.DLL I R iO°A? ? ? e
'
Public Property Get BackGroundMode() As Boolean
    Dim lngResult As Long
    lngResult = UnarjGetBackGroundMode()
    If lngResult <> 0 Then
        BackGroundMode = True
    Else
        BackGroundMode = False
    End If
End Property

'
' UnARJ32J.DLL I R iO°A? ? Y e · e B
'
Public Property Let BackGroundMode(bNewValue As Boolean)
    Dim lngResult As Long
    lngResult = UnarjSetBackGroundMode(IIf(bNewValue, 1, 0))
End Property

'
'
'
Public Function CheckArchive(strFileName As String) As Boolean
    Dim lngResult As Long
    lngResult = UnarjCheckArchive(strFileName, mintTestMode)
    If lngResult <> 0 Then
        CheckArchive = True
    Else
        CheckArchive = False
    End If
End Function

'
'
'
Public Property Let CheckMode(intNewCheckMode As Integer)
    mintTestMode = intNewCheckMode
End Property

'
'
'
Public Property Get CheckMode() As Integer
    CheckMode = mintTestMode
End Property

'
'
'
Public Sub Class_Initialize()
    mintMaxFileCount = 256
    mintTestMode = 4
End Sub

'
'
'
Private Sub Class_Terminate()
  Set mcolArcFiles = Nothing
End Sub

'
' cARJ ? n   · e e O aUI?U EI?UE?A?
'
Public Property Let CommandLine(strCommandLine As String)
    Dim lngResult As Long
    Dim strOutPutBuffer As String
    Dim lngPos As Long

    strOutPutBuffer = String$((mintMaxFileCount + 4) * 80, vbNullChar)
    lngResult = Unarj(mlnghWnd, strCommandLine, strOutPutBuffer, Len(strOutPutBuffer))
    lngPos = InStr(strOutPutBuffer, vbNullChar)
    If lngPos > 0 Then
        mstrOutput = Left$(strOutPutBuffer, lngPos - 1)
    Else
        mstrOutput = strOutPutBuffer & "[more...]"
    End If
    mstrCommandLine = strCommandLine
    mlngReturnCode = lngResult
End Property

'
' UnARJ32J.DLL IUnarj  A g i e ??IYA???Y ? O · B
'
Public Property Get CommandLine() As String
    CommandLine = mstrCommandLine
End Property

'
' UnARJ32J.DLL I Y eA??+U?? ? o · B
'
'Public Function ConfigDialog(strOption As String, ByVal lngMode As Long) As String
'    Dim lngResult As Long
'    Const MIN_BUFFER = 256 + 1
'    ' 513E??A E a IE??I§ ? p O · e B
'    strOption = Left$(strOption & String$(MIN_BUFFER, vbNullChar), MIN_BUFFER)
'    lngResult = UnarjConfigDialog(mlnghWnd, _
'                                  strOption, _
'                                  lngMode)
'    If lngResult <> 0 Then
'        ConfigDialog = Left$(strOption, InStr(strOption, vbNullChar) - 1)
'    Else
'        ConfigDialog = vbNullString
'    End If
'    mlngReturnCode = lngResult
'End Function

'
' UnARJ32J.DLL I¶°?UO°A? ? Y e · e B
'
Public Property Let CursorMode(blnNewValue As Boolean)
    Dim lngResult As Long
    lngResult = UnarjSetCursorMode(IIf(blnNewValue, 1, 0))
End Property

'
' UnARJ32J.DLL I¶°?UO°A? ? ? e B
'
Public Property Get CursorMode() As Boolean
    Dim lngResult As Long
    lngResult = UnarjGetCursorMode()
    If lngResult <> 0 Then
        CursorMode = True
    Else
        CursorMode = False
    End If
End Property

'
'
'
Public Sub Extract(strArchiveFile As String, strDstDir As String, Optional strFile As String = vbNullString, Optional intOption As ArcOptionContents = ArcEffectiveUpdate Or ArcEffectiveDirectory Or ArcEffectiveAttribute)
'Private Const conOptionArjExtract As String = "x -u -a -hp1 -hk "
' x   :  S p X ? A t @ C   ? ?  
' -u  :  t @ C   I X V
' -a  :  C O I t @ C   R < ?   ·
' -hj : x  ? ? ? e  ? ? ? ? l ? c E ·(x  ? ? A f B   N g   ? \ z ÷ E ?)
' -hp+:  i > o [ ? v   O   X E o [ A \ ¦
' -hk :  o o t @ C   I i ¬ ? } ~
    Dim strOption       As String
    Dim strCommandLine  As String
    Dim clsFile         As cFile

    '  R < ? A > · e B
    If intOption And ArcEffectiveAttribute Then
        strOption = "-a "
    Else
        strOption = vbNullString
    End If
    '  f B   N g   ? L o
    If intOption And ArcEffectiveDirectory Then
        strOption = strOption & vbNullString
    Else
        strOption = strOption & "-hj "
    End If
    '  t @ C   I X V
    If intOption And ArcEffectiveUpdate Then
        strOption = strOption & "-u "
    Else
        strOption = strOption & vbNullString
    End If
    If Len(strFile) > 0 Then
        strCommandLine = "x " & strOption & "-hp+ -hk -jyc " & _
                         strArchiveFile & " " & _
                         strDstDir & " " & _
                         strFile
    Else
        strCommandLine = "x " & strOption & "-hp+ -hk -jyc " & _
                         strArchiveFile & " " & _
                         strDstDir
    End If
    ' UnARJ32J.DLL I ¶ Y ÷ E ? f B   N g   O ?   ?   ? E ? c ÷ ? H
    '  i ? f B   N g   ? i ¬ ÷ A c c ?   · e B
    '  ^ ` ? < ? I I AVB ? ?   ? A ÷ U ¤ + ? A   e B
    Set clsFile = New cFile
    Call clsFile.MakeDirs(strDstDir)
    Set clsFile = Nothing
    CommandLine = strCommandLine
End Sub

'
'
'
Public Property Let MaxFileCount(intNewMaxFileCount As Integer)
    mintMaxFileCount = intNewMaxFileCount
End Property

'
'
'
Public Property Get MaxFileCount() As Integer
    MaxFileCount = mintMaxFileCount
End Property

'
'
'
Public Property Get OutPut() As String
    Dim lngLoopCount As Long
    ' vbLF ? c A ? ? c AvbCRLF E I ·
    For lngLoopCount = Len(mstrOutput) To 1 Step -1
        If Mid$(mstrOutput, lngLoopCount, 1) = vbLf Then
            mstrOutput = Left$(mstrOutput, lngLoopCount - 1) & vbCr & _
                          Mid$(mstrOutput, lngLoopCount)
        End If
    Next lngLoopCount

    #If DebugMode Then
        OutPut = mstrCommandLine & vbCrLf & _
                 mstrOutput
    #Else
        OutPut = mstrOutput
    #End If
End Property

'
'
'
Public Property Get ReturnCode() As Long
    ReturnCode = mlngReturnCode
End Property

'
'
'
Public Property Get Running() As Boolean
    Dim lngResult As Long
    lngResult = UnarjGetRunning()
    If lngResult <> 0 Then
        Running = True
    Else
        Running = False
    End If
End Property

'
'
'
Public Function SetArcFile(strArcFile As String) As Long
    Dim lngArcHandle        As Long
    Dim strMsg              As String
    Dim intType             As Integer
    Dim udtIndividualinfo   As INDIVIDUALINFO
    Dim lngResult           As Long
    Dim strArcFileName      As String
    Dim objArcFile          As ArcFile
    Dim clsFile             As cFile

    Set clsFile = New cFile

    lngArcHandle = UnarjOpenArchive(mlnghWnd, _
                                    strArcFile, _
                                    M_NOT_MAKE_INDEX_FILE)
    If lngArcHandle = 0& Then
        ' n   h   ? ? e E c A ? B
        strMsg = "'" & strArcFile & "'  IEYA?U I ? ? E ? s ÷ ? I T" & vbCr _
                & " R(  L [ M) ¦I?"
        intType = vbOKOnly Or vbExclamation Or vbApplicationModal
        Call MsgBox(strMsg, intType, App.Title)
    Else
        If UnarjFindFirst(lngArcHandle, "*.*", udtIndividualinfo) = 0& Then
            Do
                '  t @ C   ? I ? ? B
                strArcFileName = Left$(udtIndividualinfo.szFilename, InStr(udtIndividualinfo.szFilename, vbNullChar) - 1)

                If InStr(strArcFileName, "/") Or InStr(strArcFileName, "\") Then
                    Set objArcFile = ArcFiles.Add(Right$("0000" & ArcFiles.Count, 4) & strArcFileName, _
                                                  clsFile.GetFileName(strArcFileName), _
                                                  clsFile.GetPathName(strArcFileName) & "\")
                Else
                    Set objArcFile = ArcFiles.Add(Right$("0000" & ArcFiles.Count, 4) & strArcFileName, _
                                                  strArcFileName, _
                                                  "\")
                End If
                With objArcFile
                    .OriginalSize = udtIndividualinfo.dwOriginalSize
                    .CompressedSize = udtIndividualinfo.dwCompressedSize
                    .Ratio = Format$(udtIndividualinfo.wRatio / 10, "0.0")
                    '  t @ C   I X V u  
                    .WriteTime = clsFile.DosDateTimeToVBDate(CLng(udtIndividualinfo.wDate), CLng(udtIndividualinfo.wTime))
                    .Attr = StrConv(Left$(udtIndividualinfo.szAttribute, 4), vbLowerCase)
                    .Method = Left$(udtIndividualinfo.szMode, 5)
                    .CRC = udtIndividualinfo.dwCRC
                End With
                '  t @ C   ?vbNullString E I I A f B   N g   ? ? ? ? i [ ? e A ? e B
                If Len(objArcFile.filename) = 0 Then
                    '  f B   N g   ? ? ? I i n I ? e   - B
                    ArcFiles.Remove ArcFiles.Count
                End If
            Loop While UnarjFindNext(lngArcHandle, udtIndividualinfo) = 0&
        End If
        lngResult = UnarjCloseArchive(lngArcHandle)
    End If
    SetArcFile = ArcFiles.Count

    Set clsFile = New cFile
End Function

'
'
'
Public Property Get Version() As Long
    Dim lngResult As Long
    lngResult = UnarjGetVersion()
    Version = lngResult
End Property

'
'
'
Public Property Get hWnd() As Long
    hWnd = mlnghWnd
End Property

'
'
'
Public Property Let hWnd(lngNewhWnd As Long)
    mlnghWnd = lngNewhWnd
End Property