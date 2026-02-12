using UnityEngine;

namespace CrimsofallTechnologies.VR.Gameplay
{
    public class ElephantWaypoint : MonoBehaviour
    {
        public string Type;
        public Transform teleportPoint;

        private void OnTriggerEnter(Collider other)
        {
            Elephant elephant = other.GetComponentInChildren<Elephant>();
            if(elephant != null)
            {
                if(Type == "Teleport")
                {
                    elephant.agent.Warp(teleportPoint.position);
                }

                if(Type == "End")
                {
                    //trigger special jump and move forward animations!
                    elephant.End();
                }
            }
        }
    }
}
