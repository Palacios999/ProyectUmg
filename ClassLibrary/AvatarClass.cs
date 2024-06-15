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

        // Constructor del Avatar.
        public Avatar(string name, string gender) {
            Name = name;
            Gender = gender;
            Level = 1;
            CurrentCoordinateX = 0;
            CurrentCoordinateY = 0;
        }

        // Obtiene la coordenada del avatar
        public (int,int) GetCurrentCoordinates()
        {
            return (this.CurrentCoordinateX, this.CurrentCoordinateY);
        }

        // Actualizar coordenada del avatar
        public void UpdateCoordinate(int coordinateX, int coordinateY)
        {
            this.CurrentCoordinateX = coordinateX;
            this.CurrentCoordinateY = coordinateY;
        }

        // Limpiar datos del avatar
        public void ResetDataAvatar()
        {
            this.Level = 1;
            this.CurrentCoordinateX = 0;
            this.CurrentCoordinateY= 0;
        }
    }
}
