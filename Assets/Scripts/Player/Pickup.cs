using UnityEngine;

namespace CrimsofallTechnologies.VR.Inventory 
{
    public class Pickup : MonoBehaviour
    {
        public ItemVar itemVar;

        public bool canCarry = true; //can player carry this item? if false - only for quest purposes, cant be added to inventory
    }
}
