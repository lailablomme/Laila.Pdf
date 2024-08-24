Imports System.Drawing
Imports System.Drawing.Printing
Imports System.IO
Imports System.Threading
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports PDFiumSharp

Public Class Printer
    Private _currentPageIndex As Integer
    Private _streamsPage As IList(Of Stream)
    Private _isLandscape As List(Of Boolean)
    Private Shared _lock As SemaphoreSlim = New SemaphoreSlim(1, 1)

    Public Async Function Print(displayName As String, bytes As Byte(), Optional printerName As String = Nothing, Optional twoSided As Boolean? = True) As Task
        Dim f As Func(Of Task) =
            Async Function() As Task
                ' print
                _streamsPage = New List(Of Stream)()
                _isLandscape = New List(Of Boolean)
                Await export(bytes)
                For Each stream As Stream In _streamsPage
                    stream.Position = 0
                Next
                printPDF(displayName, printerName, twoSided)
            End Function
        Await Task.Run(f)
    End Function

    Private Async Function export(ByVal bytes As Byte()) As Task
        Await _lock.WaitAsync()
        Try
            Using document = New PdfDocument(bytes, 0, bytes.Length)
                For Each page In document.Pages
                    Dim writableBitmap As WriteableBitmap = New WriteableBitmap(page.Width * 4, page.Height * 4, 96, 96, PixelFormats.Bgra32, Nothing)
                    page.RenderPage(writableBitmap)
                    page.RenderForm(writableBitmap)
                    Dim mem As MemoryStream = New MemoryStream()
                    Dim encoder As PngBitmapEncoder = New PngBitmapEncoder()
                    encoder.Frames.Add(BitmapFrame.Create(writableBitmap))
                    encoder.Save(mem)
                    _streamsPage.Add(mem)
                    _isLandscape.Add(page.Width > page.Height)
                Next
            End Using
        Finally
            _lock.Release()
        End Try
    End Function

    Private Sub printPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        Dim pageBitmap As Image = Nothing
        pageBitmap = Image.FromStream(_streamsPage(_currentPageIndex))

        ' Adjust rectangular area with printer margins.
        Dim adjustedRect As System.Drawing.Rectangle =
            New System.Drawing.Rectangle(e.PageBounds.Left - CInt(e.PageSettings.HardMarginX),
                                         e.PageBounds.Top - CInt(e.PageSettings.HardMarginY),
                                         e.PageBounds.Width,
                                         e.PageBounds.Height)

        ' Draw a white background for the report
        e.Graphics.FillRectangle(System.Drawing.Brushes.White, adjustedRect)

        ' Draw the report content
        e.Graphics.DrawImage(pageBitmap, adjustedRect)

        ' Dispose of resources
        pageBitmap.Dispose()
        _streamsPage(_currentPageIndex).Dispose()

        ' Prepare for the next page. Make sure we haven't hit the end.
        _currentPageIndex += 1
        e.HasMorePages = (_currentPageIndex < _streamsPage.Count)
        If e.HasMorePages Then
            e.PageSettings.Landscape = _isLandscape(_currentPageIndex)
        End If
    End Sub

    Private Sub printPDF(displayName As String, printerName As String, isTwoSided As Boolean)
        If _streamsPage Is Nothing OrElse _streamsPage.Count = 0 Then
            Throw New Exception("No stream to print.")
        End If

        Dim printDoc As New PrintDocument()
        printDoc.DocumentName = displayName
        printDoc.PrintController = New StandardPrintController()
        printDoc.DefaultPageSettings.Landscape = If(_isLandscape.Count > 0, _isLandscape(0), False)
        printDoc.DefaultPageSettings.PrinterSettings.Duplex = If(isTwoSided, Duplex.Vertical, Duplex.Simplex)
        printDoc.PrinterSettings.Duplex = If(isTwoSided, Duplex.Vertical, Duplex.Simplex)
        If Not printerName Is Nothing Then
            If isPrinterInstalled(printerName) Then
                printDoc.PrinterSettings.PrinterName = printerName
            End If
        End If
        If Not printDoc.PrinterSettings.IsValid Then
            Throw New Exception("Cannot find the default printer.")
        Else
            AddHandler printDoc.PrintPage, AddressOf printPage
            _currentPageIndex = 0
            printDoc.Print()
        End If
    End Sub

    Private Function isPrinterInstalled(printerName As String) As Boolean
        For Each p In PrinterSettings.InstalledPrinters
            If p = printerName Then
                Return True
            End If
        Next
        Return False
    End Function
End Class
