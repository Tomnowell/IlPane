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
        private void Calculate (object sender, RoutedEventArgs e)
        { 
            Loaf currentLoaf = new Loaf(RecipeName.Text, ValidateFloat(FlourWeight.Text),
                ValidateFloat(TotalWeight.Text), ValidateFloat(WaterWeight.Text),
                ValidateFloat(SaltWeight.Text), ValidateFloat(OtherDryWeight.Text),
                ValidateFloat(OtherWetWeight.Text), ValidateFloat(LoafRatio.Text),
                ValidateFloat(SaltPercent.Text), ValidateFloat(OtherDryPercent.Text), Notes.Text);
        }

        private float ValidateFloat(string input)
        {
            
            try
            {
                float temp = (float)Convert.ToDouble(input);
                return temp;
            }
            catch (FormatException)
            {
                    return 0.00F;
            }
        }
    }
}
