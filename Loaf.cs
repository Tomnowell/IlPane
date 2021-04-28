using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pane
{
    public class Loaf
    {
        float totalWeight = 0.00;
        float flourWeight = 0.00;
        float waterWeight = 0.00;
        float saltWeight = 0.00;
        float otherDryWeight = 0.00;
        float otherWetWeight = 0.00;
        float bakerPercent = 0.00;
        float ratio = 0.00;
        float hydration = 0;
        float saltPercent = 0;
        float otherPercent = 0;

        string notes = "";
        public float FlourWeight { get => flourWeight; set => flourWeight = value; }
        public float TotalWeight { get => totalWeight; set => totalWeight = value; }
        public float WaterWeight { get => waterWeight; set => waterWeight = value; }
        public float SaltWeight { get => saltWeight; set => saltWeight = value; }
        public float OtherDryWeight { get => otherDryWeight; set => otherDryWeight = value; }
        public float OtherWetWeight { get => otherWetWeight; set => otherWetWeight = value; }
        public float BakerPercent { get => bakerPercent; set => bakerPercent = value; }
        public float Ratio { get => ratio; set => ratio = value; }
        public float Hydration { get => hydration; set => hydration = value; }
        public float SaltPercent { get => saltPercent; set => saltPercent = value; }
        public float OtherPercent { get => otherPercent; set => otherPercent = value; }
        public string Notes { get => notes; set => notes = value; }

        public void calculateRatiosFromWeights()
        {
            if (this.isValidWeights())
            {
                // Calculate values
            }

        }
        public void calculateWeightsFromRatios()
        {
            if (this.isValidRatios())
            {
                // Calculate ratios
            }

        }

        public bool isValidWeights()
        {
            //Check if weights can calculate a ratio.  Consider using exceptions to 
            //return more specific information if values are invalid
            if (this.FlourWeight > 0.00 && this.WaterWeight > 0.00)
            {
                return true;
            }
            else return false;
        }

        public bool isValidRatios()
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
