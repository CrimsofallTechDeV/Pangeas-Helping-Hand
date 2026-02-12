using UnityEngine;

namespace CrimsofallTechnologies.VR.Gameplay
{
    public class RevolvingDoor : MonoBehaviour
    {
        public float drag;

        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            rb.angularVelocity *= (1f - drag * Time.fixedDeltaTime);
        }
    }
}

