using UnityEngine;
using UnityEngine.Rendering;

public class DarkWoodScary : MonoBehaviour
{
    public Volume ppVolume;

    public VolumeProfile normalProfile;
    public VolumeProfile scaryProfile;

    private void Start()
    {
        ppVolume.profile = normalProfile;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ppVolume.profile = scaryProfile;
        }
    }
}
