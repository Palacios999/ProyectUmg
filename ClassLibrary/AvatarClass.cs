using static System.Console;

namespace ClassLibrary
{
    public class Avatar
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Level { get; set; }
        private int TotalCrystals { get; set; }
        private int TotalLifeJewelry { get; set; }
        private int TotalPoints { get; set; }
        private int CurrentCoordinateX { get; set; }
        private int CurrentCoordinateY { get; set; }

        public Avatar(string name, string gender) {
            Name = name;
            Gender = gender;
            Level = 2;
            TotalPoints = 0;
            TotalCrystals = 0;
            TotalLifeJewelry = 3;
            CurrentCoordinateX = 0;
            CurrentCoordinateY = 0;
        }

        public int GetTotalCrystals()
        {
            return this.TotalCrystals;
        }

        public void SetTotalCrystals(int crystals)
        {
            this.TotalCrystals += crystals;
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

        public (int,int) GetCurrentCoordinates()
        {
            return (this.CurrentCoordinateX, this.CurrentCoordinateY);
        }

        public void UpdateCoordinate(int coordinateX, int coordinateY)
        {
            this.CurrentCoordinateX = coordinateX;
            this.CurrentCoordinateY = coordinateY;
        }
    }
}
