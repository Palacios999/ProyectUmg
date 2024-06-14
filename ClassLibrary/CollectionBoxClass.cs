using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class CollectionBox
    {
        public int TotalPoints { get; set; }
        public int TotalCrystals { get; set; }
        public int TotalLifeJewelry {  get; set; }

        public CollectionBox(int totalPoint, int totalCrystal, int totalLifeJewelry) {
            TotalPoints = totalPoint;
            TotalCrystals = totalCrystal;
            TotalLifeJewelry = totalLifeJewelry;
        }

        public int GetTotalCrystals()
        {
            return this.TotalCrystals;
        }

        public void SetTotalCrystals(int crystal)
        {
            this.TotalCrystals += crystal;
        }

        public int GetTotalLifeJewelry()
        {
            return this.TotalLifeJewelry;
        }

        public void SetTotalLifeJewelry(int jewely)
        {
            this.TotalLifeJewelry += jewely;
        }

        public int GetTotalPoints()
        {
            return this.TotalPoints;
        }

        public void SetTotalPoints(int points)
        {
            this.TotalPoints += points;
        }

        public void ResetCollectionBox()
        {
            this.TotalPoints = 0;
            this.TotalCrystals = 0;
            this.TotalLifeJewelry = 0;
        }

    }
}
