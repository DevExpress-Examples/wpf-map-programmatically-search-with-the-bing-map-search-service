<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128571768/25.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4238)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->

# Map for WPF - How to Search in Code with the Azure Map Search Service

This example demonstrates how to create a custom search panel that searches for location, keywords, and other parameters with the [AzureSearchDataProvider.Search](https://docs.devexpress.com/WPF/DevExpress.Xpf.Map.AzureSearchDataProvider.Search.overloads) method.

> **Note:**
> If you run this sample as is, you get a warning message saying that the specified Azure Maps key is invalid. To learn more about Azure Map keys, please refer following tutorial: [Migrate from Bing Maps to Azure Maps](https://docs.devexpress.com/WPF/405436/controls-and-libraries/map-control/migrate-from-bing-to-azure)

## Implementation Details

To use the Search panel, specify search parameters (location, keyword, start search index, geographical point coordinates) in the textbox elements.

When you handle the `search_Click` event, all parameters are passed to the `Search` method, and you can see the result in the text block element below.

The results contain a **display name**, **entity type**, and **address** associated with the search **location**. You can also see search request information returned by the [RequestResultBase.ResultCode](https://docs.devexpress.com/WPF/DevExpress.Xpf.Map.RequestResultBase.ResultCode), [RequestResultBase.FaultReason](https://docs.devexpress.com/WPF/DevExpress.Xpf.Map.RequestResultBase.FaultReason), and [SearchRequestResult.EstimatedMatches](https://docs.devexpress.com/WPF/DevExpress.Xpf.Map.SearchRequestResult.EstimatedMatches) properties.

## Files to Review

* [MainWindow.xaml](./CS/DXMapExample/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/DXMapExample/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/DXMapExample/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/DXMapExample/MainWindow.xaml.vb))

## Documentation

* [Search](https://docs.devexpress.com/WPF/17463/controls-and-libraries/map-control/gis-data/search)

<!-- feedback -->
## Does This Example Address Your Development Requirements/Objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=wpf-map-search-with-azure-map-search-service&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=wpf-map-search-with-azure-map-search-service&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
