﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using DataAccessLibrary;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Pane
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();


        }

        private void AddData(object sender, RoutedEventArgs e)
        {
            DataAccess.AddData(Input_Box.Text);

            Output.ItemsSource = DataAccess.GetData();
        }
    }
}
