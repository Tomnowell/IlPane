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

            // Display current database
            Output.ItemsSource = DataAccess.GetData();
        }

        private void AddData(object sender, RoutedEventArgs e)
        {
           //When Save is clicked: Add data and refresh.

            DataAccess.AddData(CreateCurrentLoaf());

            Output.ItemsSource = DataAccess.GetData();
        }

        private Loaf CreateCurrentLoaf()
        {
            // Process the UI inputs into Loaf object

            Loaf currentLoaf = new Loaf(RecipeName.Text, ValidateFloat(FlourWeight.Text),
                ValidateFloat(TotalWeight.Text), ValidateFloat(WaterWeight.Text),
                ValidateFloat(SaltWeight.Text), ValidateFloat(OtherDryWeight.Text),
                ValidateFloat(OtherWetWeight.Text), ValidateFloat(Ratio.Text),
                ValidateFloat(SaltPercent.Text), ValidateFloat(OtherDryPercent.Text), Notes.Text);
            return currentLoaf;
        }
        private void Calculate (object sender, RoutedEventArgs e)
        { 
            // Refresh the UI
            DisplayLoaf(CreateCurrentLoaf());
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
            SaltPercent.Text = string.Format("{0:N2}", Convert.ToString(currentLoaf.SaltPercent));
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
                // Warning, this may have some unintended consequences :S
                return 0.00F;
            }
        }

    }
}
