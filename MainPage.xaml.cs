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
                ValidateFloat(OtherWetWeight.Text), ValidateFloat(Ratio.Text),
                ValidateFloat(SaltPercent.Text), ValidateFloat(OtherDryPercent.Text), Notes.Text);
            currentLoaf.InitializeLoaf();
            DisplayLoaf(currentLoaf);
        }

        private void DisplayLoaf(Loaf currentLoaf)
        {
            // Update UI textboxes with the values in currentLoaf

            RecipeName.Text = currentLoaf.RecipeName;
            FlourWeight.Text = string.Format("{0:N2}", Convert.ToString(currentLoaf.FlourWeight));
            TotalWeight.Text = string.Format("{0:N2}", Convert.ToString(currentLoaf.TotalWeight));
            WaterWeight.Text = string.Format("{0:N2}", Convert.ToString(currentLoaf.WaterWeight));
            SaltWeight.Text = string.Format("{0:N2}", Convert.ToString(currentLoaf.SaltWeight));
            OtherDryWeight.Text = string.Format("{0:N2}", Convert.ToString(currentLoaf.OtherDryWeight));
            OtherWetWeight.Text = string.Format("{0:N2}", Convert.ToString(currentLoaf.OtherWetWeight));
            Ratio.Text = string.Format("{0:N2}", Convert.ToString(currentLoaf.Ratio));
            OtherDryPercent.Text = string.Format("{0:N2}", Convert.ToString(currentLoaf.OtherDryPercent));
            Notes.Text = currentLoaf.Notes;
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
