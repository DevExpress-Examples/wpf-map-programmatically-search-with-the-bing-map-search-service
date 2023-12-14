Imports DevExpress.Xpf.Map
Imports System.Text
Imports System.Windows

Namespace DXMapExample

    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
        End Sub

'#Region "#Search_Click"
        Private Sub Search_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.searchDataProvider.Search(Me.teKeywords.Text)
        End Sub

'#End Region  ' #Search_Click
'#Region "#SearchCompleted_Implementation"
        Private Sub OnSearchCompleted(ByVal sender As Object, ByVal e As BingSearchCompletedEventArgs)
            If e.Cancelled Then Return
            If e.RequestResult.ResultCode <> RequestResultCode.Success Then
                Me.teResult.Text = "The Bing Search service does not work for this location."
                Return
            End If

            Dim resultList As StringBuilder = New StringBuilder("")
            Dim resCounter As Integer = 1
            For Each resultInfo As LocationInformation In e.RequestResult.SearchResults
                resultList.Append(String.Format("Result {0}:  " & Microsoft.VisualBasic.Constants.vbCrLf, resCounter))
                resultList.Append(String.Format("Name: {0}" & Microsoft.VisualBasic.Constants.vbCrLf, resultInfo.DisplayName))
                resultList.Append(String.Format("Address: {0}" & Microsoft.VisualBasic.Constants.vbCrLf, resultInfo.Address.FormattedAddress))
                resultList.Append(String.Format("Geographic coordinates:  {0}" & Microsoft.VisualBasic.Constants.vbCrLf, resultInfo.Location))
                resultList.Append(String.Format("______________________________" & Microsoft.VisualBasic.Constants.vbCrLf))
                resCounter += 1
            Next

            Me.teResult.Text = resultList.ToString()
        End Sub

'#End Region  ' #SearchCompleted_Implementation
        Private Sub OnLayerItemsGenerating(ByVal sender As Object, ByVal args As LayerItemsGeneratingEventArgs)
            Me.mapControl.ZoomToFit(args.Items, 0.4)
        End Sub
    End Class
End Namespace
