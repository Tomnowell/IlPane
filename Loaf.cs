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

        private float totalWeight = 0.0F;
        private float flourWeight = 0.0F;
        private float waterWeight = 0.0F;
        private float saltWeight = 0.0F;
        private float otherDryWeight = 0.0F;
        private float otherWetWeight = 0.0F;
        private float bakerPercent = 0.0F;
        private float ratio = 0.0F;
        private float saltPercent = 0.0F;
        private float otherDryPercent = 0.0F;


        private float totalDryWeight = 0.0F;
        private float totalWetWeight = 0.0F;


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
            if (this.FlourWeight > 0.00 && this.WaterWeight > 0.00)
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
