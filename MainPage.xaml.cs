using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;


namespace Pane
{
    /// <summary>
    /// Main page
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

        private void LoadRecipe (object sender, RoutedEventArgs e)
        {
            //Get item selected in Listview
            if (Output.Items.Count < 1)
            {
                //There are no items to load
                DisplayFailure("No items to load, please create an item first.");
            }
            else
            {
                var name = Output.SelectedItem.ToString();
                if (name != null)
                {
                    //Look for that 'name' in the db
                    Loaf currentLoaf = DataAccess.GetRecipe(name);

                    //Display
                    DisplayLoaf(currentLoaf);
                }

                else
                {
                    // That item does not exist,
                    // Add a warning
                    //
                    // And send back the current Loaf again
                    DisplayFailure("That item does not exist in the database.");
                    Loaf currentLoaf = CreateCurrentLoaf();
                    DisplayLoaf(currentLoaf);

                }
            }
        }

        private void DeleteRecipe (object sender, RoutedEventArgs e)
        {
            // This is messy, refactor soon
            if (Output.Items.Count < 1)
            {
                //There are no items to delete
                DisplayFailure("No items to delete, please create an item first.");
            }
            else
            {
                if (Output.SelectedItems.Count > 0)
                {
                    var name = Output.SelectedItem.ToString();
                    if (name == null || name == "")
                    {
                        DisplayFailure("That item does not exist in the database.");
                    }
                    else
                    {
                        Loaf currentLoaf = DataAccess.GetRecipe(name);
                        if (currentLoaf != null && currentLoaf.RecipeName != "" && currentLoaf.RecipeName != null)
                        {

                            DataAccess.DeleteData(currentLoaf);

                            //Display
                            DisplayLoaf(currentLoaf);
                        }
                        else { DisplayFailure("Failed to make a Loaf object!"); }
                    }
                }
                else
                {
                    DisplayFailure("Please select an item to delete.");
                }
            }
        }

        private Loaf DisplayLoaf(Loaf currentLoaf)
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
            Output.ItemsSource = DataAccess.GetData();

            return currentLoaf;
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
       
        private async void DisplaySuccess(string content)
        {
            ContentDialog successDialog = new ContentDialog()
            {
                Title = "Success!",
                Content = content,
                CloseButtonText = "Ok"
            };

            await successDialog.ShowAsync();
        }
        private async void DisplayFailure(string content)
        {
            ContentDialog failureDialog = new ContentDialog()
            {
                Title = "There was an error",
                Content = content,
                CloseButtonText = "Ok"
            };

            await failureDialog.ShowAsync();
        }



    }
}
