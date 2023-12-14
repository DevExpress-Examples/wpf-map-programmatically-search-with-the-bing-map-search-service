Imports DevExpress.Xpf.Map
Imports System.Text
Imports System.Windows

Namespace DXMapExample

    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
        End Sub

'#Region "#Search_Click"
        Private keywords As String

        Private location As String

        Private startingIndex As Integer

        Private longitude As Double

        Private latitude As Double

        Private Sub Search_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            GetSearchArguments()
            Search()
        End Sub

'#Region "#Search"
        Private Sub Search()
            Me.searchDataProvider.Search(keywords, location, New GeoPoint(latitude, longitude), startingIndex)
        End Sub

'#End Region  ' #Search
        Private Sub GetSearchArguments()
            keywords = Me.tbKeywords.Text
            location = Me.tbLocation.Text
            startingIndex = If(String.IsNullOrEmpty(Me.tbStartingIndex.Text), 0, Integer.Parse(Me.tbStartingIndex.Text))
            longitude = If(String.IsNullOrEmpty(Me.tbLongitude.Text), 0, Double.Parse(Me.tbLongitude.Text))
            latitude = If(String.IsNullOrEmpty(Me.tbLatitude.Text), 0, Double.Parse(Me.tbLatitude.Text))
        End Sub

'#End Region  ' #Search_Click
'#Region "#SearchCompleted_Implementation"
        Private Sub searchDataProvider_SearchCompleted(ByVal sender As Object, ByVal e As BingSearchCompletedEventArgs)
            If e.Error IsNot Nothing OrElse e.Cancelled Then Return
            Dim result As SearchRequestResult = e.RequestResult
            If result.ResultCode = RequestResultCode.Success Then
                Dim region As LocationInformation = result.SearchRegion
                If region IsNot Nothing Then
                    NavigateTo(region.Location)
                Else
                    If result.SearchResults.Count > 0 Then NavigateTo(result.SearchResults(0).Location)
                End If

                DisplayResults(e.RequestResult)
            End If
        End Sub

        Private Sub NavigateTo(ByVal location As GeoPoint)
            Me.mapControl.CenterPoint = location
            Me.mapControl.ZoomLevel = 8
        End Sub

'#End Region  ' #SearchCompleted_Implementation
'#Region "#DisplayResults"
        Private Sub DisplayResults(ByVal requestResult As SearchRequestResult)
            Dim resultList As StringBuilder = New StringBuilder("")
            resultList.Append(String.Format("Result Code: {0}" & Microsoft.VisualBasic.Constants.vbLf, requestResult.ResultCode))
            resultList.Append(String.Format("Fault Reason: {0}" & Microsoft.VisualBasic.Constants.vbLf, requestResult.FaultReason))
            resultList.Append(String.Format("Estimated Matches: {0}" & Microsoft.VisualBasic.Constants.vbLf, requestResult.EstimatedMatches))
            resultList.Append(String.Format("Keyword: {0}" & Microsoft.VisualBasic.Constants.vbLf, requestResult.Keyword))
            resultList.Append(String.Format("Location: {0}" & Microsoft.VisualBasic.Constants.vbLf, requestResult.Location))
            resultList.Append(String.Format(Microsoft.VisualBasic.Constants.vbLf))
            If requestResult.ResultCode = RequestResultCode.Success Then
                Dim resCounter As Integer = 1
                For Each resultInfo As LocationInformation In requestResult.SearchResults
                    resultList.Append(String.Format("Result {0}:" & Microsoft.VisualBasic.Constants.vbLf, resCounter))
                    resultList.Append(String.Format("Display Name: {0}" & Microsoft.VisualBasic.Constants.vbLf, resultInfo.DisplayName))
                    resultList.Append(String.Format("Entity Type: {0}" & Microsoft.VisualBasic.Constants.vbLf, resultInfo.EntityType))
                    resultList.Append(String.Format("Address: {0}" & Microsoft.VisualBasic.Constants.vbLf, resultInfo.Address))
                    resultList.Append(String.Format("Location: {0}" & Microsoft.VisualBasic.Constants.vbLf, resultInfo.Location))
                    resultList.Append(String.Format("______________________________" & Microsoft.VisualBasic.Constants.vbLf))
                    resCounter += 1
                Next

                If requestResult.SearchRegion IsNot Nothing Then
                    resultList.Append(String.Format(Microsoft.VisualBasic.Constants.vbLf & "===================================" & Microsoft.VisualBasic.Constants.vbLf))
                    resultList.Append(String.Format("Search region:" & Microsoft.VisualBasic.Constants.vbLf))
                    resultList.Append(String.Format("Display Name: {0}" & Microsoft.VisualBasic.Constants.vbLf, requestResult.SearchRegion.DisplayName))
                    resultList.Append(String.Format("Entity Type: {0}" & Microsoft.VisualBasic.Constants.vbLf, requestResult.SearchRegion.EntityType))
                    resultList.Append(String.Format("Address: {0}" & Microsoft.VisualBasic.Constants.vbLf, requestResult.SearchRegion.Address))
                    resultList.Append(String.Format("Location: {0}" & Microsoft.VisualBasic.Constants.vbLf, requestResult.SearchRegion.Location))
                End If

                resultList.Append(String.Format(Microsoft.VisualBasic.Constants.vbLf & "===================================" & Microsoft.VisualBasic.Constants.vbLf))
                resultList.Append(String.Format("Alternate search regions:" & Microsoft.VisualBasic.Constants.vbLf & Microsoft.VisualBasic.Constants.vbLf))
                resCounter = 1
                For Each locationInfo As LocationInformation In requestResult.AlternateSearchRegions
                    resultList.Append(String.Format("Region {0}:" & Microsoft.VisualBasic.Constants.vbLf, resCounter))
                    resultList.Append(String.Format("Display Name: {0}" & Microsoft.VisualBasic.Constants.vbLf, locationInfo.DisplayName))
                    resultList.Append(String.Format("Entity Type: {0}" & Microsoft.VisualBasic.Constants.vbLf, locationInfo.EntityType))
                    resultList.Append(String.Format("Address: {0}" & Microsoft.VisualBasic.Constants.vbLf, locationInfo.Address))
                    resultList.Append(String.Format("Location: {0}" & Microsoft.VisualBasic.Constants.vbLf, locationInfo.Location))
                    resultList.Append(String.Format("______________________________" & Microsoft.VisualBasic.Constants.vbLf))
                    resCounter += 1
                Next
            End If

            Me.tbResults.Text = resultList.ToString()
        End Sub
'#End Region  ' #DisplayResults
    End Class
End Namespace
