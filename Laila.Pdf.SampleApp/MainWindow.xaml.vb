Imports System.ComponentModel
Imports System.IO
Imports System.Security.Cryptography
Imports Microsoft.Win32

Class MainWindow
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private _isTextSelected As Boolean = False
    Private _currentMatchIndex As Integer = 0
    Private _numberOfMatches As Integer = 0
    Private _tool As Tool = Tool.Zoom
    Private _fileName As String
    Private _totalPages As Integer
    Private _currentPage As Integer

    Private Sub openPDFButton_Click(sender As Object, e As RoutedEventArgs) Handles openPDFButton.Click
        Dim ofd As OpenFileDialog = New OpenFileDialog()
        ofd.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*"
        If ofd.ShowDialog() Then
            _fileName = ofd.FileName
            viewer.Document = File.ReadAllBytes(ofd.FileName)
        End If
    End Sub

    Private Sub refreshPDFButton_Click(sender As Object, e As RoutedEventArgs) Handles refreshPDFButton.Click
        viewer.Document = File.ReadAllBytes(_fileName)
    End Sub

    Private Sub savePDFButton_Click(sender As Object, e As RoutedEventArgs) Handles savePDFButton.Click
        Dim ofd As SaveFileDialog = New SaveFileDialog()
        ofd.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*"
        If ofd.ShowDialog() Then
            File.WriteAllBytes(ofd.FileName, viewer.Save())
        End If
    End Sub

    Private Sub fitToWidthButton_Click(sender As Object, e As RoutedEventArgs) Handles fitToWidthButton.Click
        viewer.FitToWidth()
    End Sub

    Private Sub fitToHeightButton_Click(sender As Object, e As RoutedEventArgs) Handles fitToHeightButton.Click
        viewer.FitToHeight()
    End Sub

    Private Sub copyTextButton_Click(sender As Object, e As RoutedEventArgs) Handles copyTextButton.Click
        viewer.CopyTextToClipboard()
    End Sub

    Private Sub printButton_Click(sender As Object, e As RoutedEventArgs) Handles printButton.Click
        Me.Cursor = Cursors.Wait
        Dim printer As Printer = New Printer()
        AddHandler printer.PrintProgress,
            Sub(s As Object, e2 As Printer.PrintProgressEventArgs)
                Me.TotalPages = e2.TotalPages
                Me.CurrentPage = e2.CurrentPage
                Application.Current.Dispatcher.Invoke(
                    Sub()
                    End Sub, Windows.Threading.DispatcherPriority.ContextIdle)
            End Sub
        printer.Print("pdf", viewer.Save())
        Me.Cursor = Nothing
    End Sub

    Private Sub prevMatchButton_Click(sender As Object, e As RoutedEventArgs) Handles prevMatchButton.Click
        If Me.CurrentMatchIndex > 0 Then
            Me.CurrentMatchIndex -= 1
        Else
            Me.CurrentMatchIndex = Me.NumberOfMatches - 1
        End If
    End Sub

    Private Sub nextMatchButton_Click(sender As Object, e As RoutedEventArgs) Handles nextMatchButton.Click
        If Me.CurrentMatchIndex < Me.NumberOfMatches - 1 Then
            Me.CurrentMatchIndex += 1
        Else
            Me.CurrentMatchIndex = 0
        End If
    End Sub

    Public Property Tool As Tool
        Get
            Return _tool
        End Get
        Set(value As Tool)
            _tool = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Tool"))
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("IsZoomChecked"))
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("IsSelectChecked"))
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("IsFormChecked"))
        End Set
    End Property

    Public Property IsZoomChecked As Boolean
        Get
            Return Me.Tool = Tool.Zoom
        End Get
        Set(value As Boolean)
            If value Then Me.Tool = Tool.Zoom
        End Set
    End Property

    Public Property IsSelectChecked As Boolean
        Get
            Return Me.Tool = Tool.Select
        End Get
        Set(value As Boolean)
            If value Then Me.Tool = Tool.Select
        End Set
    End Property

    Public Property IsFormChecked As Boolean
        Get
            Return Me.Tool = Tool.Form
        End Get
        Set(value As Boolean)
            If value Then Me.Tool = Tool.Form
        End Set
    End Property

    Public Property IsTextSelected As Boolean
        Get
            Return _isTextSelected
        End Get
        Set(value As Boolean)
            _isTextSelected = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("IsTextSelected"))
        End Set
    End Property

    Public Property TotalPages As Integer
        Get
            Return _totalPages
        End Get
        Set(value As Integer)
            _totalPages = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("TotalPages"))
        End Set
    End Property

    Public Property CurrentPage As Integer
        Get
            Return _currentPage
        End Get
        Set(value As Integer)
            _currentPage = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("CurrentPage"))
        End Set
    End Property

    Public ReadOnly Property VisibleCurrentMatchIndex As Integer
        Get
            If Me.NumberOfMatches = 0 Then
                Return 0
            Else
                Return _currentMatchIndex + 1
            End If
        End Get
    End Property

    Public Property CurrentMatchIndex As Integer
        Get
            Return _currentMatchIndex
        End Get
        Set(value As Integer)
            _currentMatchIndex = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("CurrentMatchIndex"))
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("VisibleCurrentMatchIndex"))
        End Set
    End Property

    Public Property NumberOfMatches As Integer
        Get
            Return _numberOfMatches
        End Get
        Set(value As Integer)
            _numberOfMatches = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("NumberOfMatches"))
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("VisibleCurrentMatchIndex"))
        End Set
    End Property
End Class
