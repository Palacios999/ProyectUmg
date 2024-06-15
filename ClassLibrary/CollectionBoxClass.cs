using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class CollectionBox
    {
        // variables de la clase
        public int TotalPoints { get; set; }
        public int TotalCrystals { get; set; }
        public int TotalLifeJewelry {  get; set; }

        // Constructor de la caja recolectora.
        public CollectionBox(int totalPoint, int totalCrystal, int totalLifeJewelry) {
            TotalPoints = totalPoint;
            TotalCrystals = totalCrystal;
            TotalLifeJewelry = totalLifeJewelry;
        }

        // Obtiene la cantidad de cristales recolectados.
        public int GetTotalCrystals()
        {
            return this.TotalCrystals;
        }

        // Agrega cristales a la caja recolectora
        public void SetTotalCrystals(int crystal)
        {
            this.TotalCrystals += crystal;
        }

        // Obtien los cristales de vida
        public int GetTotalLifeJewelry()
        {
            return this.TotalLifeJewelry;
        }

        // Actualiza el total de cristales de vida
        public void SetTotalLifeJewelry(int jewely)
        {
            this.TotalLifeJewelry += jewely;
        }

        // Obtiene el total de puntos
        public int GetTotalPoints()
        {
            return this.TotalPoints;
        }

        // actualiza los datos de los puntos.
        public void SetTotalPoints(int points)
        {
            this.TotalPoints += points;
        }

        // Limpia los datos de la caja recolectora
        public void ResetCollectionBox()
        {
            this.TotalPoints = 0;
            this.TotalCrystals = 0;
            this.TotalLifeJewelry = 0;
        }

    }
}
