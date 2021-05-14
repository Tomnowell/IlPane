using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;



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

            this.InitializeUI();
        }

        private void InitializeUI()
        {
            // Display current recipes list
            this.Output.ItemsSource = DataAccess.GetRecipeListFromDatabase();

            //Display previous recipe
            DisplayLoaf(DataAccess.GetPreviousState());
        }
    
        private void AddData(object sender, RoutedEventArgs e)
        {
            //When Save is clicked: Add data and refresh.
            if (RecipeName.Text.Length != 0)
            {

                DataAccess.AddEntryToDatabase(CreateCurrentLoaf(), "recipeTable");
                Output.ItemsSource = DataAccess.GetRecipeListFromDatabase();
            }
            else
            {
                DisplayFailure("Please enter a name.");
            }
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
                if (Output.SelectedItems.Count > 0)
                {
                    var name = Output.SelectedItem.ToString();
                    if (name != null)
                    {
                        //Look for that  recipe's name in the db
                        Loaf currentLoaf = DataAccess.GetRecipeFromDatabaseByName(name);

                        DisplayLoaf(currentLoaf);
                    }

                    else
                    {                       
                        DisplayFailure("That item does not exist.");
                    }
                }
                else
                {
                    DisplayFailure("No items to load.");
                }
            }
        }

        private void DeleteRecipe (object sender, RoutedEventArgs e)
        {
            
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
                        Loaf currentLoaf = DataAccess.GetRecipeFromDatabaseByName(name);
                        if (currentLoaf != null && currentLoaf.RecipeName != "" && currentLoaf.RecipeName != null)
                        {

                            DataAccess.DeleteEntryFromDatabaseByName(currentLoaf, "recipeTable");

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
        public Loaf CreateCurrentLoaf(bool calculateByRatio = true)
        {
            // Process the UI inputs into Loaf object

            Loaf currentLoaf = new Loaf(RecipeName.Text, ValidateFloat(FlourWeight.Text),
                ValidateFloat(TotalWeight.Text), ValidateFloat(WaterWeight.Text),
                ValidateFloat(SaltWeight.Text), ValidateFloat(OtherDryWeight.Text),
                ValidateFloat(OtherWetWeight.Text), ValidateFloat(Ratio.Text),
                ValidateFloat(SaltPercent.Text), ValidateFloat(OtherDryPercent.Text), Notes.Text, calculateByRatio);
            return currentLoaf;
        }
        public Loaf DisplayLoaf(Loaf currentLoaf)
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
           
            // Refresh List
            Output.ItemsSource = DataAccess.GetRecipeListFromDatabase();

            // Keep track of what loaf is displayed
            // Keep persistence! 
            
            //DataAccess.SaveCurrentState();
            return currentLoaf;
        }

        private float ValidateFloat(string input)
        {
            
            try
            {
                float temp = (float)Convert.ToDouble(input);
                return temp;
            }
            catch (Exception)
            {
                // Warning, this may have some unintended consequences :S
                return 0.00F;
            }
        }

        private void ClearAll (object sender, RoutedEventArgs e)
        {
            // Clear all textboxes
            RecipeName.Text = "Recipe";
            FlourWeight.Text = "";
            TotalWeight.Text = "";
            WaterWeight.Text = "";
            SaltWeight.Text = "";
            OtherDryWeight.Text = "";
            OtherWetWeight.Text = "";
            Ratio.Text = "";
            SaltPercent.Text = "";
            OtherDryPercent.Text = "";
            Notes.Text = "";

            DisplayLoaf(CreateCurrentLoaf());
        }

        private void ClearWeights(object sender, RoutedEventArgs e)
        {
            // Reset all weights

            // This will not affect any saved recipes

            FlourWeight.Text = "";
            TotalWeight.Text = "";
            WaterWeight.Text = "";
            SaltWeight.Text = "";
            OtherDryWeight.Text = "";
            OtherWetWeight.Text = "";
            DisplayLoaf(CreateCurrentLoaf(false));
        }

        private void ClearRatios(object sender, RoutedEventArgs e)
        {
            // Just reset ratio textboxes

            // This will not affect any saved recipes
            Ratio.Text = "";
            SaltPercent.Text = "";
            OtherDryPercent.Text = "";

            DataAccess.SaveCurrentState(CreateCurrentLoaf());
        }

        private void Exit (object sender, RoutedEventArgs e)
        {
            DisplayExitDialog(RecipeName.Text);
        }

        private async void DisplayExitDialog(string recipeName)
        {
            string title = "Do you want to save your changes to " + recipeName + " ?";
            ContentDialog exitFileDialog = new ContentDialog
            {
                Title = title,
                Content = "If you quit without saving this recipe, any unsaved changes will be lost.  Do you want to save the current recipe?",
                PrimaryButtonText = "Save",
                CloseButtonText = "Exit"
            };

            ContentDialogResult result = await exitFileDialog.ShowAsync();
            DataAccess.SaveCurrentState(CreateCurrentLoaf());
            // Delete the file if the user clicked the primary button.
            /// Otherwise, do nothing.
            if (result == ContentDialogResult.Primary)
            {
                if (RecipeName.Text.Length != 0)
                {
                    // If there is a valid name for the recipe 

                    DataAccess.AddEntryToDatabase(CreateCurrentLoaf(),"recipeTable");
                    
                    DisplaySuccess("Recipe Saved");
                    Application.Current.Exit();
                }
                else
                {
                    DisplayFailure("Please enter a name.");
                }
            }
            else
            {
                // User clicked exit
                Application.Current.Exit();
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
