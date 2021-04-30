using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
            DataAccess.AddData(RecipeName.Text);

            Output.ItemsSource = DataAccess.GetData(0);
        }
        private void Calculate(object sender, RoutedEventArgs e)
        {
            Loaf currentLoaf = new Loaf(RecipeName.Text, Convert.ToSingle(FlourWeight.Text), 
                Convert.ToSingle(TotalWeight.Text), Convert.ToSingle(WaterWeight.Text),
                Convert.ToSingle(SaltWeight.Text), Convert.ToSingle(OtherDryWeight.Text),
                Convert.ToSingle(OtherWetWeight.Text), Convert.ToSingle(LoafRatio.Text),
                Convert.ToSingle(SaltPercent.Text), Convert.ToSingle(OtherDryPercent.Text), Notes.Text);
        }
    }
}
