using UnityEngine;

namespace CrimsofallTechnologies.XR.Gameplay 
{
    public class ToyCreator : MonoBehaviour
    {
        public int toysCount = 3;
        public GameObject elephant;
        public AudioClip createSound;

        private AudioSource source;
        private int _toyCount = 0;
		
		private void Start() {
			
			elephant.SetActive(false);
            source = GetComponent<AudioSource>();
		}

        //remove all old toys and create a new one!
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Toy")
            {
                Destroy(other.gameObject);
                _toyCount++;

                if(_toyCount >= toysCount)
                {
                    CreateNewToy();
                }
            }
        }

        private void CreateNewToy()
        {
            //enable and it will play all animations itself!
            elephant.SetActive(true);
            source.PlayOneShot(createSound);

            if(!GameManager.Instance.thingsDone.Contains("ToyCreated"))
                GameManager.Instance.thingsDone.Add("ToyCreated");
        }
    }
}
