using DevExpress.Xpf.Map;
using System;
using System.Text;
using System.Windows;

namespace DXMapExample {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        #region #Search_Click
        private void Search_Click(object sender, RoutedEventArgs e) {
            searchDataProvider.Search(teKeywords.Text);
        }
        #endregion #Search_Click

        #region #SearchCompleted_Implementation
        private void OnSearchCompleted(object sender, BingSearchCompletedEventArgs e) {
            if(e.Cancelled) return;
            if(e.RequestResult.ResultCode != RequestResultCode.Success) {
                teResult.Text = "The Bing Search service does not work for this location.";
                return;
            }

            StringBuilder resultList = new StringBuilder("");
            int resCounter = 1;
            foreach(LocationInformation resultInfo in e.RequestResult.SearchResults) {
                resultList.Append(String.Format("Result {0}:  \r\n", resCounter));
                resultList.Append(String.Format("Name: {0}\r\n", resultInfo.DisplayName));
                resultList.Append(String.Format("Address: {0}\r\n", resultInfo.Address.FormattedAddress));
                resultList.Append(String.Format("Geographic coordinates:  {0}\r\n", resultInfo.Location));
                resultList.Append(String.Format("______________________________\r\n"));
                resCounter++;
            }
            teResult.Text = resultList.ToString();
        }
        #endregion #SearchCompleted_Implementation

        private void OnLayerItemsGenerating(object sender, LayerItemsGeneratingEventArgs args) {
            mapControl.ZoomToFit(args.Items, 0.4);
        }
    }
}
