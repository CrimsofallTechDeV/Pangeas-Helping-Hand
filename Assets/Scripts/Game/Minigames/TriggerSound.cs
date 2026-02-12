using UnityEngine;

namespace CrimsofallTechnologies.VR.Gameplay
{
    public class TriggerSound : MonoBehaviour
    {
        public string Tag;
        public AudioSource source;

        private void OnTriggerEnter(Collider col)
        {
            if(col.tag == Tag)
            {
                source.PlayOneShot(source.clip);
            }
        }
    }
}
