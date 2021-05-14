using System;
using Windows.UI.Popups;

namespace Pane
{
    public class Loaf
    {
        // Variables for name and database key
        private string recipeName;
        private int key;

        // Ingredient variables
        private float flourWeight;
        private float waterWeight;
        private float saltWeight;
        private float otherDryWeight;
        private float otherWetWeight;
        private float ratio;
        private float saltPercent;
        private float otherDryPercent;

        // Totals variables
        private float totalWeight;
        private float totalDryWeight;
        private float totalWetWeight;

        // Keep track of notes:
        private string notes = "";

        //Track priority of calculation (to prioritize ratio or weights)
        private bool _calculateByRatio;

        public float FlourWeight { get => flourWeight; set => flourWeight = value; }
        public  float TotalWeight { get => totalWeight; set => totalWeight = value; }
        public float WaterWeight { get => waterWeight; set => waterWeight = value; }
        public float SaltWeight { get => saltWeight; set => saltWeight = value; }
        public float OtherDryWeight { get => otherDryWeight; set => otherDryWeight = value; }
        public float OtherWetWeight { get => otherWetWeight; set => otherWetWeight = value; }
        public float Ratio { get => ratio; set => ratio = value; }
        public float SaltPercent { get => saltPercent; set => saltPercent = value; }
        public float OtherDryPercent { get => otherDryPercent; set => otherDryPercent = value; }
        public string Notes { get => notes; set => notes = value; }
        public float TotalDryWeight { get => totalDryWeight; set => totalDryWeight = value; }
        public float TotalWetWeight { get => totalWetWeight; set => totalWetWeight = value; }
        public string RecipeName { get => recipeName; set => recipeName = value; }
        public int Key { get => key; set => key = value; }
        public bool CalculateByRatio { get => _calculateByRatio; set => _calculateByRatio = value; }

        // Basic constructor 
        public Loaf() 
        {
            //Set empty loaf
            Key = -1;
            RecipeName = "None";
            TotalWeight = 0.00F;
            FlourWeight = 0.00F;
            WaterWeight = 0.00F;
            SaltWeight = 0.00F;
            OtherDryWeight = 0.00F;
            OtherWetWeight = 0.00F;
            Ratio = 0.00F;
            SaltPercent = 0.00F;
            OtherDryPercent = 0.00F;
            notes = "";
            CalculateByRatio = true;
            
        } 

        // Normal constructor
        public Loaf(string recipeName, float flourWeight, float totalWeight, float waterWeight,
            float saltWeight, float otherDryWeight, float otherWetWeight, float ratio,
            float saltPercent, float otherDryPercent, string notes, bool calculateByRatio)
        {

            if (Key < 0)
            {
                // This only gets set when added to the database.
                Key = -1;
            }
            RecipeName = recipeName;
            FlourWeight = flourWeight;
            TotalWeight = totalWeight;
            WaterWeight = waterWeight;
            SaltWeight = saltWeight;
            OtherDryWeight = otherDryWeight;
            OtherWetWeight = otherWetWeight;
            Ratio = ratio;
            SaltPercent = saltPercent;
            OtherDryPercent = otherDryPercent;
            Notes = notes;
            CalculateByRatio = calculateByRatio;

            this.InitializeLoaf();
        }

        //Complete constructor
        public Loaf(int key, string recipeName, float flourWeight, float totalWeight, float waterWeight,
            float saltWeight, float otherDryWeight, float otherWetWeight, float ratio, float saltPercent, 
            float otherDryPercent, string notes, bool calculateByRatio)
        {

            if (Key < 0)
            {
                // This only gets set when added to the database.
                Key = -1;
            }
            else
            {
                // Key is fetched from db
                Key = key;
            }
            RecipeName = recipeName;
            FlourWeight = flourWeight;
            TotalWeight = totalWeight;
            WaterWeight = waterWeight;
            SaltWeight = saltWeight;
            OtherDryWeight = otherDryWeight;
            OtherWetWeight = otherWetWeight;
            Ratio = ratio;
            SaltPercent = saltPercent;
            OtherDryPercent = otherDryPercent;
            Notes = notes;
            CalculateByRatio = calculateByRatio;

            this.InitializeLoaf();
        }


    public void InitializeLoaf()
    {
        // Check priority of calculation
        // Prioritise  ratio over weight
        if (CalculateByRatio == true)
        {
            if (this.IsValidRatios())
            {
                CalculateWeightsFromRatios();
            }
            else if (this.IsValidWeights())
            {
                CalculateRatiosFromWeights();
            }
            else
            {
                var messageDialog = new MessageDialog("Invalid weights or ratios.");
            }
        }
        // Prioritise weight over ratio
        else
        {
            if (this.IsValidWeights())
            {
                CalculateRatiosFromWeights();
            }
            else if (this.IsValidRatios())
            {
                CalculateWeightsFromRatios();
            }
            else
            {
                //var messageDialog = new MessageDialog("Invalid weights or ratios.");
            }
        }

    }
    public void CalculateDryWeight()
    {
        this.TotalDryWeight = FlourWeight + SaltWeight + OtherDryWeight;
    }

    public void CalculateWetWeight()
    {
        this.TotalWetWeight = WaterWeight + OtherWetWeight;
    }

    public void CalculateTotalWeight()
    {
        this.CalculateDryWeight();
        this.CalculateWetWeight();
        this.TotalWeight = TotalDryWeight + TotalWetWeight;
    }

    public void CalculateRatiosFromWeights()
    {
        // Calculate values from weights

        this.CalculateTotalWeight();

        //** Baking note **
        // Bakers measure hydration as a ratio of dry to wet ingredients
        // Usually flour & salt to water
        this.Ratio = (this.FlourWeight * 100) / this.TotalWeight;

        //If salt weight is set
        if (this.SaltWeight > 0)
        {
            this.SaltPercent = (this.SaltWeight / this.FlourWeight) * 100;
        }
        else if (this.SaltPercent > 0)
        {
            this.SaltWeight = this.FlourWeight * (this.SaltPercent / 100);
        }

    }
    public void CalculateWeightsFromRatios()
    {
        if (this.TotalWeight == 0 || this.TotalWeight < this.WaterWeight)
        {
            //Error weights not set!
            throw new Exception();
        }
        if (saltPercent > 0)
        {
            // Calculate salt by ratio (ratio has been set)
            this.FlourWeight = this.TotalWeight * (this.Ratio / 100);
            this.SaltWeight = this.FlourWeight * (this.SaltPercent / 100);
            if (this.OtherDryPercent > 0)
            {
                this.OtherDryWeight = this.FlourWeight / (this.OtherDryPercent / 100);
            }
            this.TotalDryWeight = this.FlourWeight + this.SaltWeight + this.OtherDryWeight;
            this.TotalWetWeight = this.TotalWeight - this.TotalDryWeight;
            this.WaterWeight = this.totalWetWeight - this.OtherWetWeight;
        }
        else
        {
            // Calculate salt by weight (ratio not set)
            this.FlourWeight = this.TotalWeight * (this.Ratio / 100);

            this.TotalDryWeight = this.FlourWeight + this.SaltWeight + this.OtherDryWeight;

            this.TotalWetWeight = this.TotalWeight - this.TotalDryWeight;
            this.WaterWeight = this.totalWetWeight - this.OtherWetWeight;

            this.SaltPercent = (this.SaltWeight / this.FlourWeight) * 100;
        }

    }

    public bool IsValidWeights()
    {
        //Check if weights can calculate a ratio.  Consider using exceptions to 
        //return more specific information if values are invalid
        if (this.FlourWeight > 0.00 &&
            (this.WaterWeight > 0.00 || (this.ratio > 0 && this.ratio < 100)))
        {
            return true;
        }
        else if (this.FlourWeight > 0.00 && this.TotalWeight > this.FlourWeight)
        {
            return true;
        }
        else return false;
    }

    public bool IsValidRatios()
    {
        //Check if given values can calculate weights.  Consider using exceptions to 
        //return more specific information if values are invalid
        //
        if ((this.TotalWeight > 0 || this.FlourWeight > 0) && (this.Ratio > 0.00 && this.Ratio < 100))
        {
            return true;
        }
        else return false;
    }
}
