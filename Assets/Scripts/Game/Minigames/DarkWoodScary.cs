using UnityEngine;
using UnityEngine.Rendering;

public class DarkWoodScary : MonoBehaviour
{
    public Volume ppVolume;

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
        }
    }
}
