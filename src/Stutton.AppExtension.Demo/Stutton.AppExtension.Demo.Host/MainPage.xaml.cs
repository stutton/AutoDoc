using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Stutton.AppExtension.Demo.Shared;
using Stutton.AppExtensionHoster;
using Stutton.AppExtensionHoster.Uwp;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Stutton.AppExtension.Demo.Host
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<Extension<AppExtensionMessage, AppExtensionResponse>> Items = null;

        public ObservableCollection<double> Parameters = new ObservableCollection<double>();

        public MainPage()
        {
            InitializeComponent();
            var dispatcherWrapper =
                new CoreDispatcherWrapper(Windows.UI.Core.CoreWindow.GetForCurrentThread().Dispatcher);
            AppData.ExtensionManager.InitializeAsync(dispatcherWrapper);

            Items = AppData.ExtensionManager.Extensions;
            DataContext = this;

            Parameters.Add(5);
            Parameters.Add(5);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var cb = sender as CheckBox;
            if (cb?.DataContext is Extension<AppExtensionMessage, AppExtensionResponse> ex && ex.State == ExtensionState.Disabled)
            {
                ex.Enable();
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var cb = sender as CheckBox;
            if (cb?.DataContext is Extension<AppExtensionMessage, AppExtensionResponse> ex && ex.State == ExtensionState.Disabled)
            {
                ex.Disable();
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn?.DataContext is Extension<AppExtensionMessage, AppExtensionResponse> ex)
            {
                AppData.ExtensionManager.RemoveExtension(ex);
            }
        }

        private void AddParameterButton_OnClick(object sender, RoutedEventArgs e)
        {
            Parameters.Add(0);
        }

        private async void InvokeExtensionButton_OnClick(object sender, RoutedEventArgs e)
        {
            var message = new AppExtensionMessage
            {
                Parameters = Parameters.ToArray()
            };

            var button = sender as Button;
            if (button?.DataContext is Extension<AppExtensionMessage, AppExtensionResponse> ext)
            {
                var result = await ext.Invoke(message);
                if (result.Status == "success")
                {
                    ResultText.Foreground = new SolidColorBrush(Colors.Green);
                    ResultText.Text = result.Response.Result.ToString();
                }
                else
                {
                    ResultText.Foreground = new SolidColorBrush(Colors.Red);
                    ResultText.Text = $"{result.Status.ToUpper()}: {result.Message}";
                }
            }
        }
    }
}
