using CrimsofallTechnologies.VR.Inventory;
using UnityEngine;

namespace CrimsofallTechnologies.VR.DataSaving
{
    [System.Serializable]
    public class PlayerData
    {
        public Item[] inventoryData;
        public int points = 0;
        public bool elephantCreated;
    }
}
