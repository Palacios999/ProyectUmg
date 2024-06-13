using static System.Console;

namespace ClassLibrary
{
    public class Avatar
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Level { get; set; }
        private int CurrentCoordinateX { get; set; }
        private int CurrentCoordinateY { get; set; }

        public Avatar(string name, string gender) {
            Name = name;
            Gender = gender;
            Level = 5;
            CurrentCoordinateX = 0;
            CurrentCoordinateY = 0;
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
