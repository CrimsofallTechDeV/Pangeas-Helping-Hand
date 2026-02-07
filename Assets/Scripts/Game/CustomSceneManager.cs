using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(1)]
public class CustomSceneManager : MonoBehaviour
{
    public Light sun;
    public Material skyLight, skyDark;

    public GameObject[] dayObjects, nightObjects;

    public AudioClip musicClipDay, musicClipNight;
    public GameEnder enderManager;

    private void Awake()
    {
        GameManager.Instance.SetCustomSceneManager(this);
    }
}
