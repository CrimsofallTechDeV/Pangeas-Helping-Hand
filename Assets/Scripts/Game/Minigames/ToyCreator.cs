using System.Collections;
using UnityEngine;

namespace CrimsofallTechnologies.VR.Gameplay 
{
    public class ToyCreator : MonoBehaviour
    {
        public int toysCount = 3;
        public GameObject elephant;
        public AudioClip createSound;
        public GameObject[] toyAssets;

        private AudioSource source;
        private int _toyCount = 0;

         private IEnumerator Start()
        {
			elephant.SetActive(false);
            source = GetComponent<AudioSource>();

            yield return new WaitForSeconds(4f);

            bool created = GameManager.Instance.thingsDone.Contains("ToyCreated");
            for (int i = 0; i < toyAssets.Length; i++)
            {
                toyAssets[i].SetActive(!created);
            }
            elephant.SetActive(created);
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

        public void StopSound()
        {
            source.Stop();
        }
    }
}
