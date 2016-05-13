 
'========================================================================================
' Copyright (c) 2016 Carlos I. Nunez. All rights reserved.
'========================================================================================
'
'	Project File:	HermesFramework / Utils.Compressor.vb
'	Created on:		5/13/2016 @ 5:10 PM
'	Modified on:	5/13/2016 @ 5:12 PM 
'	Author:			Nunez, Carlos 
' 
'========================================================================================
 
Imports System.IO
Imports System.IO.Compression
Imports System.Text

Namespace Utils
    Friend NotInheritable Class Compressor

        Private Sub New()
        End Sub

        Public Shared Function Zip(text As String) As String
            Dim buffer__1 As Byte() = Encoding.UTF8.GetBytes(text)
            Dim memoryStream = New MemoryStream()
            Using gZipStream = New GZipStream(memoryStream, CompressionMode.Compress, True)
                gZipStream.Write(buffer__1, 0, buffer__1.Length)
            End Using

            memoryStream.Position = 0

            Dim compressedData = New Byte(memoryStream.Length - 1) {}
            memoryStream.Read(compressedData, 0, compressedData.Length)

            Dim gZipBuffer = New Byte(compressedData.Length + 3) {}
            Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length)
            Buffer.BlockCopy(BitConverter.GetBytes(buffer__1.Length), 0, gZipBuffer, 0, 4)
            Return Convert.ToBase64String(gZipBuffer)
        End Function


        Public Shared Function UnZip(compressedText As String) As String
            Dim gZipBuffer As Byte() = Convert.FromBase64String(compressedText)
            Using memoryStream = New MemoryStream()
                Dim dataLength As Integer = BitConverter.ToInt32(gZipBuffer, 0)
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4)

                Dim buffer = New Byte(dataLength - 1) {}

                memoryStream.Position = 0
                Using gZipStream = New GZipStream(memoryStream, CompressionMode.Decompress)
                    gZipStream.Read(buffer, 0, buffer.Length)
                End Using

                Return Encoding.UTF8.GetString(buffer)
            End Using
        End Function

    End Class

End Namespace
