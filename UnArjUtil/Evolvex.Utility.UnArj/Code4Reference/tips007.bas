Option Explicit

Public Declare Function SearchPath Lib "kernel32" Alias "SearchPathA" _
    (ByVal lpPath As String, _
    ByVal lpFileName As String, _
    ByVal lpExtension As String, _
    ByVal nBufferLength As Long, _
    ByVal lpBuffer As String, _
    ByVal lpFilePart As String) As Long

Public Declare Function UnlhaQueryFunctionList Lib "Unlha32" _
    (ByVal iFunction As Long) As Long

Public Declare Function ZipQueryFunctionList Lib "Zip32j" _
    (ByVal iFunction As Long) As Long

Public Declare Function UnZipQueryFunctionList Lib "UnZip32" _
    (ByVal iFunction As Long) As Long

Public Declare Function CabQueryFunctionList Lib "Cab32" _
    (ByVal iFunction As Long) As Long

Public Declare Function TarQueryFunctionList Lib "Tar32" _
    (ByVal iFunction As Long) As Long

Public Declare Function UnarjQueryFunctionList Lib "Unarj32j" _
    (ByVal iFunction As Long) As Long

Public Declare Function UnrarQueryFunctionList Lib "Unrar32" _
    (ByVal iFunction As Long) As Long

Public Declare Function BgaQueryFunctionList Lib "Bga32" _
    (ByVal iFunction As Long) As Long

Public Declare Function Yz1QueryFunctionList Lib "Yz1" _
    (ByVal iFunction As Long) As Long

Public Declare Function UnGCAQueryFunctionList Lib "UnGCA32" _
    (ByVal iFunction As Long) As Long
