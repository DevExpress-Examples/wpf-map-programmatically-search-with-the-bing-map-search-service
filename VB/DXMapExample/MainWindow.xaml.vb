Imports DevExpress.Xpf.Map
Imports System
Imports System.Text
Imports System.Windows

Namespace DXMapExample
    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Partial Public Class MainWindow
        Inherits Window

        Public Sub New()
            InitializeComponent()
        End Sub

        #Region "#Search_Click"
        Private keywords As String
        Private location As String
        Private startingIndex As Integer
        Private longitude As Double
        Private latitude As Double

        Private Sub Search_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            GetSearchArguments()
            Search()
        End Sub
        #Region "#Search"
        Private Sub Search()
            searchDataProvider.Search(keywords, location, New GeoPoint(latitude, longitude), startingIndex)
        End Sub
        #End Region ' #Search

        Private Sub GetSearchArguments()
            keywords = tbKeywords.Text
            location = tbLocation.Text
            startingIndex = If(String.IsNullOrEmpty(tbStartingIndex.Text), 0, Int32.Parse(tbStartingIndex.Text))
            longitude = If(String.IsNullOrEmpty(tbLongitude.Text), 0, Double.Parse(tbLongitude.Text))
            latitude = If(String.IsNullOrEmpty(tbLatitude.Text), 0, Double.Parse(tbLatitude.Text))
        End Sub
        #End Region ' #Search_Click

        #Region "#SearchCompleted_Implementation"
        Private Sub searchDataProvider_SearchCompleted(ByVal sender As Object, ByVal e As BingSearchCompletedEventArgs)
            If e.Error IsNot Nothing OrElse e.Cancelled Then
                Return
            End If

            Dim result As SearchRequestResult = e.RequestResult
            If result.ResultCode = RequestResultCode.Success Then
                Dim region As LocationInformation = result.SearchRegion

                If region IsNot Nothing Then
                    NavigateTo(region.Location)

                Else
                    If result.SearchResults.Count > 0 Then
                        NavigateTo(result.SearchResults(0).Location)
                    End If
                End If
                DisplayResults(e.RequestResult)
            End If
        End Sub

        Private Sub NavigateTo(ByVal location As GeoPoint)
            mapControl.CenterPoint = location
            mapControl.ZoomLevel = 8
        End Sub
        #End Region ' #SearchCompleted_Implementation

        #Region "#DisplayResults"
        Private Sub DisplayResults(ByVal requestResult As SearchRequestResult)
            Dim resultList As New StringBuilder("")
            resultList.Append(String.Format("Result Code: {0}" & ControlChars.Lf, requestResult.ResultCode))
            resultList.Append(String.Format("Fault Reason: {0}" & ControlChars.Lf, requestResult.FaultReason))
            resultList.Append(String.Format("Estimated Matches: {0}" & ControlChars.Lf, requestResult.EstimatedMatches))
            resultList.Append(String.Format("Keyword: {0}" & ControlChars.Lf, requestResult.Keyword))
            resultList.Append(String.Format("Location: {0}" & ControlChars.Lf, requestResult.Location))
            resultList.Append(String.Format(ControlChars.Lf))

            If requestResult.ResultCode = RequestResultCode.Success Then
                Dim resCounter As Integer = 1
                For Each resultInfo As LocationInformation In requestResult.SearchResults
                    resultList.Append(String.Format("Result {0}:" & ControlChars.Lf, resCounter))
                    resultList.Append(String.Format("Display Name: {0}" & ControlChars.Lf, resultInfo.DisplayName))
                    resultList.Append(String.Format("Entity Type: {0}" & ControlChars.Lf, resultInfo.EntityType))
                    resultList.Append(String.Format("Address: {0}" & ControlChars.Lf, resultInfo.Address))
                    resultList.Append(String.Format("Location: {0}" & ControlChars.Lf, resultInfo.Location))
                    resultList.Append(String.Format("______________________________" & ControlChars.Lf))

                    resCounter += 1
                Next resultInfo
                If requestResult.SearchRegion IsNot Nothing Then
                    resultList.Append(String.Format(ControlChars.Lf & "===================================" & ControlChars.Lf))
                    resultList.Append(String.Format("Search region:" & ControlChars.Lf))
                    resultList.Append(String.Format("Display Name: {0}" & ControlChars.Lf, requestResult.SearchRegion.DisplayName))
                    resultList.Append(String.Format("Entity Type: {0}" & ControlChars.Lf, requestResult.SearchRegion.EntityType))
                    resultList.Append(String.Format("Address: {0}" & ControlChars.Lf, requestResult.SearchRegion.Address))
                    resultList.Append(String.Format("Location: {0}" & ControlChars.Lf, requestResult.SearchRegion.Location))
                End If
                resultList.Append(String.Format(ControlChars.Lf & "===================================" & ControlChars.Lf))
                resultList.Append(String.Format("Alternate search regions:" & ControlChars.Lf & ControlChars.Lf))
                resCounter = 1
                For Each locationInfo As LocationInformation In requestResult.AlternateSearchRegions
                    resultList.Append(String.Format("Region {0}:" & ControlChars.Lf, resCounter))
                    resultList.Append(String.Format("Display Name: {0}" & ControlChars.Lf, locationInfo.DisplayName))
                    resultList.Append(String.Format("Entity Type: {0}" & ControlChars.Lf, locationInfo.EntityType))
                    resultList.Append(String.Format("Address: {0}" & ControlChars.Lf, locationInfo.Address))
                    resultList.Append(String.Format("Location: {0}" & ControlChars.Lf, locationInfo.Location))
                    resultList.Append(String.Format("______________________________" & ControlChars.Lf))
                    resCounter += 1
                Next locationInfo
            End If

            tbResults.Text = resultList.ToString()
        End Sub
        #End Region ' #DisplayResults
    End Class
End Namespace
