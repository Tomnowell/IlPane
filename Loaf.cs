using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pane
{
    public class Loaf
    {
        public Loaf() { }

        public Loaf(string recipeName, float flourWeight, float totalWeight, float waterWeight, 
            float saltWeight, float otherDryWeight, float otherWetWeight, float ratio, 
            float saltPercent, float otherDryPercent, string notes)
        {
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
            

            if(this.IsValidWeights())
            {
                CalculateRatiosFromWeights();
            }
            else if (this.IsValidRatios())
            {
                CalculateRatiosFromWeights();
            }
            else throw new ArgumentException("Parameters are invalid", this.RecipeName);
        }

        private string recipeName;

        private float totalWeight;
        private float flourWeight;
        private float waterWeight;
        private float saltWeight;
        private float otherDryWeight;
        private float otherWetWeight;
        private float bakerPercent;
        private float ratio;
        private float saltPercent;
        private float otherDryPercent;


        private float totalDryWeight;
        private float totalWetWeight;


        private string notes = "";

        public float FlourWeight { get => flourWeight; set => flourWeight = value; }
        public  float TotalWeight { get => totalWeight; set => totalWeight = value; }
        public float WaterWeight { get => waterWeight; set => waterWeight = value; }
        public float SaltWeight { get => saltWeight; set => saltWeight = value; }
        public float OtherDryWeight { get => otherDryWeight; set => otherDryWeight = value; }
        public float OtherWetWeight { get => otherWetWeight; set => otherWetWeight = value; }
        public float BakerPercent { get => bakerPercent; set => bakerPercent = value; }
        public float Ratio { get => ratio; set => ratio = value; }
        public float SaltPercent { get => saltPercent; set => saltPercent = value; }
        public float OtherDryPercent { get => otherDryPercent; set => otherDryPercent = value; }
        public string Notes { get => notes; set => notes = value; }
        public float TotalDryWeight { get => totalDryWeight; set => totalDryWeight = value; }
        public float TotalWetWeight { get => totalWetWeight; set => totalWetWeight = value; }
        public string RecipeName { get => recipeName; set => recipeName = value; }

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
            if (this.IsValidWeights())
            {
                // Calculate values
                this.CalculateTotalWeight();

                //** Baking tip **
                // Bakers measure hydration as a ratio of dry to wet ingredients
                // Usually flour & salt to water
                this.ratio = 100 * TotalWetWeight / TotalDryWeight;

                //** Baking tip **
                // Bakers measure salt as a percentage of the flour weight, not total weight
                if (SaltWeight > 0) { this.saltPercent = 100 * SaltWeight / FlourWeight; }
            }
            else
            {
                // INVALID WEIGHTS
            }

        }
        public void CalculateWeightsFromRatios()
        {
            if (this.IsValidRatios())
            {

                // Calculate ratios
                if (saltPercent > 0)
                {
                    // Calculate salt by ratio (ratio has been set)
                    this.BakerPercent = 100 + this.Ratio + this.SaltPercent + this.OtherDryPercent;
                    this.TotalDryWeight = this.TotalWeight / (this.Ratio / 100);
                    this.TotalWetWeight = this.Ratio / 100 * this.TotalDryWeight;
                }
                else
                {
                    // Calculate salt by weight (ratio not set)
                    this.BakerPercent = 100 + this.Ratio + this.OtherDryPercent;
                    this.TotalDryWeight = this.TotalWeight / (this.Ratio / 100);
                    this.TotalWetWeight = this.Ratio / 100 * this.TotalDryWeight;
                    this.SaltPercent = this.SaltWeight / this.totalDryWeight * 100;
                }

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
            else return false;
        }

        public bool IsValidRatios()
        {
            //Check if given values can calculate weights.  Consider using exceptions to 
            //return more specific information if values are invalid
            //
            if (this.TotalWeight > 0 && (this.Ratio > 0.00 && this.Ratio < 100))
            {
                return true;
            }
            else return false;
        }

    }
}
