Imports DevExpress.Xpf.Map
Imports System
Imports System.Text
Imports System.Windows

Namespace DXMapExample
    Partial Public Class MainWindow
        Inherits Window

        Public Sub New()
            InitializeComponent()
        End Sub

        #Region "#Search_Click"
        Private Sub Search_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            searchDataProvider.Search(teKeywords.Text)
        End Sub
        #End Region ' #Search_Click

        #Region "#SearchCompleted_Implementation"
        Private Sub OnSearchCompleted(ByVal sender As Object, ByVal e As BingSearchCompletedEventArgs)
            If e.Cancelled Then
                Return
            End If
            If e.RequestResult.ResultCode <> RequestResultCode.Success Then
                teResult.Text = "The Bing Search service does not work for this location."
                Return
            End If

            Dim resultList As New StringBuilder("")
            Dim resCounter As Integer = 1
            For Each resultInfo As LocationInformation In e.RequestResult.SearchResults
                resultList.Append(String.Format("Result {0}:  " & ControlChars.CrLf, resCounter))
                resultList.Append(String.Format("Name: {0}" & ControlChars.CrLf, resultInfo.DisplayName))
                resultList.Append(String.Format("Address: {0}" & ControlChars.CrLf, resultInfo.Address.FormattedAddress))
                resultList.Append(String.Format("Geographic coordinates:  {0}" & ControlChars.CrLf, resultInfo.Location))
                resultList.Append(String.Format("______________________________" & ControlChars.CrLf))
                resCounter += 1
            Next resultInfo
            teResult.Text = resultList.ToString()
        End Sub
        #End Region ' #SearchCompleted_Implementation

        Private Sub OnLayerItemsGenerating(ByVal sender As Object, ByVal args As LayerItemsGeneratingEventArgs)
            mapControl.ZoomToFit(args.Items, 0.4)
        End Sub
    End Class
End Namespace
