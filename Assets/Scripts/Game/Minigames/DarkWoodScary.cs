using CrimsofallTechnologies.VR.Interaction;
using CrimsofallTechnologies.VR.Music;
using CrimsofallTechnologies.VR.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;

namespace CrimsofallTechnologies.VR.Gameplay 
{
    public class DarkWoodScary : MonoBehaviour
    {
        public Volume ppVolume;
        public AudioClip scaryClip;

        public CustomSceneManager sceneM;
        public MusicTrigger shootingTrigger;
        public ShootingGame shootingGame;
        public Entrance outEntr, inEntr;

        public VolumeProfile normalProfile;
        public VolumeProfile scaryProfile;

        public GameObject[] characters;

        private void Start()
        {
            ppVolume.profile = normalProfile;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                ppVolume.profile = scaryProfile;

                for (int i = 0; i < characters.Length; i++)
                {
                    characters[i].SetActive(false);
                }

                //change music all over
                shootingGame.houseClip = scaryClip;
                sceneM.musicClipDay = scaryClip;
                sceneM.musicClipNight = scaryClip;
                shootingTrigger.outMusicDay = scaryClip;
                shootingTrigger.outMusicNight = scaryClip;
                outEntr.musicClip = scaryClip;
                inEntr.musicClip = scaryClip;
                GameManager.Instance.bgmPlayer.PlayMusic(scaryClip);
            }
        }
    }
}
